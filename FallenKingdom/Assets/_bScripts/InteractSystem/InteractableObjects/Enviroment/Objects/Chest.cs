using UnityEngine;

public class Chest : MonoBehaviour , IInteractable
{
    [SerializeField] private string interactText = "Open Chest";

    private bool isInteractable = true;

    private bool talkToPlayer = false;
    public string InteractionText => interactText;
    public bool IsInteractable => isInteractable;
    public bool TalkToPlayer => talkToPlayer;

    private Animator chestAnimator;

    private void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }
    public void Interact()
    {
        if (isInteractable)
        {
            //PLAY ANIMATION FOR CHEST TOO
            GameManager.Instance.Player.playerAnimator.SetTrigger("Open");
            chestAnimator.SetTrigger("TriggerChest");
            isInteractable =false;
        }
    }

    private void ShowDialog()
    {
       
    }
}
