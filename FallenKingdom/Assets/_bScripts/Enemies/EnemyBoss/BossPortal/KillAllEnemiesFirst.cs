
using UnityEngine;

public class KillAllEnemiesFirst : MonoBehaviour
{
    [SerializeField] private GameObject portalObject;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private float checkInterval = 1f;

    private int interactableLayer;
    private int defaultLayer;

    private float nextCheckTime = 0f;

    void Start()
    {

        interactableLayer = LayerMask.NameToLayer("Interactable");
        defaultLayer = LayerMask.NameToLayer("Default");


        if (portalObject != null)
        {
            portalObject.layer = defaultLayer;
            portalObject.SetActive(false);
        }
        if (messageObject != null)
        {
            messageObject.SetActive(true);
        }
    }

    void Update()
    {
        
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;

            if (AllEnemiesAreDead())
            {
                ActivatePortal();
                if (messageObject != null)
                {
                    messageObject.GetComponent<Message>().HideMessageFromPlayer();
                    messageObject.SetActive(false);
                }

            }
            else 
            {
                DeactivatePortal();
                if (messageObject != null)
                {
                    messageObject.SetActive(true);
                }
            }
        }
    }

    void ActivatePortal()
    {
        if (portalObject != null && !portalObject.activeSelf)
        {
            portalObject.SetActive(true);
            portalObject.layer = interactableLayer;
        }
    }

    void DeactivatePortal()
    {
        if (portalObject != null && portalObject.activeSelf)
        {
            portalObject.layer = defaultLayer;
            portalObject.SetActive(false);
        }
    }

    private bool AllEnemiesAreDead()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                return false;
            }
        }
        return true;
    }
}
