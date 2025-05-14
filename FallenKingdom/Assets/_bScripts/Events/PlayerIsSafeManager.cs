using UnityEngine;

public class PlayerIsSafeManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.Instance.Player==null)return;
            StateManager.safeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Player == null) return;
            StateManager.safeZone = false;
        }
    }
}
