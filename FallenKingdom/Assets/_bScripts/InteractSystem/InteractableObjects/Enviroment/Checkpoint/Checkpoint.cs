using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Rest Here";

    private bool isInteractable = true;
    public string InteractionText => interactText;

    public bool IsInteractable => isInteractable;

    public bool TalkToPlayer => false;



    public void Interact()
    {
        StateManager.playerCurrentState = MovementState.SitDown;


        GameObject portal = FindInactiveObjectByName("TransferToSecondKingdom");

        if (portal != null)
        {
            if (portal.activeSelf)
            {

               // PlayerPrefs.SetInt("PortalActivated", 1);
               // PlayerPrefs.Save();
            }

        }

        SaveGameManager.Instance.SaveGame();


    }
    private GameObject FindInactiveObjectByName(string objectName)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == objectName)
            {
                return obj;
            }
        }
        return null;
    }

}