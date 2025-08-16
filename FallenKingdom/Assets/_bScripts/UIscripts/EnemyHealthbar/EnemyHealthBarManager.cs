using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider easeHealthBarSlider;
    [SerializeField] private float maxVisibleDistance = 20f;
    private float lerpSpeed = 2f;

    private IEnemy enemy;
    private float maxHealth;


    private GameObject healthBarPanel;

    private void LateUpdate()
    {

        Transform cameraTransform = Camera.main.transform;
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0;
        transform.forward = -direction.normalized;
    }

    private void Start()
    {
        healthBarPanel = transform.Find("HealthBarPanel").gameObject;
        enemy = GetComponentInParent<IEnemy>();
        if (enemy != null)
        {
            InitializeHealthBar();
        }
    }

    private void Update()
    {
        if (enemy != null)
        {
            HandleHealthBar();
            HandleVisibility();
        }
    }

    public void UpdateMaxHealth()
    {
        if (enemy == null) return;

        maxHealth = enemy.Health;
        healthBarSlider.maxValue = maxHealth;
        easeHealthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        easeHealthBarSlider.value = maxHealth;
    }

    private void HandleHealthBar()
    {
        float currentHealth = enemy.Health;
        healthBarSlider.value = currentHealth;

        if (healthBarSlider.value != easeHealthBarSlider.value)
        {
            easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value, currentHealth, lerpSpeed * Time.deltaTime);
        }
    }

    private void HandleVisibility()
    {
        if (GameManager.Instance.Player == null) return;
        float distance = Vector3.Distance(GameManager.Instance.Player.playerObject.transform.position, transform.position);
        healthBarPanel.SetActive(distance <= maxVisibleDistance);
    }
    private void InitializeHealthBar()
    {
        maxHealth = enemy.Health;
        healthBarSlider.maxValue = maxHealth;
        easeHealthBarSlider.maxValue = maxHealth;


        healthBarSlider.value = maxHealth;
        easeHealthBarSlider.value = maxHealth;
    }
}
