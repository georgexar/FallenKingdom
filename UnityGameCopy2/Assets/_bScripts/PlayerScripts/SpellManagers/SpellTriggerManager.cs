using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellTriggerManager : MonoBehaviour
{
    [SerializeField] private GameObject spellsPanel;

    [SerializeField] private Image spell1CooldownImage;
    [SerializeField] private Image spell2CooldownImage;
    [SerializeField] private Image spell3CooldownImage;


    private Dictionary<string, float> spellTimers = new Dictionary<string, float>();
    private Dictionary<string, float> spellCooldowns = new Dictionary<string, float>
    {
        { "Spell1", 10f },
        { "Spell2", 40f },
        { "Spell3", 15f }
    };

    private Dictionary<string, float> spellManaCosts = new Dictionary<string, float>
    {
        { "Spell1", 20f },
        { "Spell3", 40f }
    };

    private void Start()
    {
        foreach (var spell in spellCooldowns.Keys)
        {
            spellTimers[spell] = 0f;
        }
    }

    public void HandleAllSpells() 
    {
        HandleSpells();
        UpdateUI();
    }
    private void HandleSpells() 
    {

        GameManager.Instance.Player.playerAnimator.SetBool("CastingSpell", StateManager.isCastingSpellState == IsCastingSpellState.Yes);

        List<string> keys = new List<string>(spellTimers.Keys);
        foreach (string key in keys)
        {
            if (spellTimers[key] > 0f)
                spellTimers[key] -= Time.deltaTime;

            if (spellTimers[key] < 0f)
                spellTimers[key] = 0f;
        }

        if (!spellsPanel.activeSelf || StateManager.isFightingState==IsFightingState.Yes || StateManager.isCastingSpellState==IsCastingSpellState.Yes)return;

        


        if (GameManager.Instance.InputManager.Spell1Action.WasPressedThisFrame() && spellTimers["Spell1"] <= 0f &&
             GameManager.Instance.Player.GetMana() >= spellManaCosts["Spell1"])
        {
            CastSpell("Spell1");
        }

        if (GameManager.Instance.InputManager.Spell2Action.WasPressedThisFrame() && spellTimers["Spell2"] <= 0f)
        {
            CastSpell("Spell2");
        }

        if (GameManager.Instance.InputManager.Spell3Action.WasPressedThisFrame() && spellTimers["Spell3"] <= 0f &&
            GameManager.Instance.Player.GetMana() >= spellManaCosts["Spell3"])
        {
            CastSpell("Spell3");
        }
    }





    private void CastSpell(string spellName)
    {
        switch (spellName)
        {
            case "Spell1":
                GameManager.Instance.Player.SetMana(GameManager.Instance.Player.GetMana() - spellManaCosts["Spell1"]);
                GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
                GameManager.Instance.Player.playerAnimator.SetTrigger("Spell1");
                break;
            case "Spell2":
                GameManager.Instance.Player.SetHealth(GameManager.Instance.Player.GetHealth() + 50f);
                GameManager.Instance.Player.SetEnergy(100f);
                GameManager.Instance.Player.SetMana(GameManager.Instance.Player.GetMana() + 50f);
                GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
                GameManager.Instance.Player.playerAnimator.SetTrigger("Spell2");
                break;
            case "Spell3":
                GameManager.Instance.Player.SetMana(GameManager.Instance.Player.GetMana() - spellManaCosts["Spell3"]);
                GameManager.Instance.Player.playerAnimator.applyRootMotion = true;
                GameManager.Instance.Player.playerAnimator.SetTrigger("Spell3");
                break;
        }

        spellTimers[spellName] = spellCooldowns[spellName]; // Reset cooldown
    }

    private void UpdateUI()
    {
        spell1CooldownImage.fillAmount = 1 - (spellTimers["Spell1"] / spellCooldowns["Spell1"]);
        spell2CooldownImage.fillAmount = 1 - (spellTimers["Spell2"] / spellCooldowns["Spell2"]);
        spell3CooldownImage.fillAmount = 1 - (spellTimers["Spell3"] / spellCooldowns["Spell3"]);
    }


    public void OnSpell1AttackAnimationStart()
    {
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.Yes;
        GameManager.Instance.Player.SetEnergy(GameManager.Instance.Player.GetEnergy() - 20f);
        StateManager.isCastingSpellState = IsCastingSpellState.Yes;
        
    }
    public void OnSpell2AttackAnimationStart()
    {
        StateManager.isCastingSpellState = IsCastingSpellState.Yes;
       
    }

    public void OnSpell3AttackAnimationStart()
    {
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.Yes;
        GameManager.Instance.Player.SetEnergy(GameManager.Instance.Player.GetEnergy() - 20f);
        StateManager.isCastingSpellState = IsCastingSpellState.Yes;
       
    }

    public void OnSpellAttackAnimationEnd() 
    {
        StateManager.isCastingSpellState = IsCastingSpellState.No;
        StateManager.spendingEnergyCurrentState = SpendingEnergyState.No;
        GameManager.Instance.Player.playerAnimator.applyRootMotion = false;
    }

    public void OnSpell2AttackAnimationEnd()
    {
        StateManager.isCastingSpellState = IsCastingSpellState.No;
        GameManager.Instance.Player.playerAnimator.applyRootMotion = false;
    }

    public void SpellDisableRootMotion()
    {
        GameManager.Instance.Player.playerAnimator.applyRootMotion = false;
    }

    public void SpellEnableDealDamage()
    {
        var weaponPosition = GameManager.Instance.Player.playerObject
            .GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "WeaponPosition");

        if (weaponPosition != null && weaponPosition.childCount > 0)
        {
            var weapon = weaponPosition.GetChild(0).gameObject;
            var collider = weapon.GetComponent<Collider>();

            if (collider != null)
            {
                collider.enabled = true;
                collider.isTrigger = true;

            }
        }

    }

    public void SpellDisableDealDamage()
    {

        var weaponPosition = GameManager.Instance.Player.playerObject
            .GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "WeaponPosition");

        if (weaponPosition != null && weaponPosition.childCount > 0)
        {
            var weapon = weaponPosition.GetChild(0).gameObject;
            var collider = weapon.GetComponent<Collider>();

            if (collider != null)
            {
                collider.isTrigger = false;
                collider.enabled =false;

            }
        }
    }

}
