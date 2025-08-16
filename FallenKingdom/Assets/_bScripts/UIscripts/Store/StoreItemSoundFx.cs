using UnityEngine;

public class StoreItemSoundFx : MonoBehaviour
{
    public void PlaySoundBtn()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(0);
    }
}
