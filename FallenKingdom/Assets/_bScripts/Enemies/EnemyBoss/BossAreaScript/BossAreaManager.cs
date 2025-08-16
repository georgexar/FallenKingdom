using UnityEngine;

public class BossAreaManager : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;
    private IEnemy boss;

    [Header("Boss Event Triggers")]
   
    [SerializeField] private GameObject kingsCrown;
    

    private bool triggeredFourth = false;

    private float maxHealth;
    private bool healthInitialized = false;
    private void Start()
    {
         boss = bossObject.GetComponent<IEnemy>();

        if (boss == null)
        {
            Debug.LogError("BossAreaManager: IEnemy component not found on bossObject!");
            enabled = false;
            return;
        }
        kingsCrown?.SetActive(false);
    }



    private void Update()
    {
        
        if (!healthInitialized && boss.Health > 0f)
        {
            maxHealth = boss.Health;
            healthInitialized = true;
        }

        if (!healthInitialized) return;

        float health = boss.Health;
        float healthPercent = health / maxHealth;

      
        if (!triggeredFourth && health <= 0f) 
        {
            triggeredFourth = true;
            kingsCrown.SetActive(true);
          
           
        }
    }





}
