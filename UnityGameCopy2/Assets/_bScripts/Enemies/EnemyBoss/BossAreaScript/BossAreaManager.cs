using UnityEngine;

public class BossAreaManager : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;
    private IEnemy boss;

    [Header("Boss Event Triggers")]
    [SerializeField] private GameObject firstEvent;   
    [SerializeField] private GameObject secondEvent;
    [SerializeField] private GameObject thirdEvent;
    [SerializeField] private GameObject kingsCrown;
    [SerializeField] private GameObject returnBack;

    private bool triggeredFirst = false;
    private bool triggeredSecond = false;
    private bool triggeredThird = false;
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

        firstEvent?.SetActive(false);
        secondEvent?.SetActive(false);
        thirdEvent?.SetActive(false);
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

        if (!triggeredFirst && healthPercent <= 0.75f)
        {
            triggeredFirst = true;
            firstEvent?.SetActive(true);
        }

        if (!triggeredSecond && healthPercent <= 0.5f)
        {
            triggeredSecond = true;
            secondEvent?.SetActive(true);
        }

        if (!triggeredThird && healthPercent <= 0.25f)
        {
            triggeredThird = true;
            thirdEvent?.SetActive(true);
        }

        if (!triggeredFourth && health <= 0f) 
        {
            triggeredFourth = true;
            kingsCrown.SetActive(true);
            returnBack.SetActive(true);
           
        }
    }





}
