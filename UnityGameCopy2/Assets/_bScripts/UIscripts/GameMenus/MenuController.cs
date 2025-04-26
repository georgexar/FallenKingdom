using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class MenuController : MonoBehaviour
{
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;


    [SerializeField] private GameObject loadingScreenCanvas = null;
    [SerializeField] private Slider loadingSlider = null;

    private void Start()
    {
        PlayerPrefs.SetInt("isPlayerInitialized", 0);
        PlayerPrefs.Save();
        GameManager.Instance.SoundsFxManager.PlayLoopingSound(13);
    }
    private void OnDestroy()
    {
        GameManager.Instance.SoundsFxManager.StopLoopingSound(13);
    }

    public void NewGameDialogYes() 
    {
        
        SaveLoadManager.DeleteSaveFile();
        StartCoroutine(LoadSceneAsync("Game"));
    }

    public void LoadGameDialogYes() 
    {
        
        GameData loadedData = SaveLoadManager.LoadGameDataFromJSON();

        if (loadedData != null)
        {
            SaveGameManager.Instance.LoadGame();
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton() 
    {
        
        Application.Quit();
    }


    public void PlaySoundBtn() 
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(0);
    }


    private IEnumerator LoadSceneAsync(string sceneName)
    {
       
        loadingScreenCanvas.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            if (operation.progress >= 0.9f)
            {
                
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
