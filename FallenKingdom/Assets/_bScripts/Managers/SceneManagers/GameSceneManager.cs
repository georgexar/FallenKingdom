using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SoundsFxManager.PlayBackgroundLoopingSound(22);
    }
    private void OnDestroy()
    {
        GameManager.Instance.SoundsFxManager.StopBackgroundLoopingSound(22);
    }
}
