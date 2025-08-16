using UnityEngine;
using UnityEngine.UI;

public class ManaBarManager : MonoBehaviour
{
    [SerializeField] private Slider manaBarSlider;
    [SerializeField] private Slider easeManaBarSlider;
    private float lerpSpeed = 2f;

    private void Update()
    {
        HandleManaBar();
    }
    private void HandleManaBar()
    {


        if (GameManager.Instance.Player != null)
        {

            manaBarSlider.value= GameManager.Instance.Player.GetMana();

            if (manaBarSlider.value != easeManaBarSlider.value)
            {
                easeManaBarSlider.value = Mathf.Lerp(easeManaBarSlider.value, GameManager.Instance.Player.GetMana(), lerpSpeed * Time.deltaTime);
            }
        }
        

    }
}
