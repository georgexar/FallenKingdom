using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private EnemyAttackManager enemyAttackManager;
    private EnemyMovement enemyMovement;
    private void Start()
    {
        enemyAttackManager = transform.parent.GetComponent<EnemyAttackManager>();
        //  Debug.Log("EnemyAttackManager found: " + (enemyAttackManager != null));
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.Player.GetPlayerIsSafe())
        {
            if (enemyMovement != null)
            {
                enemyMovement.StopChasing();
            }

          //  Debug.Log("Player entered attack trigger.");
            if (enemyAttackManager != null)
            {
             //   Debug.Log("Starting attack.");
                enemyAttackManager.InRange(true);
                enemyAttackManager.StartAttack();
            }
            else
            {
               // Debug.LogWarning("EnemyAttackManager is null.");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.Player.GetPlayerIsSafe())
        {
          //  Debug.Log("Player staying in attack trigger.");
            if (enemyAttackManager != null)
            {
            //    Debug.Log("Continuing attack.");
                enemyAttackManager.ContinueAttack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyAttackManager != null) enemyAttackManager.InRange(false);
            if (enemyMovement != null && !GameManager.Instance.Player.GetPlayerIsSafe())
            {
                enemyMovement.StartChasing();
            }
        }
    }
}
