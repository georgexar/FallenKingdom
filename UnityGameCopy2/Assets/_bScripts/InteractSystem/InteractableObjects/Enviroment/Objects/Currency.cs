using UnityEngine;

public class Currency : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Collect .... Gem";

    private bool isInteractable = true;
    public string InteractionText => interactText;
    public bool IsInteractable => isInteractable;
    public bool TalkToPlayer => false;

    [SerializeField] private bool bluegem;

    public void Interact()
    {
        if (isInteractable)
        {
            if (bluegem) //BLUE GEM
            {
                GameManager.Instance.Player.IncreaseBlueGems();
            }
            else
            {
                GameManager.Instance.Player.IncreasePurpleGems();
            }
            GameManager.Instance.Player.AddCollectedItem(gameObject.name);
            Destroy(gameObject);
            
        }
    }
}
