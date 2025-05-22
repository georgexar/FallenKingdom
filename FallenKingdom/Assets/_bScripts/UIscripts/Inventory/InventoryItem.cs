using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InventorySlot inventorySlot;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null )
        {
            string quickSlotTag = eventData.pointerEnter.tag;
            if (quickSlotTag == "QuickSlot1" && (inventorySlot.Item.itemName == "Weapon"))
            {
                if (GameManager.Instance.QuickSlotManager.GetQuickSlot(1) == null)
                { 
                    GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 1);
                    GameManager.Instance.QuickSlotManager.CastQuickSlot(1);
                   
                }
                else if (inventorySlot.Item.itemID != GameManager.Instance.QuickSlotManager.GetQuickSlot(1).Item.itemID)
                {
                    GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 1);
                    GameManager.Instance.QuickSlotManager.CastQuickSlot(1);
                }
            }
            else if (quickSlotTag == "QuickSlot2" && (inventorySlot.Item.itemName == "Shield"))
            {
                if (GameManager.Instance.QuickSlotManager.GetQuickSlot(2) == null)
                {
                    GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 2);
                    GameManager.Instance.QuickSlotManager.CastQuickSlot(2);

                }
                else if (inventorySlot.Item.itemID != GameManager.Instance.QuickSlotManager.GetQuickSlot(2).Item.itemID)
                {
                    GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 2);
                    GameManager.Instance.QuickSlotManager.CastQuickSlot(2);
                }
            }
            else if (quickSlotTag == "QuickSlot3" && (inventorySlot.Item.type == ItemType.Consumable))
            {
                GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 3);
            }
            else if (quickSlotTag == "QuickSlot4" && (inventorySlot.Item.type == ItemType.Consumable))
            {
                GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 4);
            }
            else if (quickSlotTag == "QuickSlot5" && (inventorySlot.Item.type == ItemType.Consumable))
            {
                GameManager.Instance.QuickSlotManager.AddToQuickSlot(inventorySlot.Item, inventorySlot.Quantity, 5);
            }

        }
        rectTransform.anchoredPosition = Vector2.zero;
        GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas/QuickSlotsPanel").gameObject.SetActive(false);
        GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas/QuickSlotsPanel").gameObject.SetActive(true);
        GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas/InventoryScrollView/Viewport").gameObject.SetActive(false);
        GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas/InventoryScrollView/Viewport").gameObject.SetActive(true);
    }
}
