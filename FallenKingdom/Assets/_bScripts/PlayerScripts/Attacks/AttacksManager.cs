
using System.Linq;
using UnityEngine;

public class AttacksManager : MonoBehaviour
{
   
    private bool doNextAttack = false;

    private bool firstAttackIsDone = false;
    public void HandleAttacks()
    {
        GameManager.Instance.Player.playerAnimator.SetBool("Fighting",StateManager.isFightingState == IsFightingState.Yes );

        if (GameManager.Instance.Player.GetEnergy() > 0)
        {
            if (GameManager.Instance.QuickSlotManager.GetQuickSlot(1) != null 
                && GameManager.Instance.Player.playerController.isGrounded 
                && GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponPosition").childCount>0  
                && StateManager.getHit==false
                && StateManager.isCastingSpellState==IsCastingSpellState.No
                && StateManager.isBlocking == IsBlockingState.No)
            {
                if (GameManager.Instance.InputManager.AttackAction.WasPressedThisFrame())
                {
                    if (!firstAttackIsDone)
                    {
                      //  Debug.Log("firstattack");
                        firstAttackIsDone = true;
                        GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
                        GameManager.Instance.Player.playerAnimator.SetTrigger("Attack1");
                       
                    }
                    else
                    {
                      //  Debug.Log("donextAttack");
                        doNextAttack = true;
                    }
                }
            }
        }
    }




   

    public void OnAttackAnimationStart()
    {
        doNextAttack = false;
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.Yes;
        GameManager.Instance.Player.SetEnergy(GameManager.Instance.Player.GetEnergy() - 15f);
        StateManager.isFightingState = IsFightingState.Yes;
        //GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
    }

    private void ResetCombo()
    {
       
        firstAttackIsDone = false;
        doNextAttack = false;
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.No;
        StateManager.isFightingState = IsFightingState.No;
        GameManager.Instance.Player.playerAnimator.applyRootMotion = false;
    }

    public void AttackDisableRootMotion() 
    {
       // GameManager.Instance.Player.playerAnimator.applyRootMotion = false;
    }
   
    public void EnableDealDamage()
    {
        var weaponPosition = GameManager.Instance.Player.playerObject
            .GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "WeaponPosition");

        if (weaponPosition != null && weaponPosition.childCount > 0)
        {
            var weapon = weaponPosition.GetChild(0).gameObject;
            var collider = weapon.GetComponent<Collider>();

            if (collider != null)
            {
                collider.enabled = true;
                collider.isTrigger = true;
               
            }
        }

    }

    public void DisableDealDamage() 
    {
        
        var weaponPosition = GameManager.Instance.Player.playerObject
            .GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "WeaponPosition");

        if (weaponPosition != null && weaponPosition.childCount > 0)
        {
            var weapon = weaponPosition.GetChild(0).gameObject;
            var collider = weapon.GetComponent<Collider>();

            if (collider != null)
            {
                collider.isTrigger = false;
                collider.enabled = false;

            }
        }
    }


    public void OnAttack1AnimationEnd() 
    {
       
        StateManager.isFightingState = IsFightingState.No;
        if (doNextAttack)
        {
            GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
            GameManager.Instance.Player.playerAnimator.SetTrigger("Attack2");
        }
        else
        {
            ResetCombo(); 
        }

    }
    public void OnAttack2AnimationEnd()
    {
      
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.No;
        StateManager.isFightingState = IsFightingState.No;
        if (doNextAttack)
        {
            GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
            GameManager.Instance.Player.playerAnimator.SetTrigger("Attack3");
        }
        else
        {
            ResetCombo();
        }
    }
    public void OnAttack3AnimationEnd()
    {
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.No;
        ResetCombo();
    }


    public void DieAnimationStart()
    {
        GameManager.Instance.InputManager.DisableAllInputs();
        StateManager.isFightingState = IsFightingState.No;
        StateManager.isCastingSpellState = IsCastingSpellState.No;
        DisableDealDamage();
    }

}
