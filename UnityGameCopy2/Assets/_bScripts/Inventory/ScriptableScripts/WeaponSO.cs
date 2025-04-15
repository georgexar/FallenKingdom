using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]

public class WeaponSO : Item
{
    public int damage;        // Damage dealt by the weapon
    public float attackSpeed; // Speed of attacks


    public override bool ItemFunction()
    {
       
        if (GameManager.Instance.QuickSlotManager.GetQuickSlot(1) != null)
        {
            if (GameManager.Instance.QuickSlotManager.GetQuickSlot(1).Item.itemID == itemID)
            {
                GameManager.Instance.QuickSlotManager.CastQuickSlot(1);
            }
            else //EQUIP
            {
                GameManager.Instance.QuickSlotManager.AddToQuickSlot(GameManager.Instance.ItemsManager.ReturnItem(itemID), 1, 1);
                GameManager.Instance.QuickSlotManager.CastQuickSlot(1);
            }
        }
        else
        {
            GameManager.Instance.QuickSlotManager.AddToQuickSlot(GameManager.Instance.ItemsManager.ReturnItem(itemID), 1, 1);
            GameManager.Instance.QuickSlotManager.CastQuickSlot(1);
        }

        return true;
    }

}