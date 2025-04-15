using TMPro;
using UnityEngine;

public class InteractUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InteractButtonContainerTMP;
    [SerializeField] private TextMeshProUGUI InteractButtonName;


    private void OnEnable()
    {
        InteractButtonContainerTMP.text = GameManager.Instance.InputManager.ConvertString(GameManager.Instance.InputManager.InteractAction.bindings[0].effectivePath).ToUpper();
        InteractButtonName.text = InteractButtonContainerTMP.text;
    }
}
