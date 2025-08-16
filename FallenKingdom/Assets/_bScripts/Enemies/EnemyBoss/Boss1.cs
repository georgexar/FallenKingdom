using UnityEngine;

public class Boss1 : MonoBehaviour, IEnemy
{
    [SerializeField] private float health = 600f;
    [SerializeField] private float damage = 15f;

    [SerializeField] private GameObject bossHealthBarCanvasGameObject;

    private float currentHealth;
    private float currentDamage;

    private Animator animator;
    private CharacterController characterController;

    private float lastDamageTime;
    private float damageCooldown = 0.08f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        UpdateStats();
        lastDamageTime = -damageCooldown;
    }

    public float Health => currentHealth;
    public float Damage => currentDamage;

    public void TakeDamage(int damage)
    {

        if (Time.time - lastDamageTime >= damageCooldown)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        GameManager.Instance.Player.AddKilledEnemies(gameObject.name);
        animator.SetTrigger("Die");
        characterController.enabled = false;
        //Destroy(gameObject, 2f);
    }


    public void UpdateStats()
    {
      
        float multiplier = PlayerPrefs.GetFloat("DifficultyMultiplier", 1f);
        currentHealth = health * multiplier;
        currentDamage = damage * multiplier;
        var bossHealthBarManager = bossHealthBarCanvasGameObject.GetComponent<BossHealthBarManager>();
        if (bossHealthBarManager != null)
        {
            bossHealthBarManager.UpdateMaxHealth();
        }
        //Debug.Log($"Enemy stats updated: Health = {currentHealth}, Damage = {currentDamage}");
    }
}
