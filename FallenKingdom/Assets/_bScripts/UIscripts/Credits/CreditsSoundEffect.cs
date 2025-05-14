using UnityEngine;

public class CreditsSoundEffect : MonoBehaviour
{
    public void PlaySoundBtn()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(0);
    }

    private void Start()
    {
        SoundsFxManager.Instance.PlayLoopingSound(21);
    }

    private void OnDestroy()
    {
        SoundsFxManager.Instance.StopLoopingSound(21);
    }
}
