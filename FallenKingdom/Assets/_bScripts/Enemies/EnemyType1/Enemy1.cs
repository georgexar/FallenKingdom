using UnityEngine;

public class Enemy1 : MonoBehaviour, IEnemy
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float damage = 10f;

    private float currentHealth;
    private float currentDamage;

    private Animator animator;

    private float lastDamageTime;
    private float damageCooldown = 0.08f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        UpdateStats();
        lastDamageTime = -damageCooldown;
    }

    public float Health => currentHealth ;
    public float Damage => currentDamage;

    public void TakeDamage(int damage)
    {

        if (Time.time - lastDamageTime >= damageCooldown)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;

            if (currentHealth > 0)
            {
                animator.SetTrigger("takeDamage");
            }
            else
            {
                Die();
            }
        }
    }

    public void Die()
    {
        GameManager.Instance.Player.AddKilledEnemies(gameObject.name);
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }


    public void UpdateStats()
    {
        float multiplier = PlayerPrefs.GetFloat("DifficultyMultiplier", 1f);
        currentHealth = health * multiplier;
        currentDamage = damage * multiplier;

        var healthBarManager = GetComponentInChildren<EnemyHealthBarManager>();
        if (healthBarManager != null)
        {
            healthBarManager.UpdateMaxHealth();
        }

        //Debug.Log($"Enemy stats updated: Health = {currentHealth}, Damage = {currentDamage}");
    }
}
