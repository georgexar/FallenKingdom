using UnityEngine;
using UnityEngine.UI;
public class EnergyBarManager : MonoBehaviour
{
    [SerializeField] private Slider energyBarSlider;
    [SerializeField] private Slider easeEnergyBarSlider;
    private float lerpSpeed = 5f;


    void Start()
    {
    }

    private void Update()
    {
        HandleEnergyBar();
    }

    private void HandleEnergyBar()
    {
        if (GameManager.Instance.Player != null)
        {
            energyBarSlider.value = GameManager.Instance.Player.GetEnergy();
            if (energyBarSlider.value != easeEnergyBarSlider.value)
            {
                easeEnergyBarSlider.value = Mathf.Lerp(easeEnergyBarSlider.value, GameManager.Instance.Player.GetEnergy(), lerpSpeed * Time.deltaTime);
            }
           
        }
       

    }
}
