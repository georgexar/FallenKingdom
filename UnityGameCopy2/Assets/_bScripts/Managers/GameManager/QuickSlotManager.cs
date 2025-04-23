
using System.Linq;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    public static QuickSlotManager Instance;

    private InventorySlot QuickSlot1;
    private InventorySlot QuickSlot2;
    private InventorySlot QuickSlot3;
    private InventorySlot QuickSlot4;
    private InventorySlot QuickSlot5;
    


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
        }

      //  InitializeQuickSlots();

    }
    public void InitializeQuickSlots()
    {
        QuickSlot1 = InitializeQuickSlot1();
        QuickSlot2 = InitializeQuickSlot2();
        QuickSlot3 = InitializeQuickSlot3();
        QuickSlot4 = InitializeQuickSlot4();
        QuickSlot5 = InitializeQuickSlot5();
        
    }


    public bool AddToQuickSlot(Item item, int quantity, int slotIndex)
    {
        RemoveItemByID(item.itemID);
        switch (slotIndex)
        {
            case 1:
                QuickSlot1 = new InventorySlot(item, quantity);
                break;
            case 2:
                QuickSlot2 = new InventorySlot(item, quantity);
                break;
            case 3:
                QuickSlot3 = new InventorySlot(item, quantity);
                break;
            case 4:
                QuickSlot4 = new InventorySlot(item, quantity);
                break;
            case 5:
                QuickSlot5 = new InventorySlot(item, quantity);
                break;
            default:
                Debug.LogError("Invalid quick slot index!");
                return false;
        }
        return true;
    }


    public bool RemoveFromQuickSlot(int slotIndex)
    {
        switch (slotIndex)
        {
            case 1:
                QuickSlot1 = null;
                break;
            case 2:
                QuickSlot2 = null;
                break;
            case 3:
                QuickSlot3 = null;
                break;
            case 4:
                QuickSlot4 = null;
                break;
            case 5:
                QuickSlot5 = null;
                break;
            default:
                Debug.LogError("Invalid quick slot index!");
                return false;
        }
        return true;
    }


    public InventorySlot GetQuickSlot(int slotIndex)
    {
        switch (slotIndex)
        {
            case 1:
                return QuickSlot1;
            case 2:
                return QuickSlot2;
            case 3:
                return QuickSlot3;
            case 4:
                return QuickSlot4;
            case 5:
                return QuickSlot5;
            default:
                return null;
        }
    }

    public void CastQuickSlot(int slotIndex)
    {
        switch (slotIndex)
        {
            case 1:
                if (StateManager.isFightingState == IsFightingState.Yes) return;
                if (QuickSlot1 != null)
                {
                    if (QuickSlot1.Item != null) 
                    {
                        Transform weaponPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponPosition");
                        Transform weaponInBody = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponInBody");

                        if (weaponPosition != null)
                        {

                            Transform existingWeapon = weaponPosition.childCount > 0 ? weaponPosition.GetChild(0) : null;

                            if (existingWeapon != null && existingWeapon.name == QuickSlot1.Item.GameObject.name + "(Clone)")
                            {
                                GameManager.Instance.Player.playerAnimator.SetTrigger("HideSword");
                                
                                Destroy(existingWeapon.gameObject , 0.7f); //UNEQUIP FROM HANDS EQUIP IN SWORD POSITION

                                if (weaponInBody != null)
                                {
                                    foreach (Transform child in weaponInBody)
                                    {
                                        Destroy(child.gameObject);
                                    }

                                    Invoke(nameof(InstantiateWeaponInBody), 0.7f);

                                }
                            }
                            else //EQUIP
                            {
                                foreach (Transform child in weaponPosition)
                                {
                                    Destroy(child.gameObject);
                                }
                                if (weaponInBody != null)
                                {
                                    foreach (Transform child in weaponInBody)
                                    {
                                        Destroy(child.gameObject ,0.7f );
                                    }
                                }
                                GameManager.Instance.Player.playerAnimator.SetTrigger("WithdrawSword");
                                Invoke(nameof(InstantiateWeaponInHand), 0.7f);

                            }
                        }
                        
                    }
                }
                return;
            case 2:
                if (StateManager.isBlocking == IsBlockingState.Yes) return;
                if (QuickSlot2 != null)
                {
                    if (QuickSlot2.Item != null)
                    {
                        Transform shieldPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldPosition");
                        Transform shieldInBody = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldInBody");

                        if (shieldPosition != null)
                        {

                            Transform existingShield = shieldPosition.childCount > 0 ? shieldPosition.GetChild(0) : null;

                            if (existingShield != null && existingShield.name == QuickSlot2.Item.GameObject.name + "(Clone)")
                            {
                                GameManager.Instance.Player.playerAnimator.SetTrigger("WithdrawShield");

                                Destroy(existingShield.gameObject, 0.7f); //UNEQUIP FROM HANDS EQUIP IN Shield POSITION

                                if (shieldInBody != null)
                                {
                                    foreach (Transform child in shieldInBody)
                                    {
                                        Destroy(child.gameObject);
                                    }

                                    Invoke(nameof(InstantiateShieldInBody), 0.7f);

                                }
                            }
                            else //EQUIP
                            {
                                foreach (Transform child in shieldPosition)
                                {
                                    Destroy(child.gameObject);
                                }
                                if (shieldInBody != null)
                                {
                                    foreach (Transform child in shieldInBody)
                                    {
                                        Destroy(child.gameObject, 0.7f);
                                    }
                                }
                                GameManager.Instance.Player.playerAnimator.SetTrigger("WithdrawShield");
                                Invoke(nameof(InstantiateShieldInHand), 0.7f);

                            }
                        }

                    }
                }
                return;
                
            case 3:
                if (QuickSlot3 != null)
                {
                    if (QuickSlot3.Item != null) GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot3.Item).ItemFunct();
                }
                return;
            case 4:
                if (QuickSlot4 != null)
                {
                    if (QuickSlot4.Item != null) GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot4.Item).ItemFunct();
                }
                return;
            case 5:
                if (QuickSlot5 != null)
                {
                    if (QuickSlot5.Item != null) GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot5.Item).ItemFunct();
                }
                return;
           
            default:
                return;
        }
    }
    private void InstantiateWeaponInBody()
    {
        Transform weaponInBody = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponInBody");
        if (weaponInBody != null)
        {
            GameObject instantiatedObject = Instantiate(QuickSlot1.Item.GameObject, weaponInBody.position, weaponInBody.rotation, weaponInBody);
            //instantiatedObject.name = QuickSlot1.Item.GameObject.name;
        }
    }

    private void InstantiateWeaponInHand()
    {
        Transform weaponPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponPosition");
        if (weaponPosition != null)
        {
            GameObject instantiatedObject = Instantiate(QuickSlot1.Item.GameObject, weaponPosition.position, weaponPosition.rotation, weaponPosition);
            //instantiatedObject.name = QuickSlot1.Item.GameObject.name;
           
            //Optional: Update QuickSlotManager

        }
    }
    private void InstantiateShieldInBody()
    {
        Transform shieldInBody = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldInBody");
        if (shieldInBody != null)
        {
            GameObject instantiatedObject = Instantiate(QuickSlot2.Item.GameObject, shieldInBody.position, shieldInBody.rotation, shieldInBody);
            //instantiatedObject.name = QuickSlot1.Item.GameObject.name;
        }
    }

    private void InstantiateShieldInHand()
    {
        Transform shieldPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldPosition");
        if (shieldPosition != null)
        {
            GameObject instantiatedObject = Instantiate(QuickSlot2.Item.GameObject, shieldPosition.position, shieldPosition.rotation, shieldPosition);
            //instantiatedObject.name = QuickSlot1.Item.GameObject.name;

            //Optional: Update QuickSlotManager

        }
    }
    public void RefreshQuickSlot()
    {
        if (QuickSlot1 != null)
        {
            QuickSlot1 = GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot1.Item);
        }
        if (QuickSlot2 != null)
        {
            QuickSlot2 = GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot2.Item);
        }
        if (QuickSlot3 != null)
        {
            QuickSlot3 = GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot3.Item);
        }
        if (QuickSlot4 != null)
        {
            QuickSlot4 = GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot4.Item);
        }
        if (QuickSlot5 != null)
        {
            QuickSlot5 = GameManager.Instance.Player.GetInventory().GetSlot(QuickSlot5.Item);
        }
       

    }

    public void RemoveItemByID(int itemID)
    {
        if (QuickSlot1 != null && QuickSlot1.Item.itemID == itemID)
            QuickSlot1 = null;

        if (QuickSlot2 != null && QuickSlot2.Item.itemID == itemID)
            QuickSlot2 = null;

        if (QuickSlot3 != null && QuickSlot3.Item.itemID == itemID)
            QuickSlot3 = null;

        if (QuickSlot4 != null && QuickSlot4.Item.itemID == itemID)
            QuickSlot4 = null;

        if (QuickSlot5 != null && QuickSlot5.Item.itemID == itemID)
            QuickSlot5 = null;

    }

    public InventorySlot ReturnQuickslot(Item item)
    {
        if (QuickSlot1 != null && QuickSlot1.Item.itemID == item.itemID) return QuickSlot1;

        if (QuickSlot2 != null && QuickSlot2.Item.itemID == item.itemID) return QuickSlot2;

        if (QuickSlot3 != null && QuickSlot3.Item.itemID == item.itemID) return QuickSlot3;

        if (QuickSlot4 != null && QuickSlot4.Item.itemID == item.itemID) return QuickSlot4;

        if (QuickSlot5 != null && QuickSlot5.Item.itemID == item.itemID) return QuickSlot5;

        return null;
    }
    public void PrintQuickSlots()
    {
        Debug.Log("QuickSlot 1: " + (QuickSlot1 != null ? QuickSlot1.Item.itemName + " x" + QuickSlot1.Quantity : "Empty"));
        Debug.Log("QuickSlot 2: " + (QuickSlot2 != null ? QuickSlot2.Item.itemName + " x" + QuickSlot2.Quantity : "Empty"));
        Debug.Log("QuickSlot 3: " + (QuickSlot3 != null ? QuickSlot3.Item.itemName + " x" + QuickSlot3.Quantity : "Empty"));
        Debug.Log("QuickSlot 4: " + (QuickSlot4 != null ? QuickSlot4.Item.itemName + " x" + QuickSlot4.Quantity : "Empty"));
        Debug.Log("QuickSlot 5: " + (QuickSlot5 != null ? QuickSlot5.Item.itemName + " x" + QuickSlot5.Quantity : "Empty"));
       
    }





    public void SaveQuickSlots()
    {
        if (QuickSlot1 == null)
        {
            PlayerPrefs.SetInt("QuickSlot1ItemID", -1);
            PlayerPrefs.SetInt("QuickSlot1ItemQuantity", -1);
        }
        else
        {
            PlayerPrefs.SetInt("QuickSlot1ItemID", QuickSlot1.Item.itemID);
            PlayerPrefs.SetInt("QuickSlot1ItemQuantity", QuickSlot1.Quantity);
        }
        if (QuickSlot2 == null)
        {
            PlayerPrefs.SetInt("QuickSlot2ItemID", -1);
            PlayerPrefs.SetInt("QuickSlot2ItemQuantity", -1);
        }
        else
        {
            PlayerPrefs.SetInt("QuickSlot2ItemID", QuickSlot2.Item.itemID);
            PlayerPrefs.SetInt("QuickSlot2ItemQuantity", QuickSlot2.Quantity);
        }
        if (QuickSlot3 == null)
        {
            PlayerPrefs.SetInt("QuickSlot3ItemID", -1);
            PlayerPrefs.SetInt("QuickSlot3ItemQuantity", -1);
        }
        else
        {
            PlayerPrefs.SetInt("QuickSlot3ItemID", QuickSlot3.Item.itemID);
            PlayerPrefs.SetInt("QuickSlot3ItemQuantity", QuickSlot3.Quantity);
        }
        if (QuickSlot4 == null)
        {
            PlayerPrefs.SetInt("QuickSlot4ItemID", -1);
            PlayerPrefs.SetInt("QuickSlot4ItemQuantity", -1);
        }
        else
        {
            PlayerPrefs.SetInt("QuickSlot4ItemID", QuickSlot4.Item.itemID);
            PlayerPrefs.SetInt("QuickSlot4ItemQuantity", QuickSlot4.Quantity);
        }
        if (QuickSlot5 == null)
        {
            PlayerPrefs.SetInt("QuickSlot5ItemID", -1);
            PlayerPrefs.SetInt("QuickSlot5ItemQuantity", -1);
        }
        else
        {
            PlayerPrefs.SetInt("QuickSlot5ItemID", QuickSlot5.Item.itemID);
            PlayerPrefs.SetInt("QuickSlot5ItemQuantity", QuickSlot5.Quantity);
        }
        
    }


    public void PrintPlayerPrefs()
    {
        // QuickSlot 1
        Debug.Log("QuickSlot 1 ItemID: " + PlayerPrefs.GetInt("QuickSlot1ItemID", -1));
        Debug.Log("QuickSlot 1 ItemQuantity: " + PlayerPrefs.GetInt("QuickSlot1ItemQuantity", -1));

        // QuickSlot 2
        Debug.Log("QuickSlot 2 ItemID: " + PlayerPrefs.GetInt("QuickSlot2ItemID", -1));
        Debug.Log("QuickSlot 2 ItemQuantity: " + PlayerPrefs.GetInt("QuickSlot2ItemQuantity", -1));

        // QuickSlot 3
        Debug.Log("QuickSlot 3 ItemID: " + PlayerPrefs.GetInt("QuickSlot3ItemID", -1));
        Debug.Log("QuickSlot 3 ItemQuantity: " + PlayerPrefs.GetInt("QuickSlot3ItemQuantity", -1));

        // QuickSlot 4
        Debug.Log("QuickSlot 4 ItemID: " + PlayerPrefs.GetInt("QuickSlot4ItemID", -1));
        Debug.Log("QuickSlot 4 ItemQuantity: " + PlayerPrefs.GetInt("QuickSlot4ItemQuantity", -1));

        // QuickSlot 5
        Debug.Log("QuickSlot 5 ItemID: " + PlayerPrefs.GetInt("QuickSlot5ItemID", -1));
        Debug.Log("QuickSlot 5 ItemQuantity: " + PlayerPrefs.GetInt("QuickSlot5ItemQuantity", -1));

    
       

       
    }


    private InventorySlot InitializeQuickSlot1()
    {
        int itemID = PlayerPrefs.GetInt("QuickSlot1ItemID", -1);
        int itemQuantity = PlayerPrefs.GetInt("QuickSlot1ItemQuantity", -1);

        if (itemID == -1 || itemQuantity == -1)
        {
            return null;
        }
        else
        {
            Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);
            if (item != null)
            {
                return new InventorySlot(item, itemQuantity);
            }
            return null;
        }
    }

    private InventorySlot InitializeQuickSlot2()
    {
        int itemID = PlayerPrefs.GetInt("QuickSlot2ItemID", -1);
        int itemQuantity = PlayerPrefs.GetInt("QuickSlot2ItemQuantity", -1);

        if (itemID == -1 || itemQuantity == -1)
        {
            return null;
        }
        else
        {
            Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);
            if (item != null)
            {
                return new InventorySlot(item, itemQuantity);
            }
            return null;
        }
    }

    private InventorySlot InitializeQuickSlot3()
    {
        int itemID = PlayerPrefs.GetInt("QuickSlot3ItemID", -1);
        int itemQuantity = PlayerPrefs.GetInt("QuickSlot3ItemQuantity", -1);

        if (itemID == -1 || itemQuantity == -1)
        {
            return null;
        }
        else
        {
            Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);
            if (item != null)
            {
                return new InventorySlot(item, itemQuantity);
            }
            return null;
        }
    }

    private InventorySlot InitializeQuickSlot4()
    {
        int itemID = PlayerPrefs.GetInt("QuickSlot4ItemID", -1);
        int itemQuantity = PlayerPrefs.GetInt("QuickSlot4ItemQuantity", -1);

        if (itemID == -1 || itemQuantity == -1)
        {
            return null;
        }
        else
        {
            Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);
            if (item != null)
            {
                return new InventorySlot(item, itemQuantity);
            }
            return null;
        }
    }

    private InventorySlot InitializeQuickSlot5()
    {
        int itemID = PlayerPrefs.GetInt("QuickSlot5ItemID", -1);
        int itemQuantity = PlayerPrefs.GetInt("QuickSlot5ItemQuantity", -1);

        if (itemID == -1 || itemQuantity == -1)
        {
            return null;
        }
        else
        {
            Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);
            if (item != null)
            {
                return new InventorySlot(item, itemQuantity);
            }
            return null;
        }


    }

   
}
