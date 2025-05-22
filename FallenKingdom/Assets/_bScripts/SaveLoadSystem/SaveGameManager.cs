using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }


    private GameObject loadingScreenCanvas;
    private Slider loadingSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveGame()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        GameData gameData = new GameData(GameManager.Instance.Player, currentSceneName);

        SaveLoadManager.SaveGameData(gameData);
        SaveLoadManager.SaveInventory();
        SaveLoadManager.SaveQuickSlots();
        SaveLoadManager.SaveCollectedItems();
        SaveLoadManager.SaveKilledEnemies();
       
        List<Npc> allNpcs = FindObjectsByType<Npc>(FindObjectsSortMode.None).ToList();
        SaveLoadManager.SaveNpcState(allNpcs);
    }

    public void LoadGame() 
    {
        GameData loadedData = SaveLoadManager.LoadGameDataFromJSON();
      
        if (loadedData != null)
        {
            StartCoroutine(LoadSceneAsync(loadedData.loadedCurrentScene));
           
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        FindLoadingScreenElements();

        if (loadingScreenCanvas != null)
        {
            loadingScreenCanvas.SetActive(true);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (loadingSlider != null)
            {
                loadingSlider.value = progress;
            }

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void FindLoadingScreenElements()
    {
        loadingScreenCanvas = GameObject.Find("LoadingScreenCanvas");

        if (loadingScreenCanvas == null)
        {
            //Debug.LogWarning("LoadingScreenCanvas not found! Searching in all objects...");
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "LoadingScreenCanvas")
                {
                    loadingScreenCanvas = obj;
                    break;
                }
            }
        }

        if (loadingScreenCanvas != null)
        {
            //Debug.Log("LoadingScreenCanvas found!");
            loadingSlider = loadingScreenCanvas.GetComponentInChildren<Slider>(true);
        }
       
    }



}
