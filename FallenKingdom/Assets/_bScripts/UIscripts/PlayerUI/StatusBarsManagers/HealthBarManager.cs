using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider easeHealthBarSlider;
    private float lerpSpeed = 2f;


    private void Update()
    {
        HandleHealthBar();
    }
    private void HandleHealthBar()
    {


        if (GameManager.Instance.Player != null)
        {

            healthBarSlider.value = GameManager.Instance.Player.GetHealth();

            if (healthBarSlider.value != easeHealthBarSlider.value)
            {
                easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value, GameManager.Instance.Player.GetHealth(), lerpSpeed * Time.deltaTime);
            }
        }
        

    }
}
