using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveLoadManager
{

    private static readonly string saveFilePath = Application.persistentDataPath + "/savegame.json";
    private static readonly string inventoryFilePath = Application.persistentDataPath + "/saveInventory.json";
    private static readonly string quickSlotsFilePath = Application.persistentDataPath + "/saveQuickslots.json";
    private static readonly string collectedItemsFilePath = Application.persistentDataPath + "/collectedItems.json";
    private static readonly string killedEnemiesFilePath = Application.persistentDataPath + "/killedEnemies.json";
    private static readonly string npcStateFilePath = Application.persistentDataPath + "/npcState.json";
   


    public static void DeleteSaveFile() //NEWGAME
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        if (File.Exists(inventoryFilePath))
        {
            File.Delete(inventoryFilePath);
        }
        if (File.Exists(quickSlotsFilePath))
        {
            File.Delete(quickSlotsFilePath);
        }
        if (File.Exists(collectedItemsFilePath))
        {
            File.Delete(collectedItemsFilePath);
        }
        if (File.Exists(killedEnemiesFilePath))
        {
            File.Delete(killedEnemiesFilePath);
        }
        if (File.Exists(npcStateFilePath))
        {
            File.Delete(npcStateFilePath);
        }
       // PlayerPrefs.SetInt("PortalActivated", 0);
       // PlayerPrefs.Save();
    }

    public static void SaveCollectedItems()
    {
        try
        {
            if (GameManager.Instance.Player.GetCollectedItems() == null || GameManager.Instance.Player.GetCollectedItems().Count == 0)
            {

                File.WriteAllText(collectedItemsFilePath, "[]");
                //  Debug.Log("Collected items list is empty. Saved as []");
            }
            else
            {

                string json = "[" + string.Join(",", GameManager.Instance.Player.GetCollectedItems().Select(item => "\"" + item + "\"")) + "]";
                File.WriteAllText(collectedItemsFilePath, json);
                // Debug.Log("Collected items saved successfully.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save collected items: " + ex.Message);
        }
    }

    public static List<string> LoadCollectedItems()
    {
        try
        {
            if (File.Exists(collectedItemsFilePath))
            {
                string json = File.ReadAllText(collectedItemsFilePath);

                if (string.IsNullOrWhiteSpace(json) || json == "[]")
                {
                    //Debug.Log("No collected items found. Returning an empty list.");
                    return new List<string>();
                }

                json = json.Trim('[', ']');

                string[] items = json.Split(new string[] { "\",\"" }, StringSplitOptions.None);


                List<string> collectedItems = new List<string>();
                foreach (string item in items)
                {
                    collectedItems.Add(item.Replace("\"", ""));
                }

                //Debug.Log("Loaded collected items: " + string.Join(", ", collectedItems));
                return collectedItems;
            }
            else
            {
                //Debug.LogWarning("No collected items data found.");
                return new List<string>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Failed to load collected items: " + ex.Message);
            return new List<string>();
        }
    }

    public static void SaveKilledEnemies()
    {
        try
        {
            if (GameManager.Instance.Player.GetKilledEnemies() == null || GameManager.Instance.Player.GetKilledEnemies().Count == 0)
            {

                File.WriteAllText(killedEnemiesFilePath, "[]");
               
            }
            else
            {

                string json = "[" + string.Join(",", GameManager.Instance.Player.GetKilledEnemies().Select(enemy => "\"" + enemy + "\"")) + "]";
                File.WriteAllText(killedEnemiesFilePath, json);
                
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save collected items: " + ex.Message);
        }
    }

    public static List<string> LoadKilledEnemies()
    {
        try
        {
            if (File.Exists(killedEnemiesFilePath))
            {
                string json = File.ReadAllText(killedEnemiesFilePath);

                if (string.IsNullOrWhiteSpace(json) || json == "[]")
                {
                    
                    return new List<string>();
                }

                json = json.Trim('[', ']');

                string[] enemies = json.Split(new string[] { "\",\"" }, StringSplitOptions.None);


                List<string> killedEnemies = new List<string>();
                foreach (string enemy in enemies)
                {
                    killedEnemies.Add(enemy.Replace("\"", ""));
                }

                //Debug.Log("Loaded collected items: " + string.Join(", ", collectedItems));
                return killedEnemies;
            }
            else
            {
                //Debug.LogWarning("No collected items data found.");
                return new List<string>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Failed to load collected items: " + ex.Message);
            return new List<string>();
        }
    }


    public static void SaveGameData(GameData gameData)
    {
        try
        {
            // Convert GameData object to JSON string
            string json = JsonUtility.ToJson(gameData, true);


            File.WriteAllText(saveFilePath, json);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save game: " + ex.Message);
        }
    }


    public static void SaveInventory()
    {
        try
        {
            InventoryData inventoryData = new InventoryData();
            List<List<int>> inventoryList = inventoryData.inventoryList;

            if (inventoryList == null || inventoryList.Count == 0)
            {

                File.WriteAllText(inventoryFilePath, "[]");
                //  Debug.Log("Inventory is empty. Saved as []");
            }
            else
            {
                List<string> savedSubLists = new List<string>();

                foreach (List<int> subList in inventoryList)
                {

                    string subListString = "[" + string.Join(",", subList) + "]";
                    savedSubLists.Add(subListString);
                }


                string json = "[" + string.Join(",", savedSubLists) + "]";


                File.WriteAllText(inventoryFilePath, json);

                // Debug.Log("Final JSON to be saved: " + json);
            }
            //Debug.Log("Inventory saved successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save inventory: " + ex.Message);
        }
    }


    // Load GameData from a JSON file
    public static GameData LoadGameDataFromJSON()
    {
        try
        {
            // Check if the save file exists
            if (File.Exists(saveFilePath))
            {
                // Read the JSON string from the file
                string json = File.ReadAllText(saveFilePath);

                // Convert the JSON string back to a GameData object
                GameData gameData = JsonUtility.FromJson<GameData>(json);
                return gameData;
            }
            else
            {
                return null; // Return null if no save file exists
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load game: " + ex.Message);
            return null;
        }
    }

    private static List<List<int>> LoadInventoryFromJSON()
    {


        try
        {
            if (File.Exists(inventoryFilePath))
            {
                string json = File.ReadAllText(inventoryFilePath);

                if (string.IsNullOrWhiteSpace(json) || json == "[]")
                {
                    //  Debug.Log("Empty inventory data loaded. Returning an empty list.");
                    return new List<List<int>>();
                }


                json = json.Trim('[', ']');


                string[] subListStrings = json.Split(new string[] { "],[" }, System.StringSplitOptions.None);

                List<List<int>> loadedInventory = new List<List<int>>();

                foreach (string subListString in subListStrings)
                {

                    string[] items = subListString.Trim('[', ']').Split(',');

                    List<int> subList = new List<int>();
                    foreach (string item in items)
                    {
                        subList.Add(int.Parse(item));
                    }

                    loadedInventory.Add(subList);
                }

                //   Debug.Log("Loaded Inventory: " + FormatInventoryAsString(loadedInventory));
                return loadedInventory;
            }
            else
            {
                // Debug.LogWarning("No inventory data found.");
                return new List<List<int>>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load inventory: " + ex.Message);
            return new List<List<int>>();
        }
    }

    public static Inventory GetLoadedInventoryFromJson()
    {
        List<List<int>> rawInventory = LoadInventoryFromJSON();
        Inventory inventory = new Inventory();

        foreach (var subList in rawInventory)
        {
            if (subList.Count >= 2)
            {
                int itemID = subList[0];
                int quantity = subList[1];

                Item item = GameManager.Instance.ItemsManager.ReturnItem(itemID);

                if (item != null)
                {
                    inventory.AddItem(item, quantity);
                }
                else
                {
                    //  Debug.LogWarning($"Item with ID {itemID} not found. Skipping.");
                }
            }
            else
            {
                //  Debug.LogWarning("Invalid sublist format. Skipping.");
            }
        }

        return inventory;
    }


    private static string FormatInventoryAsString(List<List<int>> inventory)
    {
        List<string> subLists = new List<string>();
        foreach (var subList in inventory)
        {
            subLists.Add("[" + string.Join(", ", subList) + "]");
        }
        return "[" + string.Join(", ", subLists) + "]";
    }





    public static void PrintJSONFile()
    {
        try
        {

            if (File.Exists(saveFilePath))
            {

                string json = File.ReadAllText(saveFilePath);


                // Debug.Log("Saved JSON File:\n" + json);
            }
            else
            {
                //  Debug.LogWarning("No save file found to print.");
            }
            if (File.Exists(inventoryFilePath))
            {
                string json = File.ReadAllText(inventoryFilePath);
                // Debug.Log("Inventory JSON:\n" + json);
            }
            else
            {
                // Debug.LogWarning("No inventory data found.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to print JSON file: " + ex.Message);
        }
    }



    public static void SaveQuickSlots()
    {
        try
        {

            List<List<int>> quickSlotsList = GameManager.Instance.GetQuickSlotsAsList();


            if (quickSlotsList == null || quickSlotsList.Count == 0)
            {
                File.WriteAllText(quickSlotsFilePath, "[]");
                //   Debug.Log("QuickSlots are empty. Saved as []");
            }
            else
            {

                List<string> savedSubLists = new List<string>();

                foreach (List<int> subList in quickSlotsList)
                {
                    string subListString = "[" + string.Join(",", subList) + "]";
                    savedSubLists.Add(subListString);
                }


                string json = "[" + string.Join(",", savedSubLists) + "]";
                File.WriteAllText(quickSlotsFilePath, json);

                //   Debug.Log("Final JSON to be saved: " + json);
            }

            // Debug.Log("QuickSlots saved successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save quickslots: " + ex.Message);
        }
    }



    public static List<List<int>> LoadQuickSlotsFromJSON()
    {
        try
        {
            if (File.Exists(quickSlotsFilePath))
            {
                string json = File.ReadAllText(quickSlotsFilePath);

                if (string.IsNullOrWhiteSpace(json) || json == "[]")
                {
                    //  Debug.Log("Empty quickslots data loaded. Returning an empty list.");
                    return new List<List<int>>();
                }

                json = json.Trim('[', ']');

                string[] subListStrings = json.Split(new string[] { "],[" }, System.StringSplitOptions.None);

                List<List<int>> loadedQuickSlots = new List<List<int>>();

                foreach (string subListString in subListStrings)
                {
                    string[] items = subListString.Trim('[', ']').Split(',');

                    List<int> subList = new List<int>();
                    foreach (string item in items)
                    {
                        subList.Add(int.Parse(item));
                    }

                    loadedQuickSlots.Add(subList);
                }

                //  Debug.Log("Loaded QuickSlots: " + FormatQuickSlotsAsString(loadedQuickSlots));
                return loadedQuickSlots;
            }
            else
            {
                //      Debug.LogWarning("No quickslots data found.");
                return new List<List<int>>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load quickslots: " + ex.Message);
            return new List<List<int>>();
        }
    }



    private static string FormatQuickSlotsAsString(List<List<int>> quickSlots)
    {
        return string.Join(", ", quickSlots.Select(subList => $"[{string.Join(", ", subList)}]"));
    }


    public static void SaveNpcState(List<Npc> npcs)
    {
        List<NpcState> npcStates = new List<NpcState>();

        foreach (var npc in npcs)
        {
            NpcState state = new NpcState
            {
                npcId = npc.NpcId,
                hasGivenItem = !npc.GiveItem,
                hasGivenBlueGems = !npc.GiveBlueGems,
                hasGivenPurpleGems = !npc.GivePurpleGems,
                isInteractable = npc.IsInteractable,
                currentSentenceIndex = npc.CurrentSentenceIndex,
                hasEnabledGameObject = !npc.EnableGameObject ,
                hasDisabledGameObject = !npc.DisableGameObject ,
                keepGameObjectEnabled = npc.KeepGameObjectEnabled,
                keepGameObjectDisabled = npc.KeepGameObjectDisabled
            };

            npcStates.Add(state);
        }

        string json = JsonUtility.ToJson(new NpcStateList { npcStates = npcStates }, true);
        // Debug.Log("Saving NPC State to JSON: " + json);
        File.WriteAllText(npcStateFilePath, json);
    }


    public static List<NpcState> LoadNpcState()
    {
        if (!File.Exists(npcStateFilePath))
        {
            // Debug.Log("No saved NPC state found.");
            return new List<NpcState>();
        }

        string json = File.ReadAllText(npcStateFilePath);
        // Debug.Log("Loaded NPC State from JSON: " + json);

        NpcStateList npcStateList = JsonUtility.FromJson<NpcStateList>(json);




        return npcStateList != null ? npcStateList.npcStates : new List<NpcState>();
    }


}


[System.Serializable]
public class NpcState
{
    public string npcId;
    public bool hasGivenItem;
    public bool hasGivenBlueGems;
    public bool hasGivenPurpleGems;
    public bool isInteractable;
    public int currentSentenceIndex;
    public bool hasEnabledGameObject;
    public bool hasDisabledGameObject;
    public bool keepGameObjectEnabled;
    public bool keepGameObjectDisabled;
}

[System.Serializable]
public class NpcStateList
{
    public List<NpcState> npcStates;
}