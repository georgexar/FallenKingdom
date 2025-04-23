using UnityEngine;

public class CreditsSoundEffect : MonoBehaviour
{
    public void PlaySoundBtn()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(0);
    }
}
