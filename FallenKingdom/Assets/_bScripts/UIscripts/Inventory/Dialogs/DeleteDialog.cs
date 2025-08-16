using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeleteDialog : MonoBehaviour
{
    [SerializeField] private GameObject deleteDialog;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_InputField quantityInputField;
    [SerializeField] private GameObject inventoryViewport;

    

    private InventorySlot currentSlot;

   
    public void ShowDeleteDialog(InventorySlot slot)
    {
        currentSlot = slot;
        itemImage.sprite = slot.Item.icon;
        itemImage.preserveAspect = true;
        quantityInputField.text = "1"; 
        quantityInputField.characterLimit = 3; 
        quantityInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        quantityInputField.onValueChanged.AddListener(ValidateInput);
        deleteDialog.SetActive(true);
    }

 
    private void ValidateInput(string input)
    {
      


        if (int.TryParse(input, out int quantity))
        {
            quantity = Mathf.Clamp(quantity, 1, currentSlot.Quantity);
            quantityInputField.text = quantity.ToString();
        }
        else
        {
            quantityInputField.text = "1";
        }
    }

   
    public void ConfirmDelete()
    {
        int quantityToDelete = int.Parse(quantityInputField.text);
        GameManager.Instance.Player.GetInventory().RemoveItem(currentSlot.Item, quantityToDelete);
    }

    private void OnDisable()
    {
        quantityInputField.onValueChanged.RemoveListener(ValidateInput);
        inventoryViewport.SetActive(true);
    }

    private void OnEnable()
    {
        inventoryViewport.SetActive(false);
    }


}
