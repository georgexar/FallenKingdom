using System.Linq;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    private Animator animator;
    private float attackCooldown = 2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    private bool isTakingDamage = false;
    private Collider swordCollider;

    private bool inRange = false;
  

   

    EnemyMovement enemyMovement;

    private void Start()
    {


        enemyMovement = GetComponent<EnemyMovement>();

        animator = GetComponent<Animator>();
        // Debug.Log("Animator found: " + (animator != null));

        var weaponPosition = transform
            .GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "WeaponPo");

        if (weaponPosition != null)
        {
            swordCollider = weaponPosition.GetChild(0).GetComponent<Collider>();
        }
     

    }

    public bool IsAttacking() 
    {
        return isAttacking;
    }

    private void Update()
    {
      //  Debug.Log(inRange);
        enemyMovement.IsPlayerInAttackZoneFunct(inRange);
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        if (!inRange) 
        {
            if (animator != null)
            {
                
                animator.ResetTrigger("Attack");

            }
        }


    }

    public void AttackStarted() 
    {
        if (swordCollider != null)
        {



            attackTimer = attackCooldown;
            swordCollider.enabled = true;
            swordCollider.isTrigger = true;
            

          //  Debug.Log(swordCollider.isTrigger);
        }
    }
      
    public void AttackEnd() 
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
            swordCollider.isTrigger = false;
          // Debug.Log(swordCollider.isTrigger);
        }
    }

    public void StartAttack()
    {
        if (attackTimer <= 0 && !isTakingDamage && inRange)
        {
            if (animator != null)
            {
                //  Debug.Log("Triggering attack animation.");
               // Debug.Log("Attack!!");
                animator.SetTrigger("Attack");
                
            }
            else
            {
               // Debug.LogWarning("Animator is null.");
            }
        }
        else
        {
          //  Debug.Log("Attack not started, cooldown active.");
        }
    }

    public void InRange(bool inRange) 
    {
        this.inRange = inRange;
    }
   
    public void OnTakeDamageStart() 
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
            swordCollider.isTrigger = false;
            // Debug.Log(swordCollider.isTrigger);
        }

        attackTimer = 10f;
        AttackEnd();
        if (isAttacking)
        {
            isAttacking = false;
            
        }

        isTakingDamage = true;
        
    }

    public void OnTakeDamageEnd()
    {
        isTakingDamage = false;
        attackTimer = 0.5f;
        isAttacking = false;
    }

   


    public void ContinueAttack()
    {
        if (attackTimer <= 0f && !isAttacking && inRange)
        {
            if (animator != null)
            {
               // Debug.Log("Continuing attack animation.");
                animator.SetTrigger("Attack");
            }
        }
        else
        {
           // Debug.Log("Continuing attack not possible, cooldown active.");
        }
    }
   

    public void EndAttackEnemy() 
    {
        isAttacking = false;
    }


    public void StartAttackEnemy()
    {
        isAttacking = true;
       // enemyMovement.StopRotating();
    }


    public void ResetOnWalking() 
    {
        // Reset attack flags and cooldown
        
        isAttacking = false;
        isTakingDamage = false;
       

        // Optionally disable the sword collider if it's still active
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
            swordCollider.isTrigger = false;

        }

    }


    public void OnEnemyDeath() 
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
            swordCollider.isTrigger = false;

        }
    }
}
