using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreBuyDialogManager : MonoBehaviour
{
    [SerializeField] private GameObject buyDialog;
    [SerializeField] private GameObject notEnoughGemsDialog;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_InputField quantityInputField;
    [SerializeField] private GameObject storeViewport;
    [SerializeField] private TextMeshProUGUI priceTMP;
    [SerializeField] private Image gemIMG;

    [SerializeField] private Sprite blueGemSprite;
    [SerializeField] private Sprite PurpleGemSprite;

    [SerializeField] private StoreCanvasManager storeCanvasManager;
    private int itemPrice =0;
    private int storeQuantity;
    private Item ItemToBuy;

    private void Update()
    {
        if (quantityInputField.text == null) return;
        if (priceTMP == null) return;
        if (string.IsNullOrEmpty(quantityInputField.text))
        {
            priceTMP.text = "0";
            return;
        }

        if (int.TryParse(quantityInputField.text, out int quantity))
        {
            int price = itemPrice * quantity;
            priceTMP.text = price.ToString();
        }
        else
        {
            priceTMP.text = "0";
        }

    }

    public void ShowBuyDialog( Item item , int StoreItemQuantity , Sprite gemIMG2 , int itemPrice2 )
    {
        ItemToBuy = item;
        itemImage.sprite = item.icon;
        itemImage.preserveAspect = true;
        gemIMG.sprite = gemIMG2; 
        quantityInputField.text = "1";
        quantityInputField.characterLimit = 3;
        quantityInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        quantityInputField.onValueChanged.AddListener(ValidateInput);
        this.itemPrice = itemPrice2;
        storeQuantity = StoreItemQuantity;

        buyDialog.SetActive(true);
    }


    private void ValidateInput(string input)
    {
        if (int.TryParse(input, out int quantity))
        {
            quantity = Mathf.Clamp(quantity, 1, storeQuantity);
            quantityInputField.text = quantity.ToString();
        }
        else
        {
            quantityInputField.text = "1";
        }
    }


    public void ConfirmPurchase()
    {

        if( GameManager.Instance.Player == null)return;
        int quantityToBuy = 1;
        if (int.TryParse(quantityInputField.text, out int parsedQuantity))
        {
            quantityToBuy = parsedQuantity;
        }
        int totalPrice = itemPrice * quantityToBuy;
        if (gemIMG.sprite == blueGemSprite)
        {
          //  Debug.Log("BLUE");
            if (GameManager.Instance.Player.GetBlueGems() >= int.Parse(priceTMP.text)) 
            {
                if (!ItemToBuy.isStackable && GameManager.Instance.Player.GetInventory().GetSlot(ItemToBuy) != null) return;

                GameManager.Instance.Player.GetInventory().AddItem(ItemToBuy,int.Parse(quantityInputField.text));
                GameManager.Instance.Player.SetBlueGems(GameManager.Instance.Player.GetBlueGems() - int.Parse(priceTMP.text));
                storeCanvasManager.BuyItem(ItemToBuy, quantityToBuy);


                buyDialog.SetActive(false);
               

            }
            else 
            {
               // Debug.Log("Not enough Gems");
                notEnoughGemsDialog.SetActive(true);
               
               

            }
        }
        else //purple gems
        {
           // Debug.Log("PURPLE");
            if (GameManager.Instance.Player.GetPurpleGems() >= int.Parse(priceTMP.text))
            {
                if (!ItemToBuy.isStackable && GameManager.Instance.Player.GetInventory().GetSlot(ItemToBuy) != null) return;
                GameManager.Instance.Player.GetInventory().AddItem(ItemToBuy, int.Parse(quantityInputField.text));
                GameManager.Instance.Player.SetPurpleGems(GameManager.Instance.Player.GetPurpleGems() - int.Parse(priceTMP.text));
                storeCanvasManager.BuyItem(ItemToBuy, quantityToBuy);

                buyDialog.SetActive(false);
                
            }
            else
            {
               // Debug.Log("Not enough Gems");
                notEnoughGemsDialog.SetActive(true);
                
                
            }
        }
    }

    private void OnDisable()
    {
        if (quantityInputField != null)
        {
            quantityInputField.onValueChanged.RemoveListener(ValidateInput);
        }

        storeViewport.SetActive(true);
    }

    private void OnEnable()
    {
        storeViewport.SetActive(false);
    }

}
