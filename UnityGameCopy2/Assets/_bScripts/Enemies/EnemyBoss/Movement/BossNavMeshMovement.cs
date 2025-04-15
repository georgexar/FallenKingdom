using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossNavMeshMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
   


    private float lookRotationSpeed = 5f;

    private float defaultSpeed;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        defaultSpeed = agent.speed;

        

    }

    private void Update()
    {
        if (GameManager.Instance.Player == null) return;
        if (agent == null) return;
        
        float distance = Vector3.Distance(transform.position, GameManager.Instance.Player.playerObject.transform.position);
        float currentSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
        

        agent.isStopped = animator.applyRootMotion;


        if (animator.applyRootMotion)
        {
            return;
        }

        LookAtPlayer();
        agent.speed = defaultSpeed;
        agent.SetDestination(GameManager.Instance.Player.playerObject.transform.position);


    }


    private void LookAtPlayer()
    {
        Vector3 direction = GameManager.Instance.Player.playerObject.transform.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookRotationSpeed);
        }
    }



    

}
