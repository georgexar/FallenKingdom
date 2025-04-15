using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class InputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerInputs;

    [Header("Action Map Names References")]
    [SerializeField] private string actionMapName = "DefaultPlayerInputs";

    [Header("Action Names References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string run = "Run";
    [SerializeField] private string targetLock = "TargetLock";
    [SerializeField] private string pauseMenu = "PauseMenu";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string block = "Block";
    [SerializeField] private string inventory = "Inventory";
    [SerializeField] private string quickSlot1 = "QuickSlot1";
    [SerializeField] private string quickSlot2 = "QuickSlot2";
    [SerializeField] private string quickSlot3 = "QuickSlot3";
    [SerializeField] private string quickSlot4 = "QuickSlot4";
    [SerializeField] private string quickSlot5 = "QuickSlot5";
    [SerializeField] private string spell1 = "Spell1";
    [SerializeField] private string spell2 = "Spell2";
    [SerializeField] private string spell3 = "Spell3";



    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction targetLockAction;
    private InputAction pauseMenuAction;
    private InputAction interactAction;
    private InputAction attackAction;
    private InputAction blockAction;
    private InputAction equipWeaponAction;
    private InputAction inventoryAction;
    private InputAction quickSlot1Action;
    private InputAction quickSlot2Action;
    private InputAction quickSlot3Action;
    private InputAction quickSlot4Action;
    private InputAction quickSlot5Action;
    private InputAction spell1Action;
    private InputAction spell2Action;
    private InputAction spell3Action;


    public static InputManager Instance { get; private set; }

    public InputAction MoveAction => moveAction;
    public InputAction JumpAction => jumpAction;
    public InputAction RunAction => runAction;
    public InputAction TargetLockAction => targetLockAction;
    public InputAction PauseMenuAction => pauseMenuAction;
    public InputAction InteractAction => interactAction;
    public InputAction AttackAction => attackAction;
    public InputAction BlockAction => blockAction;
    public InputAction InventoryAction => inventoryAction;
    public InputAction QuickSlot1Action => quickSlot1Action;
    public InputAction QuickSlot2Action => quickSlot2Action;
    public InputAction QuickSlot3Action => quickSlot3Action;
    public InputAction QuickSlot4Action => quickSlot4Action;
    public InputAction QuickSlot5Action => quickSlot5Action;
    public InputAction Spell1Action => spell1Action;
    public InputAction Spell2Action => spell2Action;
    public InputAction Spell3Action => spell3Action;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeInputActions();

    }

   

    private void InitializeInputActions()
    {
        moveAction = playerInputs.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerInputs.FindActionMap(actionMapName).FindAction(jump);
        runAction = playerInputs.FindActionMap(actionMapName).FindAction(run);
        targetLockAction = playerInputs.FindActionMap(actionMapName).FindAction(targetLock);
        pauseMenuAction = playerInputs.FindActionMap(actionMapName).FindAction(pauseMenu);
        interactAction = playerInputs.FindActionMap(actionMapName).FindAction(interact);
        attackAction = playerInputs.FindActionMap(actionMapName).FindAction(attack);
        blockAction = playerInputs.FindActionMap(actionMapName).FindAction(block);
        inventoryAction = playerInputs.FindActionMap(actionMapName).FindAction(inventory);
        quickSlot1Action = playerInputs.FindActionMap(actionMapName).FindAction(quickSlot1);
        quickSlot2Action = playerInputs.FindActionMap(actionMapName).FindAction(quickSlot2);
        quickSlot3Action = playerInputs.FindActionMap(actionMapName).FindAction(quickSlot3);
        quickSlot4Action = playerInputs.FindActionMap(actionMapName).FindAction(quickSlot4);
        quickSlot5Action = playerInputs.FindActionMap(actionMapName).FindAction(quickSlot5);
        spell1Action = playerInputs.FindActionMap(actionMapName).FindAction(spell1);
        spell2Action = playerInputs.FindActionMap(actionMapName).FindAction(spell2);
        spell3Action = playerInputs.FindActionMap(actionMapName).FindAction(spell3);


        RebindActionsIfCan();




        EnableInputActions();
    }

    public Dictionary<string, bool> GetInputsState()
    {
        var inputStates = new Dictionary<string, bool>();

        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            inputStates.Add(action.name, action.enabled);
        }

        return inputStates;
    }

    public void SetInputsState(Dictionary<string, bool> inputStates)
    {
        foreach (var entry in inputStates)
        {
           
            var action = playerInputs.FindActionMap(actionMapName).FindAction(entry.Key);

            if (action != null)
            {
                if (entry.Value)
                {
                    action.Enable();
                }
                else
                {
                    action.Disable();
                }
            }
        }
    }

    public void ClearInputPreferences()
    {
        PlayerPrefs.DeleteKey("MoveUp");
        PlayerPrefs.DeleteKey("MoveDown");
        PlayerPrefs.DeleteKey("MoveLeft");
        PlayerPrefs.DeleteKey("MoveRight");
        PlayerPrefs.DeleteKey("Jump");
        PlayerPrefs.DeleteKey("Run");
        PlayerPrefs.DeleteKey("TargetLock");
        PlayerPrefs.DeleteKey("PauseMenu");
        PlayerPrefs.DeleteKey("Interact");
        PlayerPrefs.DeleteKey("Attack");
        PlayerPrefs.DeleteKey("Block");
        PlayerPrefs.DeleteKey("Inventory");
        PlayerPrefs.DeleteKey("QuickSlot1");
        PlayerPrefs.DeleteKey("QuickSlot2");
        PlayerPrefs.DeleteKey("QuickSlot3");
        PlayerPrefs.DeleteKey("QuickSlot4");
        PlayerPrefs.DeleteKey("QuickSlot5");
        PlayerPrefs.DeleteKey("Spell1");
        PlayerPrefs.DeleteKey("Spell2");
        PlayerPrefs.DeleteKey("Spell3");

        PlayerPrefs.Save();

    }


    private void RebindActionsIfCan() 
    {
        // Rebind Actions if can
        RebindCompositePart(moveAction, "MoveUp", 1);
        RebindCompositePart(moveAction, "MoveDown", 2);
        RebindCompositePart(moveAction, "MoveLeft", 3);
        RebindCompositePart(moveAction, "MoveRight", 4);
        RebindInput(jumpAction, "Jump");
        RebindInput(runAction, "Run");
        RebindInput(targetLockAction, "TargetLock");
        RebindInput(pauseMenuAction, "PauseMenu");
        RebindInput(interactAction, "Interact");
        RebindInput(attackAction, "Attack");
        RebindInput(blockAction, "Block");
        RebindInput(inventoryAction, "Inventory");
        RebindInput(quickSlot1Action, "QuickSlot1");
        RebindInput(quickSlot2Action, "QuickSlot2");
        RebindInput(quickSlot3Action, "QuickSlot3");
        RebindInput(quickSlot4Action, "QuickSlot4");
        RebindInput(quickSlot5Action, "QuickSlot5");
        RebindInput(spell1Action, "Spell1");
        RebindInput(spell2Action, "Spell2");
        RebindInput(spell3Action, "Spell3");

    }

    public void EnableInputActions()
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            action?.Enable();
        }
    }

   

    private void OnDisable()
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            action?.Disable();
        }
    }

    public void DisableAllExceptPause()
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            if (action == pauseMenuAction) continue;
            action?.Disable();
        }
    }

    public void DisableAllExceptPauseAndInventory()
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            if ( (action == pauseMenuAction) || (action == inventoryAction)) continue;
            action?.Disable();
        }
    }

    public void DisableAllInputs()
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            action?.Disable();
        }
    }
    public void ResetAllInputs() 
    {
        ClearInputPreferences();

        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            action.RemoveAllBindingOverrides();
            action.Disable();
            action.Enable();
        }

    }
   
    public void RebindInput(InputAction action, string playerPrefKey)
    {
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            string savedPath = PlayerPrefs.GetString(playerPrefKey);
            action.ApplyBindingOverride(0, savedPath);
        }
    }

    public void RebindCompositePart(InputAction action, string playerPrefKey, int index)
    {
        if (action == null)
        {
            Debug.LogError("Action is null. Please provide a valid InputAction.");
            return;
        }

        if (action.bindings.Count == 0)
        {
            Debug.LogError("The action provided does not have any bindings.");
            return;
        }

        if (index < 0 || index >= action.bindings.Count)
        {
            Debug.LogError($"Invalid index: {index}. It must be within the range of action bindings.");
            return;
        }


        if (!action.bindings[index].isPartOfComposite)
        {
            Debug.LogError($"The binding at index {index} is not part of a composite binding.");
            return;
        }


        if (!PlayerPrefs.HasKey(playerPrefKey))
        {
            
            return;
        }

        string newBindingPath = PlayerPrefs.GetString(playerPrefKey);

        if (string.IsNullOrEmpty(newBindingPath))
        {
            Debug.LogWarning($"The PlayerPrefs value for key '{playerPrefKey}' is empty or null. Rebind canceled.");
            return;
        }

        action.ApplyBindingOverride(index, newBindingPath);

    }


    public void LogAllEffectivePaths()
    {

        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            foreach (var binding in action.bindings)
            {
                Debug.Log($"Action '{action.name}' binding: {binding.effectivePath}");
            }
        }

    }


    public bool IsDuplicate(string effective_path_of_binding, InputAction inputAction, string composingPartName)
    {

        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            foreach (var actionBinding in action.bindings)
            {
                //SOS POLY SHMANTIKO GIA NA PROSPERNAEI TO IDIO BUTTON POU PATAEI CLICK
                if (string.IsNullOrEmpty(composingPartName))
                {
                    if (action == inputAction)
                    {
                        continue;
                    }
                }
                else
                {
                    if (actionBinding.name.ToLower() == composingPartName.ToLower())
                    {
                        continue;
                    }
                }
                if (ConvertString(actionBinding.effectivePath).ToLower() == ConvertString(effective_path_of_binding).ToLower())
                {
                    return true;
                }

            }

        }
        return false;
    }

    public string ConvertString(string stringname)
    {
        int lastSlashIndex = stringname.LastIndexOf('/');

        if (lastSlashIndex != -1 && lastSlashIndex + 1 < stringname.Length)
        {
            return stringname.Substring(lastSlashIndex + 1);
        }

        return stringname;
    }

    public bool AreAllInputsDisabled()
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
         
            if (action.enabled)
            {
                return false;
            }
        }
        return true;
    }

    public string ReturnAction(InputAction actionToFind) 
    {
        foreach (var action in playerInputs.FindActionMap(actionMapName).actions)
        {
            if(action == actionToFind)
            {
               return ConvertString(action.bindings[0].effectivePath).ToUpper(); ; 
            } 
        }
        return " ";
    }



}
