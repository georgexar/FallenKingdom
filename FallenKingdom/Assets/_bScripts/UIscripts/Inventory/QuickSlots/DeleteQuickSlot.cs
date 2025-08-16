using System.Linq;
using UnityEngine;

public class DeleteQuickSlot : MonoBehaviour
{
    [SerializeField] private int QuickSlotToDelete;
    [SerializeField] private bool deleteSword;
    [SerializeField] private bool deleteShield;
    public void DeleteQuickSlotBtn() 
    {
        GameManager.Instance.QuickSlotManager.RemoveFromQuickSlot(QuickSlotToDelete);
        if (deleteSword) 
        {
            Transform weaponPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponPosition");
            Transform weaponInBody = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "WeaponInBody");
            foreach (Transform child in weaponPosition)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in weaponInBody)
            {
                Destroy(child.gameObject);
            }
        }
        else if (deleteShield) 
        {
            Transform shieldPosition = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldPosition");
            Transform shieldInBody = GameManager.Instance.Player.playerObject.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ShieldInBody");
            foreach (Transform child in shieldPosition)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in shieldInBody)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
