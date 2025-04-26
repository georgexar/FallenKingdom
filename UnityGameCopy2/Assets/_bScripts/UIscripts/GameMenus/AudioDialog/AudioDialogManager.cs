using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioDialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderValue;
    [SerializeField] private Slider slider;
    [SerializeField] private Button okBtn;

    private string SoundVolumeKey = "soundVolume";

    private void Start()
    {
       
     
        slider.onValueChanged.AddListener(UpdateSliderValueText);

        okBtn.onClick.AddListener(SaveSoundVolume);
    }

    private void OnEnable()
    {
        float savedVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 1.0f);
        slider.value = savedVolume;

        UpdateSliderValueText(savedVolume);
    }

    private void UpdateSliderValueText(float value)
    {
        sliderValue.text = value.ToString("F2");
    }

    private void SaveSoundVolume()
    {
        float currentVolume = slider.value;
        PlayerPrefs.SetFloat(SoundVolumeKey, currentVolume);
        PlayerPrefs.Save();
        

        GameManager.Instance.SoundsFxManager.UpdateAllVolumes();

    }

    private void OnDestroy()
    {
       
        slider.onValueChanged.RemoveListener(UpdateSliderValueText);
        okBtn.onClick.RemoveListener(SaveSoundVolume);
    }

}
