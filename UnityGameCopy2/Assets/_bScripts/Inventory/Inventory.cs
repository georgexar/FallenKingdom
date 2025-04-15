using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
  
    private List<InventorySlot> slots;

    public Inventory()
    {
        slots = new List<InventorySlot>();
    }

    public bool AddItem(Item item, int quantity)
    {
      
        if (item.isStackable)
        {
            InventorySlot slot = GetSlot(item);
            if (slot != null)
            {
                slot.AddQuantity(quantity);
                
                return true;
            }
            else
            {
             
                slots.Add(new InventorySlot(item, quantity));
                return true;
            }
        }
        else
        {
            InventorySlot existingSlot = GetSlot(item);
            if (existingSlot == null)
            {
                slots.Add(new InventorySlot(item, 1));
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item item, int quantity)
    {
        InventorySlot slot = GetSlot(item);
        if (slot != null)
        { 
          slot.RemoveQuantity(quantity);
        }
    }

    public InventorySlot GetSlot(Item item)
    {
        if(item == null) return null;
        foreach (var slot in slots)
        {
            if (slot.Item.itemID == item.itemID)
            {
                return slot;
            }
        }
        return null;
    }

   
    public void DisplayInventory()
    {
        foreach (var slot in slots)
        {
            Debug.Log($"{slot.Item.itemName} - {slot.Quantity}");
        }
    }


    public List<InventorySlot> GetInventorySlots()
    {
        return slots;
    }

    public bool IsEmpty()
    {
        return slots == null || slots.Count == 0;
    }
}