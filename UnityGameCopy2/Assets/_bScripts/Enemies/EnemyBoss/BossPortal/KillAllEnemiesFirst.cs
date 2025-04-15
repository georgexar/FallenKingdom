using System.Collections.Generic;
using UnityEngine;

public class KillAllEnemiesFirst : MonoBehaviour
{
    [SerializeField] private GameObject portalObject;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private float checkInterval = 1f;

    private float nextCheckTime = 0f;

    void Start()
    {
        if (portalObject != null)
        {
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
                messageObject.GetComponent<Message>().HideMessageFromPlayer();
                messageObject.SetActive(false);

            }
            else 
            {
                portalObject.SetActive(false);
                messageObject.SetActive(true);
            }
        }
    }

    void ActivatePortal()
    {
        if (portalObject != null && !portalObject.activeSelf)
        {
            portalObject.SetActive(true);
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
