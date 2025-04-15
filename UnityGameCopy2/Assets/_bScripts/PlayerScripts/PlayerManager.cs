using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

    [SerializeField] private GameObject inventory;

  

    void Update()
    {
        if(GameManager.Instance.Player == null) return;
        
        GameManager.Instance.Player.playerObject.GetComponent<HealthManager>().HandleHealth();
        GameManager.Instance.Player.playerObject.GetComponent<ManaManager>().HandleMana();
        GameManager.Instance.Player.playerObject.GetComponent<EnergyManager>().HandleEnergy();
        GameManager.Instance.Player.playerObject.GetComponent<MovementManager>().HandleAllMovement();
        GameManager.Instance.Player.playerObject.GetComponent<AttacksManager>().HandleAttacks();
        GameManager.Instance.Player.playerObject.GetComponent<BlockManager>().HandleBlocking();
        GameManager.Instance.Player.playerObject.GetComponent<AnimationManager>().HandleAllAnimations();
        GameManager.Instance.Player.playerObject.GetComponent<QuickSlotTrigger>().HandleQuickSlots();
        GameManager.Instance.Player.playerObject.GetComponent<SpellTriggerManager>().HandleAllSpells();

        if (GameManager.Instance.InputManager.InventoryAction.WasPressedThisFrame()) 
        {
            inventory.SetActive(!inventory.activeSelf);
        }



        
        //
        //($"Current State: {StateManager.playerCurrentState}");
    }
}
