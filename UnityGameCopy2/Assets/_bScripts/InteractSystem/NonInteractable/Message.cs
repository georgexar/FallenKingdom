using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private string message = "Message....";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").gameObject.activeSelf)
        {
            if (GameManager.Instance.Player == null) return;
            GameManager.Instance.Player.playerObject.transform.Find("MessageCanvas").gameObject.GetComponent<MessageManager>().ShowMessage(message);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Player == null) return;
            GameManager.Instance.Player.playerObject.transform.Find("MessageCanvas").gameObject.GetComponent<MessageManager>().HideMessage();
        }
    }

    public void HideMessageFromPlayer()
    {
        if(GameManager.Instance.Player==null)return;
        GameManager.Instance.Player.playerObject.transform.Find("MessageCanvas").gameObject.GetComponent<MessageManager>().HideMessage();
    }

}
