using UnityEngine;

public class BossSoundsFxManager : MonoBehaviour
{
    public void AxeSwooshSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(18);
    }
    public void BossDeathSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(20);
    }
    public void BossAttackSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(19);
    }
    public void WalkStepSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(1);
    }
}
