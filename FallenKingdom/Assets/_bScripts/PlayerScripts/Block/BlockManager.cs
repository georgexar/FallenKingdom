using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public void HandleBlocking()
    {


        GameManager.Instance.Player.playerAnimator.SetBool("Blocking", StateManager.isBlocking == IsBlockingState.Yes);

        if (GameManager.Instance.Player.GetEnergy() > 0
            && GameManager.Instance.QuickSlotManager.GetQuickSlot(2) != null
            && GameManager.Instance.Player.playerController.isGrounded
            && GameManager.Instance.InputManager.BlockAction.IsPressed()
            && StateManager.isFightingState == IsFightingState.No
            && StateManager.isCastingSpellState == IsCastingSpellState.No
            && GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldPosition").childCount > 0)
        {
            StateManager.isBlocking = IsBlockingState.Yes;

        }
        else
        {
            StateManager.isBlocking = IsBlockingState.No;
        }


    }
}
