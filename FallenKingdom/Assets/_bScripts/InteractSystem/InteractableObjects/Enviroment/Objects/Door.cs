using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Toggle Door";  
    private bool isInteractable = true;
    public string InteractionText => interactText;

    public bool IsInteractable => isInteractable;

    public bool TalkToPlayer => false;

    public void Interact()
    {
       
       
       
    }

}
