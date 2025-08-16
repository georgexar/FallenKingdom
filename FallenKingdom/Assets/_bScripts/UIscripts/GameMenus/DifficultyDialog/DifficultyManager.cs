using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI difficultyText;

    [SerializeField] private Image difficultyImage;

    [SerializeField] private Sprite easySprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hardSprite;

    [SerializeField] private string DifficultyKey;
    

    private int currentDifficultyIndex = 0;
    private string[] difficultyLevels = { "easy", "normal", "hard" };
    private float[] difficultyMultipliers = { 0.7f, 1.0f, 1.3f };


    void OnEnable()
    {
        string savedDifficulty = PlayerPrefs.GetString(DifficultyKey, "normal");

        currentDifficultyIndex = System.Array.IndexOf(difficultyLevels, savedDifficulty);
        if (currentDifficultyIndex == -1)
        {
            currentDifficultyIndex = 1; // Default to Normal
        }

        UpdateDifficultyDisplay();
    }




    public void OnNextButtonClick()
    {
        
        currentDifficultyIndex++;
        if (currentDifficultyIndex >= difficultyLevels.Length)
        {
            currentDifficultyIndex = 0; // Loop back to Easy
        }
       
        UpdateDifficultyDisplay();
    }

    public void OnPreviousButtonClick()
    {
        
        currentDifficultyIndex--;
        if (currentDifficultyIndex < 0)
        {
            currentDifficultyIndex = difficultyLevels.Length - 1; // Loop back to Hard
        }
        
        UpdateDifficultyDisplay();
    }

    public void OnOkButtonClick() 
    {
        SaveDifficulty();
    }

    private void UpdateDifficultyDisplay()
    {

        difficultyText.text = difficultyLevels[currentDifficultyIndex];

        switch (difficultyText.text.ToLower())
        {
            case "easy": 
                difficultyImage.sprite = easySprite;
                break;
            case "normal": 
                difficultyImage.sprite = normalSprite;
                break;
            case "hard": 
                difficultyImage.sprite = hardSprite;
                break;
        }
    }


    private void SaveDifficulty()
    {
        PlayerPrefs.SetString(DifficultyKey, difficultyLevels[currentDifficultyIndex]);

        float multiplier = 1.0f;
        switch (difficultyLevels[currentDifficultyIndex].ToLower())
        {
            case "easy":
                multiplier = 0.7f;
                break;
            case "normal":
                multiplier = 1.0f;
                break;
            case "hard":
                multiplier = 1.3f;
                break;
        }

        PlayerPrefs.SetFloat("DifficultyMultiplier", multiplier);

        PlayerPrefs.Save();

       // Debug.Log(PlayerPrefs.GetFloat("DifficultyMultiplier").ToString());


        
        
    }




}
