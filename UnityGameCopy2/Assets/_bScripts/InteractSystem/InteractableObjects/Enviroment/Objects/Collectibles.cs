using NUnit.Framework.Interfaces;
using UnityEngine;

public class Collectibles : MonoBehaviour , IInteractable
{
    [SerializeField] private string interactText = "Pick up .... Item";
    [SerializeField] private Item itemSO;
    
    private bool isInteractable = true;

    public string InteractionText => interactText;

    public bool IsInteractable => isInteractable;

    public bool TalkToPlayer => false;

    public void Interact()
    {   if (isInteractable && itemSO != null)
        {
            bool itemAdded = GameManager.Instance.Player.GetInventory().AddItem(itemSO, 1);
            if (itemAdded) 
            {
                GameManager.Instance.Player.AddCollectedItem(gameObject.name);
                Destroy(gameObject);
            }
        }
    }
}
