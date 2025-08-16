using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsCanvasManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SaveLoadManager.DeleteSaveFile();
        SceneManager.LoadScene("MainMenu");
    }
}
