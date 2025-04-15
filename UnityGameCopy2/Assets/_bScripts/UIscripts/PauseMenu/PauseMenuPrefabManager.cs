using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenuPrefabManager : MonoBehaviour
{
    [SerializeField] private GameObject DefaultMenuImage;
    [SerializeField] private GameObject OptionsMenuImage;

    [SerializeField] private GameObject MainMenuDialog;
    [SerializeField] private GameObject DifficultyDialog;
    [SerializeField] private GameObject ControlsDialog;

   
    private Dictionary<string, bool> savedInputStates;
    void OnEnable()
    {
        Time.timeScale = 0;

        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        savedInputStates = GameManager.Instance.InputManager.GetInputsState();
        GameManager.Instance.InputManager.DisableAllExceptPause();

       
        
    }

    void OnDisable()
    {
        
        FixMenu();

       /* if (!IsAnyStoreOpen())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }*/

        

        Time.timeScale = 1;
        
        GameManager.Instance.InputManager.SetInputsState(savedInputStates);
       
    }

    private void FixMenu() 
    {
        DefaultMenuImage.SetActive(true);
        OptionsMenuImage.SetActive(false);
        MainMenuDialog.SetActive(false);
        DifficultyDialog.SetActive(false);
        ControlsDialog.SetActive(false);

       
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    bool IsAnyStoreOpen()
    {
        
        GameObject[] stores = GameObject.FindGameObjectsWithTag("Store");

        
        foreach (GameObject store in stores)
        {
            if (store.activeInHierarchy)
            {
                return true; 
            }
        }

        return false; 
    }

}
