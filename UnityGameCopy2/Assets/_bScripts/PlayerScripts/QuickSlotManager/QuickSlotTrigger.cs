using UnityEngine;

public class QuickSlotTrigger : MonoBehaviour
{
    private float[] quickSlotTimers = new float[5];
    private const float cooldownDuration = 1.2f;  

    void Update()
    {
        
        for (int i = 0; i < quickSlotTimers.Length; i++)
        {
            if (quickSlotTimers[i] > 0)
            {
                quickSlotTimers[i] -= Time.deltaTime;
            }
        }
    }

    public void HandleQuickSlots()
    {
        if (GameManager.Instance.InputManager.QuickSlot1Action.WasPressedThisFrame() && StateManager.isFightingState==IsFightingState.No && StateManager.isCastingSpellState==IsCastingSpellState.No  )
        {
            HandleQuickSlot(0, 1); 
        }
        else if (GameManager.Instance.InputManager.QuickSlot2Action.WasPressedThisFrame() && StateManager.isFightingState == IsFightingState.No && StateManager.isCastingSpellState == IsCastingSpellState.No)
        {
            HandleQuickSlot(1, 2); 
        }
        else if (GameManager.Instance.InputManager.QuickSlot3Action.WasPressedThisFrame() && StateManager.isFightingState == IsFightingState.No && StateManager.isCastingSpellState == IsCastingSpellState.No)
        {
            HandleQuickSlot(2, 3); 
        }
        else if (GameManager.Instance.InputManager.QuickSlot4Action.WasPressedThisFrame() && StateManager.isFightingState == IsFightingState.No && StateManager.isCastingSpellState == IsCastingSpellState.No)
        {
            HandleQuickSlot(3, 4);
        }
        else if (GameManager.Instance.InputManager.QuickSlot5Action.WasPressedThisFrame() && StateManager.isFightingState == IsFightingState.No && StateManager.isCastingSpellState == IsCastingSpellState.No)
        {
            HandleQuickSlot(4, 5);
        }
    }

    private void HandleQuickSlot(int timerIndex, int slotIndex)
    {
        
        if (quickSlotTimers[timerIndex] <= 0)
        {
            if (GameManager.Instance.QuickSlotManager.GetQuickSlot(slotIndex) != null)
            {
                GameManager.Instance.QuickSlotManager.CastQuickSlot(slotIndex);
                quickSlotTimers[timerIndex] = cooldownDuration; 
            }
        }
       
    }


}
