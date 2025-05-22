
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 6.5f;
    private float rotationSpeed = 10f;
    private float gravity = -15f;
    private float groundedGravity = -3f;
    private float verticalVelocity = 0f;
    private bool isChasing = false;
    private bool isRotating = true;
    private bool isPlayerInAttackZone = false;
    private bool isPlayerInChaseZone = false;

    private Vector3 velocity; 
    private Vector3 moveDirection;

    Animator animator;

    private EnemyAttackManager enemyAttackManager;

    private CharacterController characterController;
    private IEnemy enemy;

    


    private void Start()
    {

        animator = GetComponent<Animator>();
        enemyAttackManager = GetComponent<EnemyAttackManager>();
        characterController = GetComponent<CharacterController>();
        enemy = GetComponent<IEnemy>();
    }

    private void Update()
    {
        animator.SetBool("Walk", isChasing);
        ApplyGravity();
        if (enemy.Health <= 0 || GameManager.Instance.Player == null)
        {
            isChasing = false;
           // isRotating = false;
            return;
        }
        if (GameManager.Instance.Player.playerController == null || GameManager.Instance.Player.GetHealth() <= 0f)
        {
            isChasing = false;
            //isRotating = false;
            return;
        }

        if (GameManager.Instance.Player.GetPlayerIsSafe())
        {
            isChasing = false;
            return;
        }
        if (!isPlayerInChaseZone || (isPlayerInAttackZone && enemyAttackManager != null && !enemyAttackManager.IsAttacking()))
        {
            isChasing = false;
            return;
        }
        if (!isChasing && isPlayerInChaseZone)
        {
            StartChasing();
        }

        if(isPlayerInChaseZone && enemyAttackManager != null && !enemyAttackManager.IsAttacking()) 
        {
            RotateTowardsPlayer();
        }

        if (isChasing && enemyAttackManager != null && !enemyAttackManager.IsAttacking())
        {
            MoveTowardsPlayer();
            
        }


      

    }

    public void StartChasing()
    {

        isChasing = true;
    }

    public void IsPlayerIn(bool flag)
    {

        isPlayerInChaseZone = flag;
    }
   
    public void IsPlayerInAttackZoneFunct(bool flag) 
    {
        isPlayerInAttackZone = flag;
    }

    public void StopChasing()
    {
        if (GameManager.Instance.Player == null)return; 
        
        isChasing = false;
    }
    public void StopRotating()
    {
        isRotating = false;
    }
    public void StartRotating()
    {

        isRotating = true;
    }

    private void RotateTowardsPlayer()
    {
        if (!isRotating) return;

        Vector3 direction = GameManager.Instance.Player.playerObject.transform.position - transform.position;


        direction.y = 0f;


        Quaternion targetRotation = Quaternion.LookRotation(direction);


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void MoveTowardsPlayer()
    {

        Vector3 direction = GameManager.Instance.Player.playerObject.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 1f) { direction.Normalize(); }

        moveDirection = direction * speed;
        characterController.Move(moveDirection * Time.deltaTime);
    }


    private void ApplyGravity()
    {
        bool isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            verticalVelocity = groundedGravity;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);

    }

}