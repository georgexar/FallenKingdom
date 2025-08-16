using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{

    void Update()
    {
        if (CursorShown())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    private bool CursorShown()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "CreditsScene")
        {
            return true;
        }

        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "BossFight")
        {
            GameObject pauseMenu = GameObject.Find("PauseMenuCanvas");

            if (pauseMenu != null && pauseMenu.activeSelf)
            {
                return true;
            }
            if (IsAnyStoreOpen())
            {
                return true;
            }
        }

        if (GameManager.Instance.Player != null)
        {

            if (GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas") != null && GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas").gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
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