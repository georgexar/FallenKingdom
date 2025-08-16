using UnityEngine;

public class PauseMenuSoundEffect : MonoBehaviour
{
    public void PlaySoundBtn()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(0);
    }
}
