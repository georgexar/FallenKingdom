using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider easeHealthBarSlider;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private GameObject bossGameObject;
    private float lerpSpeed = 2f;

    private IEnemy boss;
    private float maxHealth;

    private void Start()
    {
        boss = bossGameObject.GetComponent<IEnemy>();
        if (boss != null)
        {
            InitializeHealthBar();
        }
    }

    private void Update()
    {
        if (boss != null)
        {
            HandleBossHealthBar();
            if (boss.Health <= 0f)
            {
                StartCoroutine(HideCanvasAfterDelay(2f));
            }
        }
    }

    public void UpdateMaxHealth()
    {
        if (boss == null) return;

        maxHealth = boss.Health;
        healthBarSlider.maxValue = maxHealth;
        easeHealthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        easeHealthBarSlider.value = maxHealth;
    }

    private void HandleBossHealthBar()
    {
        float currentHealth = boss.Health;
        healthBarSlider.value = currentHealth;

        if (healthBarSlider.value != easeHealthBarSlider.value)
        {
            easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value, currentHealth, lerpSpeed * Time.deltaTime);
        }
    }

    private void InitializeHealthBar()
    {
        maxHealth = boss.Health;
        healthBarSlider.maxValue = maxHealth;
        easeHealthBarSlider.maxValue = maxHealth;


        healthBarSlider.value = maxHealth;
        easeHealthBarSlider.value = maxHealth;
        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(true);
        }
    }

    private IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(false);
        }
    }
}
