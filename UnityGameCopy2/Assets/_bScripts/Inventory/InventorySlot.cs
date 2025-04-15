using UnityEngine;

public class InventorySlot
{
    public Item Item { get; private set; } 
    public int Quantity { get; private set; }


    public InventorySlot(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
       
    }

    public void AddQuantity(int amount)
    {
        Quantity += amount;
        InventorySlot quickslot = GameManager.Instance.QuickSlotManager.ReturnQuickslot(Item);
        if (quickslot != null) { quickslot.Quantity += amount; }
    }

   
    public void RemoveQuantity(int amount)
    {
        Quantity -= amount;

        InventorySlot quickslot = GameManager.Instance.QuickSlotManager.ReturnQuickslot(Item);
        if (quickslot != null) { quickslot.Quantity -= amount;}

        if (Quantity <= 0)
        {
           GameManager.Instance.Player.GetInventory().GetInventorySlots().Remove(GameManager.Instance.Player.GetInventory().GetSlot(this.Item));
           GameManager.Instance.QuickSlotManager.RemoveItemByID(this.Item.itemID);
        }
    }


    public void ItemFunct()
    {
        if (Item != null)
        {
            bool success = Item.ItemFunction();
            if (success && Item.type == ItemType.Consumable)
            {
                RemoveQuantity(1);
                
            }
        }
    }
}
