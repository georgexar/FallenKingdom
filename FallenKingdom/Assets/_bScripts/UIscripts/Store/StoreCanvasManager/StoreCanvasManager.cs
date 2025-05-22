using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class StoreCanvasManager : MonoBehaviour
{
    
    [SerializeField] private GameObject content; 
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Item> items = new List<Item>(); 
    [SerializeField] private List<int> quantities = new List<int>();
    [SerializeField] private Sprite blueGem;
    [SerializeField] private Sprite purpleGem;


    [Header("BUYDIALOG")]
    [SerializeField] private StoreBuyDialogManager BuyDialog;
    


    // [SerializeField] private ;

    private Dictionary<Item, int> itemDictionary = new Dictionary<Item, int>();
    private bool once=true;
    private void OnEnable()
    {
        if (once) 
        { 
            InitializeShop();
            once=false;
        }
        

        UpdateShopUI();
    }

    private void InitializeShop()
    {

        for (int i = 0; i < items.Count && i < quantities.Count; i++)
        {
            if (!itemDictionary.ContainsKey(items[i]))
            {
                itemDictionary.Add(items[i], quantities[i]);
            }
        }
    }

    private void UpdateShopUI()
    {
       
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        List<Item> itemsToRemove = new List<Item>();

        foreach (KeyValuePair<Item, int> entry in itemDictionary)
        {

            if (entry.Value <= 0)
            {
                itemsToRemove.Add(entry.Key);
                continue;
            }

            GameObject newItem = Instantiate(itemPrefab, content.transform);

            TMP_Text itemName = newItem.transform.Find("ItemNameTMP")?.GetComponent<TMP_Text>();
            TMP_Text itemQuantity = newItem.transform.Find("ItemQuantityTMP")?.GetComponent<TMP_Text>();
            Image itemIcon = newItem.transform.Find("ItemIconIMG")?.GetComponent<Image>();
            itemIcon.preserveAspect = true;
            TMP_Text gemQuantity = newItem.transform.Find("GemQuantity")?.GetComponent<TMP_Text>();
            UnityEngine.UI.Image rarityImage = newItem.transform.Find("ItemRarityIMG")?.GetComponent<UnityEngine.UI.Image>();
            Image gemImage = newItem.transform.Find("GemIMG")?.GetComponent<Image>();

            if (itemName != null) itemName.text = entry.Key.itemName;
            if (itemQuantity != null) itemQuantity.text = $"{entry.Value}";
            if (itemIcon != null) itemIcon.sprite = entry.Key.icon;
            if (rarityImage != null) rarityImage.color = GetRarityColor(entry.Key.ItemRarity);

            string priceDetails = ItemPrices(entry.Key);

            if (gemQuantity != null) gemQuantity.text = /* ":" +*/ priceDetails.Split('_')[1];
            if (gemImage != null)
            {
                if (priceDetails.Contains("blueGems"))
                {
                    gemImage.sprite = blueGem;
                }
                else if (priceDetails.Contains("purpleGems"))
                {
                    gemImage.sprite = purpleGem;
                }
            }



            Button itemButton = newItem.GetComponent<Button>();
            if (itemButton != null)
            {
                itemButton.onClick.RemoveAllListeners();
                itemButton.onClick.AddListener(() => BuyDialog.ShowBuyDialog(entry.Key ,entry.Value ,gemImage.sprite, int.Parse(priceDetails.Split('_')[1])));
            }
        }

        foreach (Item item in itemsToRemove)
        {
            itemDictionary.Remove(item);
        }
    }

    public void BuyItem(Item item , int quantity)
    {
        if (itemDictionary.ContainsKey(item) && itemDictionary[item] > 0)
        {
            itemDictionary[item] -= quantity; 
            //Debug.Log($"Bought {item.itemName}, Remaining: {itemDictionary[item]}");
            UpdateShopUI(); 
        }
        else
        {
           // Debug.Log("Item is out of stock!");
        }
    }




    private Color GetRarityColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Uncommon:
                return new Color(164 / 255f, 164 / 255f, 164 / 255f);
            case ItemRarity.Rare:
                return new Color(61 / 255f, 123 / 255f, 164 / 255f);
            case ItemRarity.Epic:
                return new Color(76 / 255f, 36 / 255f, 79 / 255f);
            case ItemRarity.Legendary:
                return new Color(94 / 255f, 0f, 7 / 255f);
            default:
                return Color.white;
        }
    }

    private string ItemPrices(Item item) 
    {
        if( item.type == ItemType.Consumable && item.ItemRarity == ItemRarity.Uncommon) 
        {
            return "blueGems_30";
        }
        if (item.type == ItemType.Equipment && item.ItemRarity == ItemRarity.Uncommon)
        {
            return "blueGems_150";
        }
        if (item.type == ItemType.Equipment && item.ItemRarity == ItemRarity.Rare)
        {
            return "blueGems_300";
        }
        if (item.type == ItemType.Equipment && item.ItemRarity == ItemRarity.Epic)
        {
            return "purpleGems_10";
        }
        if (item.type == ItemType.Equipment && item.ItemRarity == ItemRarity.Legendary)
        {
            return "purpleGems_35";
        }
        return "";
    }

}


