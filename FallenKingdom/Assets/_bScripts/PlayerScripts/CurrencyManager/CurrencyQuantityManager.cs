using TMPro;
using UnityEngine;

public class CurrencyQuantityManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blueGemsQuantity;
    [SerializeField] private TextMeshProUGUI purpleGemsQuantity;
    private void Update()
    {
        if (GameManager.Instance.Player == null) return;
        blueGemsQuantity.text = " " + GameManager.Instance.Player.GetBlueGems();
        purpleGemsQuantity.text = " " + GameManager.Instance.Player.GetPurpleGems();
    }
}
