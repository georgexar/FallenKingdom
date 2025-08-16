

public interface IEnemy
{
    void TakeDamage(int damage);
    void Die();
    float Health { get; }
    float Damage { get; }

    void UpdateStats();
}
