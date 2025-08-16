using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject dieDialog;
    public void HandleHealth()
    {
        if (StateManager.playerDeadState == PlayerDeadState.Dead) return;

        if (GameManager.Instance.Player.GetHealth() <= 0f) 
        {
            StateManager.playerDeadState = PlayerDeadState.Dead;
            HandleDeath();

        }
        else 
        {
            StateManager.playerDeadState = PlayerDeadState.Alive;
        }

        if (GameManager.Instance.Player.GetHealth() > 100f)
        {
            GameManager.Instance.Player.SetHealth(100f);
            
            return;
        }

    }

    private void HandleDeath() 
    {
        if (GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas").gameObject.activeSelf)
        {
            GameManager.Instance.Player.playerObject.transform.Find("InventoryCanvas").gameObject.SetActive(false);
        }

        GameManager.Instance.InputManager.DisableAllInputs();

        GameManager.Instance.Player.playerObject.tag = "Untagged";


       // if (GameManager.Instance.Player.playerController != null)
       // {
       //     GameManager.Instance.Player.playerController.enabled = false;
       // }

        dieDialog.SetActive(true);

        

        GameManager.Instance.Player.playerAnimator.SetTrigger("Die");
        StartCoroutine(RespawnPlayerAfterDelay(4f));
    }

    IEnumerator RespawnPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject existingPlayer = GameObject.Find("Player");
        if (existingPlayer != null)
        {
            Destroy(existingPlayer);
            GameManager.Instance.Player = null;
        }
        StateManager.playerDeadState = PlayerDeadState.Alive;
        GameData loadedData = SaveLoadManager.LoadGameDataFromJSON();

        if (loadedData != null)
        {
            SaveGameManager.Instance.LoadGame();
        }
        else
        {
            SaveLoadManager.DeleteSaveFile();
            SceneManager.LoadScene("Game");
        }
    }


    public void GetHitAnimationEventStart() 
    {
        StateManager.getHit = true;
    }
    public void GetHitAnimationEventEnd()
    {
        StateManager.getHit = false;
    }

   
    
}
