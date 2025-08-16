using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFromCheckpoint : MonoBehaviour
{
    public void StartFromCheckpointBtn() 
    {
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
}
