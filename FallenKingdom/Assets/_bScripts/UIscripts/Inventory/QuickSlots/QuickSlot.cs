using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private int slotIndex = 0;
    [SerializeField] private Sprite ConsumableSprite;


    private void Update()
    {
        InventorySlot newInvSlot = GameManager.Instance.QuickSlotManager.GetQuickSlot(slotIndex);
        if (newInvSlot == null)
        {
            if (itemImage == null || itemQuantity == null) return;
            Color color = itemImage.color;
            color = itemImage.color;
            color.a = 33f / 255f;
            itemImage.color = color;
            itemImage.sprite = ConsumableSprite;
            itemImage.preserveAspect = true;
            itemQuantity.gameObject.SetActive(false);
            return;
        }
        else
        {
            if (itemImage == null || itemQuantity == null) return;
            Color color = itemImage.color;
            color = itemImage.color;
            color.a = 1f;
            itemImage.color = color;
            itemQuantity.gameObject.SetActive(true);
            itemImage.sprite = newInvSlot.Item.icon;
            itemImage.preserveAspect = true;
            itemQuantity.text = newInvSlot.Quantity.ToString();
        }
    }

  
}
