using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    

    private void Start()
    {
       
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Player.GetHealth() > 0f && !GameManager.Instance.Player.GetPlayerIsSafe()) 
            {
                enemyMovement.IsPlayerIn(true);
                enemyMovement.StartChasing(); 
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyMovement.IsPlayerIn(false);
            enemyMovement.StopChasing();
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyMovement.IsPlayerIn(true);
        }
    }

}
