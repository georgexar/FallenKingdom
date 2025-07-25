using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatDialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private float characterDisplayDelay = 0.05f;
    private const int maxCharsPerScreen = 40;

    private int currentCharIndex = 0;


    private void OnDisable()
    {
        textMeshPro.text = "";
        currentCharIndex = 0;
    }

    public void ShowDialog(string text)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        textMeshPro.text = ""; 
        currentCharIndex = 0;
        StartCoroutine(DisplayTextGradually(text ,false,false));
    }

    public void ShowDialogAndDisableAllInputs(string text , bool disableInputs) 
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        textMeshPro.text = "";
        currentCharIndex = 0;
        GameManager.Instance.InputManager.DisableAllInputs();   
        StartCoroutine(DisplayTextGradually(text, disableInputs , false));
    }

    public void ShowDialogEndGame(string text, bool disableInputs)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        textMeshPro.text = "";
        currentCharIndex = 0;
        GameManager.Instance.InputManager.DisableAllInputs();
        StartCoroutine(DisplayTextGradually(text, disableInputs, true));
    }

    public void ShowDialogAndDisableAllInputsExceptPause(string text, bool disableInputs)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        textMeshPro.text = "";
        currentCharIndex = 0;
        GameManager.Instance.InputManager.DisableAllExceptPause();
        StartCoroutine(DisplayTextGradually(text, disableInputs,false));
    }


    private IEnumerator DisplayTextGradually(string fullText, bool enableInputs , bool endgame)
    {
     
        StringBuilder currentText = new StringBuilder(); 
        int charCountOnScreen = 0;

        while (currentCharIndex < fullText.Length)
        {
            char currentChar = fullText[currentCharIndex];
            currentText.Append(currentChar); 
            textMeshPro.text = currentText.ToString();

            charCountOnScreen++;

           
            if (currentChar == '.' || currentChar == '?' || currentChar == '!' || charCountOnScreen >= maxCharsPerScreen)
            {

                yield return new WaitForSecondsRealtime(1f);
                currentText.Clear(); 
                textMeshPro.text = ""; 
                charCountOnScreen = 0; 
            }

            currentCharIndex++;

         
            yield return new WaitForSeconds(characterDisplayDelay);
        }
        if (enableInputs && !endgame) 
        {
            GameManager.Instance.InputManager.EnableInputActions();
        }

        yield return new WaitForSecondsRealtime(1.3f);

        if (endgame)
        {
            ShowCredits();
        }

        gameObject.SetActive(false);

        

    }
    //DEN BRISKEI TO CANVAS
    private void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

}
