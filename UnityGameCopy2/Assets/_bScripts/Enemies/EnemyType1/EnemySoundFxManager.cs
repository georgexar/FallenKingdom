using UnityEngine;

public class EnemySoundFxManager : MonoBehaviour
{
    public void TakeDamageSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(7);
    }

    public void WalkStepSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(1);
    }

    public void DieSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(16);
    }
    public void Attack1SoundStart()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(3);
    }
}
