using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private DeleteDialog deleteDialog;
    [SerializeField] private GameObject DeleteQuickSlotPanel;
    [SerializeField] private Toggle DeleteItems;

  

    private float doubleClickTimeLimit = 0.4f;
    private float lastClickTime = 0f;

    private float timer = 0f;
    private bool canPress = true;
    private bool startTimer=false;

    private void Update()
    {

        if (startTimer && timer<1.2f) 
        {
            timer += Time.deltaTime;
            canPress = false;
           
        }
        else if(timer>1.2f)
        {
            timer = 1.2f;
            canPress = true;
        }
    }

    private void OnEnable()
    {
        if (DeleteItems != null)
        {
            DeleteItems.isOn = false;
        }

        if (GameManager.Instance.Player.GetInventory() != null)
        {
            UpdateUI();
        }

    }

    private void OnDisable()
    {

        if (DeleteItems != null)
        {
            DeleteItems.isOn = false;
        }
    }

    private void UpdateUI()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        List<InventorySlot> slots = GameManager.Instance.Player.GetInventory().GetInventorySlots();
        foreach (InventorySlot slot in slots)
        {
            GameObject newItem = Instantiate(itemPrefab, content.transform);

            TMP_Text itemName = newItem.transform.Find("ItemNameTMP")?.GetComponent<TMP_Text>();
            TMP_Text itemQuantity = newItem.transform.Find("ItemQuantityTMP")?.GetComponent<TMP_Text>();
            UnityEngine.UI.Image itemSprite = newItem.transform.Find("ItemIconIMG")?.GetComponent<UnityEngine.UI.Image>();
            itemSprite.preserveAspect = true;
            UnityEngine.UI.Image rarityImage = newItem.transform.Find("ItemRarityIMG")?.GetComponent<UnityEngine.UI.Image>();

            if (itemName != null) itemName.text = slot.Item.itemName;
            if (itemQuantity != null) itemQuantity.text = slot.Quantity.ToString();
            if (itemSprite != null) itemSprite.sprite = slot.Item.icon;
            if (rarityImage != null) rarityImage.color = GetRarityColor(slot.Item.ItemRarity);
           

            InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                inventoryItem.inventorySlot = slot;
            }

           
            Button itemButton = newItem.GetComponent<Button>();
            if (itemButton != null)
            {
                itemButton.onClick.RemoveAllListeners();  
                itemButton.onClick.AddListener(() => HandleItemDoubleClick(slot));
            }

        }
    }


    public void EnableDeleteItems() 
    {
        if (DeleteItems.isOn) 
        {
            DeleteQuickSlotPanel.SetActive(true);
            foreach (Transform item in content.transform) 
            {
                item.Find("DeleteBtn").gameObject.SetActive(true);
                Button deleteButton = item.Find("DeleteBtn").GetComponent<Button>();
                InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
                if (inventoryItem != null)
                {
                    deleteButton.onClick.RemoveAllListeners();
                    deleteButton.onClick.AddListener(() => deleteDialog.ShowDeleteDialog(inventoryItem.inventorySlot));
                }
            }
        }
        else 
        {
            DeleteQuickSlotPanel.SetActive(false);
            foreach (Transform item in content.transform)
            {
                item.Find("DeleteBtn").gameObject.SetActive(false);
            }
        }
    }

    private void HandleItemDoubleClick(InventorySlot slot)
    {
      
        if (Time.time - lastClickTime < doubleClickTimeLimit)
        {
            HandleItemClick(slot);
        }
        lastClickTime = Time.time;
     
        UpdateUI();
    }

    private void HandleItemClick(InventorySlot slot)
    {
        if (!canPress) 
        {
            return;
        }
        if (slot != null)
        {
            slot.ItemFunct();

            startTimer = true;
            timer = 0f;
        }
    }


    private Color GetRarityColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Uncommon:
                return new Color(164/255f, 164 / 255f, 164 / 255f);
            case ItemRarity.Rare:
                return new Color(61 / 255f, 123 / 255f, 164 / 255f);
            case ItemRarity.Epic:
                return new Color(76/255f,36/255f, 79/255f);
            case ItemRarity.Legendary:
                return new Color(94/255f, 0f, 7/255f);
            default:
                return Color.white;
        }
    }
}
