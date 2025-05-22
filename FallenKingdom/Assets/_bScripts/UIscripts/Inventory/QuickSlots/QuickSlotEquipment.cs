using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotEquipment : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private int slotIndex = 0;
    [SerializeField] private Sprite SwordSprite;
    [SerializeField] private Sprite ShieldSprite;

    private void Update()
    {
        InventorySlot newInvSlot = GameManager.Instance.QuickSlotManager.GetQuickSlot(slotIndex);
        if (newInvSlot == null)
        {
            if (itemImage == null) return;
            if (SwordSprite != null) 
            {
                Color color = itemImage.color;
                color = itemImage.color;
                color = itemImage.color;
                color.a = 33f / 255f;
                itemImage.color = color;
                itemImage.sprite = SwordSprite;
                itemImage.preserveAspect = true;

            }
            else 
            {
                Color color = itemImage.color;
                color = itemImage.color;
                color = itemImage.color;
                color.a = 33f / 255f;
                itemImage.color = color;
                itemImage.sprite = ShieldSprite;
                itemImage.preserveAspect = true;
            }
            return;
        }
        else
        {
            if (itemImage == null) return;
            Color color = itemImage.color;
            color = itemImage.color;
            color.a = 1f;
            itemImage.color = color;
            itemImage.sprite = newInvSlot.Item.icon;
            itemImage.preserveAspect = true;
        }
    }

}
