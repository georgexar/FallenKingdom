using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private void Update()
    {

        if (GameManager.Instance.InputManager.PauseMenuAction.WasPressedThisFrame()) 
        {

            if (GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas").gameObject.activeSelf) 
            {
                GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas").gameObject.SetActive(false);
                return;
            }

            if ( !pauseMenu.activeSelf )
            {
                pauseMenu.SetActive(true);   
            }
            else 
            {
                if (PlayerPrefs.GetInt("IsRebindingInProgress", 0) == 0)
                {
                    pauseMenu.SetActive(false); 
                }
            }

        }
    }
}

