using System.Collections.Generic;
using UnityEngine;

public class EndGameNPC : MonoBehaviour ,IInteractable
{
    public string InteractionText => interactText;
    public bool IsInteractable => isInteractable;
    public bool TalkToPlayer => talkToPlayer;

    private bool isInteractable = true;

    private bool talkToPlayer = true;


    [Header("Npc InteractText")]
    [SerializeField] private string interactText = "Talk to Marcus";

    [Header("Interaction Options")]
    [SerializeField] private bool interactOnce = true;


    [Header("Sentences for the dialog / Disable Inputs in which sentence")]
    [SerializeField] private string sentence = "Well , thankfully the story ends well. As you see , people appreciate what you did for them. You are the King now. This is where your adventure as a soldier ends. I will see you soon my King.";
    private bool disableInput=true;
   

   
    public void Interact()
    {
        if (isInteractable)
        {
            ShowDialog();
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

    private void ShowDialog()
    {
        if (disableInput)
        {
            GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").GetComponentInChildren<ChatDialogManager>().ShowDialogEndGame(sentence, true);
        }
    }
}
