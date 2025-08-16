using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellsButtonsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI spellBtnTMP;
    [SerializeField] private InputActionReference spellAction;
    public void Update()
    {
        spellBtnTMP.text = GameManager.Instance.InputManager.ReturnAction(spellAction);
    }
}
