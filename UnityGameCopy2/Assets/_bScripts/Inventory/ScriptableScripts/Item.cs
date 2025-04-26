
using UnityEngine;


public abstract class Item : ScriptableObject
{
    public GameObject GameObject;
    public int itemID;
    public string itemName;      
    public Sprite icon;                  
    public ItemType type;
    public ItemRarity ItemRarity;
    public bool isStackable;

    public abstract bool ItemFunction();
    

}
public enum ItemType
{
    Consumable, 
    Equipment,
    Currency,
    Quest
}

public enum ItemRarity 
{
    Uncommon,
    Rare,
    Epic,
    Legendary
}




