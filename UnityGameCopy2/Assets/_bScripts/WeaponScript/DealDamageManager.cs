using System.Linq;
using UnityEngine;

public class DealDamageManager : MonoBehaviour
{

    private int damage; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
           
            
            Item equippedItem = GameManager.Instance.QuickSlotManager.GetQuickSlot(1).Item;
            if (equippedItem is WeaponSO weapon)
            {
                damage = weapon.damage;
            }
            else
            {
                damage = 0;
            }
            IEnemy enemy = other.GetComponent<IEnemy>();
            if (enemy != null)
            {
             //   Debug.Log("Hit enemy!");
                enemy.TakeDamage(damage);
               // Debug.Log("Dealt"+damage);
            }
        }
    }
}
