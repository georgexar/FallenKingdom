using System.Linq;
using UnityEngine;

public class SpellsHideManager : MonoBehaviour
{
    [SerializeField] private GameObject hideSpellPanel;
    

    private Transform weaponPosition;

    public void Start()
    {
        weaponPosition = GameManager.Instance.Player.playerObject
           .GetComponentsInChildren<Transform>()
           .FirstOrDefault(t => t.name == "WeaponPosition");
    }
    public void Update()
    {
        if (weaponPosition != null)
        {
            bool hasWeapon = weaponPosition.childCount > 0;
            hideSpellPanel.SetActive(hasWeapon);
        }
    }


}

