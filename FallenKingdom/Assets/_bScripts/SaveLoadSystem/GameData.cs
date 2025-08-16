using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class GameData
{
    [Header("Player Attributes to Save")]
    public List<float> loadedPlayerPosition;
    public float loadedPlayerHealth;
    public float loadedPlayerEnergy;
    public float loadedPlayerMana;
    public int loadedPlayerBlueGems;
    public int loadedPlayerPurpleGems;

    [Header("Scene to Save")]
    public string loadedCurrentScene;


    public GameData(Player player, string currentSceneName)
    {
        this.loadedPlayerPosition = new List<float> {
            player.playerObject.transform.position.x,
            player.playerObject.transform.position.y,
            player.playerObject.transform.position.z
        };

        this.loadedPlayerHealth = player.GetHealth();
        this.loadedPlayerEnergy = player.GetEnergy();
        this.loadedPlayerMana = player.GetMana();
        this.loadedPlayerBlueGems = player.GetBlueGems();
        this.loadedPlayerPurpleGems = player.GetPurpleGems();

        this.loadedCurrentScene = currentSceneName;
    }
}


[System.Serializable]
public class InventoryData
{
    public List<List<int>> inventoryList;

    public InventoryData()
    {
        inventoryList = GameManager.Instance.ConvertInventoryDictionaryToList();
    }
}


[System.Serializable]
public class QuickSlotData
{
    public int slotIndex;
    public int itemId; 
    public int quantity;

    public QuickSlotData(int slotIndex, int itemId, int quantity)
    {
        this.slotIndex = slotIndex;
        this.itemId = itemId;
        this.quantity = quantity;
    }
}

