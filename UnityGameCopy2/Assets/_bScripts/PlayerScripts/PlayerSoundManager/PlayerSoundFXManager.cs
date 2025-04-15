using UnityEngine;

public class PlayerSoundFXManager : MonoBehaviour
{
    public void WalkStepSound() 
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(1);
    }
    public void RunStepSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(2);
    }
    public void Attack1SoundStart() 
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(14);
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(3);
    }
    public void Attack2SoundStart()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(4);
    }
    public void Attack3SoundStart()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(6);
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(5);
    }

    public void TakeDamageSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(7);
    }
    public void DieSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(8);
    }

    public void EquipSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(9);
    }
    public void UnequipSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(10);
    }
    public void EquipShieldSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(15);
    }
    
    public void DrinkSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(11);
    }
    public void JumpUpSound()
    {
        GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(12);
    }
}
