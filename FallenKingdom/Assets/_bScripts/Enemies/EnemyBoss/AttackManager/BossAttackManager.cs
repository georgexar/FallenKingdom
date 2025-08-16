using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BossAttackManager : MonoBehaviour
{
    private Animator animator;
    private IEnemy boss;
    private float bossMaxHealth;
    private BossNavMeshMovement bossMovement;
    private NavMeshAgent agent;

    private Collider axeCollider;

    private bool isAttacking = false;




    // === Cooldown Variables === PHASE 1
    private float lastP1A1Time = -Mathf.Infinity;
    private float cooldownP1A1 = 3f;

    private float lastP1A2Time = -Mathf.Infinity;
    private float cooldownP1A2 = 6f;

    private float lastP1A3Time = -Mathf.Infinity;
    private float cooldownP1A3 = 2f;


    // === Cooldown Variables === PHASE 2
    private float lastP2A1Time = -Mathf.Infinity;
    private float cooldownP2A1 = 4f;

    private float lastP2A2Time = -Mathf.Infinity;
    private float cooldownP2A2 = 9f;

    private float lastP2A3Time = -Mathf.Infinity;
    private float cooldownP2A3 = 2f;




    private float waitForNextAttackTimer = 2f;
    private float lastAttackSwitchTime = -Mathf.Infinity;


    private bool phase2SoundPlayed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<IEnemy>();
        agent = GetComponent<NavMeshAgent>();
        bossMovement = GetComponent<BossNavMeshMovement>();

       
        bossMaxHealth = boss.Health;


        var axePosition = transform
            .GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "AxePosition");

        if (axePosition != null)
        {
            axeCollider = axePosition.GetChild(0).GetComponent<Collider>();
        }
    }

  
    void Update()
    {
        if (GameManager.Instance.Player == null) return;
        


        float distance = Vector3.Distance(transform.position, GameManager.Instance.Player.playerObject.transform.position);

        
        if (boss.Health > bossMaxHealth * 0.5f) //PHASE 1
        {
            
            bool P1A1range = (distance <= agent.stoppingDistance && distance >= agent.stoppingDistance * 0.7);
            bool P1A1cooldownPassed = (Time.time >= lastP1A1Time + cooldownP1A1);

            bool P1A2range = (distance <= 7 && distance > agent.stoppingDistance);
            bool P1A2cooldownPassed = (Time.time >= lastP1A2Time + cooldownP1A2);

            bool P1A3range = (distance >= 0 && distance < agent.stoppingDistance * 0.7);
            bool P1A3cooldownPassed = (Time.time >= lastP1A3Time + cooldownP1A3);


            if (P1A2range && !isAttacking && P1A2cooldownPassed && Time.time >= lastAttackSwitchTime + waitForNextAttackTimer)
            {
                TriggerP1A2();
            }


            if (P1A1range && !isAttacking && P1A1cooldownPassed && Time.time >= lastAttackSwitchTime + waitForNextAttackTimer)
            {
                TriggerP1A1();
            }

            if (P1A3range && !isAttacking && P1A3cooldownPassed && Time.time >= lastAttackSwitchTime + waitForNextAttackTimer)
            {
                TriggerP1A3();
            }
        }
        else // PHASE 2
        {
            if (!phase2SoundPlayed)
            {
                GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(20);
                phase2SoundPlayed = true;
            }
            bool P2A1range = distance <= 7f;
            bool P2A1cooldownPassed = (Time.time >= lastP2A1Time + cooldownP2A1);

            bool P2A2range = distance <= 7f;
            bool P2A2cooldownPassed = (Time.time >= lastP2A2Time + cooldownP2A2);

            bool P2A3range = (distance <= agent.stoppingDistance && distance >= 0);
            bool P2A3cooldownPassed = (Time.time >= lastP2A3Time + cooldownP2A3);


            if (P2A2range && !isAttacking && P2A2cooldownPassed && Time.time >= lastAttackSwitchTime + waitForNextAttackTimer)
            {
                TriggerP2A2();
            }


            if (P2A1range && !isAttacking && P2A1cooldownPassed && Time.time >= lastAttackSwitchTime + waitForNextAttackTimer)
            {
                TriggerP2A1();
            }

            if (P2A3range && !isAttacking && P2A3cooldownPassed && Time.time >= lastAttackSwitchTime + waitForNextAttackTimer)
            {
                TriggerP2A3();
            }



        }

    }

    private void TriggerP1A1()
    {
        animator.applyRootMotion = true;
        isAttacking = true;
        animator.SetTrigger("P1A1");
    }

    public void OnP1A1End()
    {
        lastP1A1Time = Time.time;
        waitForNextAttackTimer = 1.5f;
        lastAttackSwitchTime = Time.time;
        isAttacking = false;
        animator.applyRootMotion = false;
    }

    private void TriggerP1A3()
    {

        animator.applyRootMotion = true;
        isAttacking = true;
        animator.SetTrigger("P1A3");
    }
    public void OnP1A3End()
    {
        lastP1A3Time = Time.time;
        waitForNextAttackTimer = 1.5f;
        lastAttackSwitchTime = Time.time;
        isAttacking = false;
        animator.applyRootMotion = false;
    }

    private void TriggerP1A2()
    {

        animator.applyRootMotion = true;
        isAttacking = true;
        animator.SetTrigger("P1A2");
    }
    public void OnP1A2End()
    {
        lastP1A2Time = Time.time;
        waitForNextAttackTimer = 1.5f;
        lastAttackSwitchTime = Time.time;
        isAttacking = false;
        animator.applyRootMotion = false;
    }


    private void TriggerP2A1()
    {
        animator.applyRootMotion = true;
        isAttacking = true;
        animator.SetTrigger("P2A1");
    }

    public void OnP2A1End()
    {
        lastP2A1Time = Time.time;
        waitForNextAttackTimer = 1.5f;
        lastAttackSwitchTime = Time.time;
        isAttacking = false;
        animator.applyRootMotion = false;
    }

    private void TriggerP2A3()
    {

        animator.applyRootMotion = true;
        isAttacking = true;
        animator.SetTrigger("P2A3");
    }
    public void OnP2A3End()
    {
        lastP2A3Time = Time.time;
        waitForNextAttackTimer = 1.5f;
        lastAttackSwitchTime = Time.time;
        isAttacking = false;
        animator.applyRootMotion = false;
    }

    private void TriggerP2A2()
    {

        animator.applyRootMotion = true;
        isAttacking = true;
        animator.SetTrigger("P2A2");
    }
    public void OnP2A2End()
    {
        lastP2A2Time = Time.time;
        waitForNextAttackTimer = 1.5f;
        lastAttackSwitchTime = Time.time;
        isAttacking = false;
        animator.applyRootMotion = false;
    }






    public void OnDealDamageStart() 
    {
        if (axeCollider != null)
        {
            axeCollider.enabled = true;
            axeCollider.isTrigger = true;
        }
    }

    public void OnDealDamageEnd() 
    {
        if (axeCollider != null)
        {
            axeCollider.enabled = false;
            axeCollider.isTrigger = false;
        }
    }


    public void OnBossDeathAnimation() 
    {
        if (axeCollider != null)
        {
            axeCollider.enabled = false;
            axeCollider.isTrigger = false;
        }
    }
}
