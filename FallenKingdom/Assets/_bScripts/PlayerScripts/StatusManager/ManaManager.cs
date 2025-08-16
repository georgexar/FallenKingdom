using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public void HandleMana()
    {
        if (GameManager.Instance.Player.GetMana() < 0f)
        {
            GameManager.Instance.Player.SetMana(0f);

            return;
        }
        if (GameManager.Instance.Player.GetMana() > 100f)
        {
            GameManager.Instance.Player.SetMana(100f);

            return;
        }

    }

}
