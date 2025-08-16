using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotInInventory : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private int slotIndex = 0;
    private void OnEnable()
    {
        InventorySlot newInvSlot = GameManager.Instance.QuickSlotManager.GetQuickSlot(slotIndex);

        if (newInvSlot == null)
        {
            if (itemImage == null || itemQuantity == null) return;
            itemImage.gameObject.SetActive(false);
            itemQuantity.gameObject.SetActive(false);
        }
        else
        {
            if (!itemImage.gameObject.activeSelf)
            {
                if (itemImage == null || itemQuantity == null) return;
                itemImage.gameObject.SetActive(true);
                itemQuantity.gameObject.SetActive(true);

            }
            itemImage.sprite = newInvSlot.Item.icon;
            itemQuantity.text = newInvSlot.Quantity.ToString();
        }
    }
}

