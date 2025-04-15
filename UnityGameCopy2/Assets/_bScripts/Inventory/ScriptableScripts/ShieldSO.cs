using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShield", menuName = "Inventory/Shield")]
public class ShieldSO : Item
{
    public int sustain;
    public int damage;

    public override bool ItemFunction()
    {
        if (GameManager.Instance.QuickSlotManager.GetQuickSlot(2) != null)
        {
            if (GameManager.Instance.QuickSlotManager.GetQuickSlot(2).Item.itemID == itemID)
            {
                GameManager.Instance.QuickSlotManager.CastQuickSlot(2);
            }
            else //EQUIP
            {
                GameManager.Instance.QuickSlotManager.AddToQuickSlot(GameManager.Instance.ItemsManager.ReturnItem(itemID), 1, 2);
                GameManager.Instance.QuickSlotManager.CastQuickSlot(2);
            }
        }
        else
        {
            GameManager.Instance.QuickSlotManager.AddToQuickSlot(GameManager.Instance.ItemsManager.ReturnItem(itemID), 1, 2);
            GameManager.Instance.QuickSlotManager.CastQuickSlot(2);
        }
        return true;
    }
}