using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Inventory/Consumable")]
public class ConsumablesSO : Item
{
    public bool healthPot;        
    public bool energyPot;
    public bool manaPot;
    public override bool ItemFunction()
    {
        Transform potionPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "PotionPosition");
        if (healthPot)
        {
            if (GameManager.Instance.Player.GetHealth() >= 100f)
                return false; 

            GameManager.Instance.Player.playerAnimator.SetTrigger("Drink");
            if (potionPosition != null)
            {
                GameObject instantiatedObject = Instantiate(GameObject, potionPosition.position, potionPosition.rotation, potionPosition);
                Destroy(instantiatedObject, 0.7f);
            }
            GameManager.Instance.Player.SetHealth(Mathf.Min(100f, GameManager.Instance.Player.GetHealth() + 20));
            return true;
        }

       
        if (manaPot)
        {
            if (GameManager.Instance.Player.GetMana() >= 100f)
                return false;

            GameManager.Instance.Player.playerAnimator.SetTrigger("Drink");
            if (potionPosition != null)
            {
                GameObject instantiatedObject = Instantiate(GameObject, potionPosition.position, potionPosition.rotation, potionPosition);
                Destroy(instantiatedObject, 0.7f);
            }
            GameManager.Instance.Player.SetMana(Mathf.Min(100f, GameManager.Instance.Player.GetMana() + 30));
            return true;
        }

        if (energyPot)
        {
            if (GameManager.Instance.Player.GetEnergy() >= 100f)
                return false;

            GameManager.Instance.Player.playerAnimator.SetTrigger("Drink");
            if (potionPosition != null)
            {
                GameObject instantiatedObject = Instantiate(GameObject, potionPosition.position, potionPosition.rotation, potionPosition);
                Destroy(instantiatedObject, 0.7f);
            }
            GameManager.Instance.Player.SetEnergy(Mathf.Min(100f, GameManager.Instance.Player.GetEnergy() + 50));
            return true;
        }

        return false; 
    }
}
