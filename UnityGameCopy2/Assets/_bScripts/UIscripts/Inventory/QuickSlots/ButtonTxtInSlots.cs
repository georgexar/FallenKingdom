using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonTxtInSlots : MonoBehaviour
{
    [SerializeField] InputActionReference QuickSlotAction;
    [SerializeField] private TextMeshProUGUI buttonText;
    private void Update()
    {
        buttonText.text = GameManager.Instance.InputManager.ReturnAction(QuickSlotAction);
    }
}
