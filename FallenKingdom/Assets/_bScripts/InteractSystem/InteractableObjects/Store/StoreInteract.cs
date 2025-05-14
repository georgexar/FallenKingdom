
using UnityEngine;

public class StoreInteract : MonoBehaviour, IInteractable
{
    public string InteractionText => interactText;
    public bool IsInteractable => isInteractable;
    public bool TalkToPlayer => talkToPlayer;

    private bool isInteractable = true;

    private bool talkToPlayer = false;

    private GameObject storeCanvas;

    [Header("Npc InteractText")]
    [SerializeField] private string interactText = "Open Store";

    [Header("Interaction Options")]
    [SerializeField] private bool interactOnce = false;

    private void Start()
    {
        
        storeCanvas = transform.Find("StoreCanvas")?.gameObject;

        if (storeCanvas == null)
        {
            Debug.LogWarning("StoreCanvas not found! Make sure it is a child of the Shop GameObject.");
        }
        else
        {
            storeCanvas.SetActive(false);
        }
    }
    public void Interact()
    {
        if (isInteractable)
        {
            OpenStore();
            FixInteract();
        }
    }


    private void FixInteract()
    {
        if (interactOnce)
        {
            isInteractable = false;
        }
    }

    private void OpenStore()
    {
        if (storeCanvas == null) return;
        if (storeCanvas != null)
        {
            storeCanvas.SetActive(true);
        }
    }

}
