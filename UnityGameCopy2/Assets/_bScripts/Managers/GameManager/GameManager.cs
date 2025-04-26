
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public Player Player { get; set; }
    public InputManager InputManager { get; set; }
    public QuickSlotManager QuickSlotManager { get; set; }
    public ItemsManager ItemsManager { get; set; }
    public SoundsFxManager SoundsFxManager { get; set; }

    [SerializeField] private GameObject PlayerPrefab;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
       
        InitializeManagers();
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    
    private void InitializeManagers()
    {
       
        InputManager = InputManager.Instance;
        ItemsManager = ItemsManager.Instance;
        QuickSlotManager = QuickSlotManager.Instance;
        SoundsFxManager = SoundsFxManager.Instance;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") //EAN PAW STO MAIN MENU...
        {
            //DESTROY OBJECTS ON MAIN MENU
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;

            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                Destroy(playerObject);
                GameManager.Instance.Player = null;
            }
            return;
        }
       else if(scene.name == "Game") 
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

            GameObject existingPlayer = GameObject.Find("Player");
            if (existingPlayer == null) //EAN DEN YPARXEI HDH PAIKTHS STHN SKHNH MHN KANEIS TIPOTA ALLIWS DHMIOURGHSE TON
            {
                GameManager.Instance.CreateOrLoadPlayer();
            }
        }
        
    }

    
    public void CreateOrLoadPlayer() 
    {

        GameObject existingPlayer = GameObject.Find("Player");
        if (existingPlayer != null) //EAN YPARXEI HDH PAIKTHS STHN SKHNH MHN KANEIS TIPOTA ALLIWS DHMIOURGHSE TON
        {
            Destroy(existingPlayer);
            GameManager.Instance.Player=null;
        }

        GameData savedData = SaveLoadManager.LoadGameDataFromJSON();
        Inventory loadedInventory = SaveLoadManager.GetLoadedInventoryFromJson();
        List<List<int>> list = SaveLoadManager.LoadQuickSlotsFromJSON();
        List<string> collectedItems = SaveLoadManager.LoadCollectedItems();
        List<string> killedEnemies = SaveLoadManager.LoadKilledEnemies();
        GameManager.Instance.QuickSlotManager.InitializeQuickSlots();
        if (savedData != null) //YPARXEI APOTHIKEUMENO ARXEIO LOADED
        {
            Vector3 loadedPlayerPosition = new Vector3(savedData.loadedPlayerPosition[0], savedData.loadedPlayerPosition[1], savedData.loadedPlayerPosition[2]);
            GameObject playerObject = Instantiate(PlayerPrefab, loadedPlayerPosition, Quaternion.identity);
            Player = new Player(playerObject);
            playerObject.name = "Player";
            playerObject.tag = "Player";
            Player.SetHealth(savedData.loadedPlayerHealth);
            Player.SetEnergy(savedData.loadedPlayerEnergy);
            Player.SetMana(savedData.loadedPlayerMana);
            Player.SetBlueGems(savedData.loadedPlayerBlueGems);
            Player.SetPurpleGems(savedData.loadedPlayerPurpleGems);
            Player.SetCollectedItems(collectedItems);
            Player.SetKilledEnemies(killedEnemies);



            if (collectedItems != null && collectedItems.Count > 0)
            {
                foreach (string itemName in collectedItems)
                {
                    GameObject itemObject = GameObject.Find(itemName);
                    if (itemObject != null)
                    {
                        Destroy(itemObject);
                       // Debug.Log("Destroyed item: " + itemName);
                    }
                    else
                    {
                       // Debug.LogWarning("Item not found in the scene: " + itemName);
                    }
                }
            }
            if (killedEnemies != null && killedEnemies.Count > 0)
            {
                foreach (string enemy in killedEnemies)
                {
                    GameObject enemyObject = FindInactiveObjectByName(enemy);
                    if (enemyObject != null)
                    {
                        Destroy(enemyObject);
                        // Debug.Log("Destroyed item: " + itemName);
                    }
                    else
                    {
                        // Debug.LogWarning("Item not found in the scene: " + itemName);
                    }
                }
            }






            if (loadedInventory != null)
            {
                GameManager.Instance.Player.SetInventory(loadedInventory);
            }
            if (list != null && list.Count > 0)
            {
                AssignQuickSlotsFromList(list);
            }







            //OTI EIXA SAVED GIA TON PLAYER TA BAZW EDW
            DontDestroyOnLoad(playerObject);
        }
        else // DEN YPARXEI LOADED GIA TON PLAYER - NEWGAMEBTN
        {
            GameObject spawnPoint = GameObject.Find("SpawnPoint");
            if (spawnPoint == null)
            {
                Debug.LogWarning("SpawnPoint not found!");
                return;
            }
            Vector3 spawnPosition = spawnPoint.transform.position;
          //  spawnPosition.y += 1.8f;
            spawnPosition.y += 2f;
            GameObject playerObject = Instantiate(PlayerPrefab, spawnPosition, spawnPoint.transform.rotation);
            Player = new Player(playerObject);
            playerObject.name = "Player";
            playerObject.tag = "Player";
            DontDestroyOnLoad(playerObject);
            
        }
        GameManager.Instance.InputManager.EnableInputActions();
    }

    private GameObject FindInactiveObjectByName(string objectName)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == objectName)
            {
                return obj;
            }
        }
        return null;
    }

    private void TransferPlayer() 
    {
        GameObject existingPlayer = GameObject.Find("Player");
        if (existingPlayer == null)
        {
            return;
        }

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint not found!");
            return;
        }
        Vector3 spawnPosition = spawnPoint.transform.position;
        spawnPosition.y += 1.8f;
        Player.playerObject.transform.position = spawnPosition;

    }


    public Dictionary<int, int> GetInventoryAsDictionary()
    {
        Dictionary<int, int> inventoryDict = new Dictionary<int, int>();

        List<InventorySlot> inventorySlots = Player.GetInventory().GetInventorySlots();

         
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.Item != null)
            {
                inventoryDict[slot.Item.itemID] = slot.Quantity;
               
            }
        }
        return inventoryDict;
    }

    public List<List<int>> ConvertInventoryDictionaryToList()
    {
        List<List<int>> inventoryList = new List<List<int>>();

        foreach (var kvp in GetInventoryAsDictionary())
        {
            inventoryList.Add(new List<int> { kvp.Key, kvp.Value });
        }

      
        return inventoryList;
    }

    public List<List<int>> GetQuickSlotsAsList()
    {
        List<List<int>> quickSlotsList = new List<List<int>>();

        for (int i = 1; i <= 5; i++)
        {
            InventorySlot slot = GameManager.Instance.QuickSlotManager.GetQuickSlot(i);
            if (slot != null && slot.Item != null)
            {
                quickSlotsList.Add(new List<int> { i, slot.Item.itemID, slot.Quantity });
            }
        }
    
        return quickSlotsList;
    }

    public void AssignQuickSlotsFromList(List<List<int>> quickSlotsList)
    {
        if (quickSlotsList == null || quickSlotsList.Count == 0)
        {
           // Debug.LogWarning("QuickSlots list is empty. No quickslots to assign.");
            return;
        }

        foreach (var quickSlotData in quickSlotsList)
        {
            if (quickSlotData.Count >= 3)
            {
                int quickSlotIndex = quickSlotData[0];
                int itemID = quickSlotData[1];
                int quantity = quickSlotData[2];

               
                Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);

                if (item != null)
                {
                    GameManager.Instance.QuickSlotManager.AddToQuickSlot(item,quantity,quickSlotIndex);
                    //Debug.Log($"Assigned QuickSlot {quickSlotIndex} with ItemID {itemID} and Quantity {quantity}.");
                }
                else
                {
                    //Debug.LogWarning($"Item with ID {itemID} not found. Skipping QuickSlot {quickSlotIndex}.");
                }
            }
            else
            {
               // Debug.LogWarning("Invalid quickslot data format. Skipping.");
            }
        }
    }
}
