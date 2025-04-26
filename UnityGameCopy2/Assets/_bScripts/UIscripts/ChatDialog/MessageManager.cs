using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;

 
    public void ShowMessage(string message) 
    {
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    public void HideMessage() 
    {
        messageText.text = "";
        messagePanel.SetActive(false);
    }

}
