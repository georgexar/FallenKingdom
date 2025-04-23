using UnityEngine;

public class InventorySoundFx : MonoBehaviour
{
    public void PlaySoundBtn()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(0);
    }
}
