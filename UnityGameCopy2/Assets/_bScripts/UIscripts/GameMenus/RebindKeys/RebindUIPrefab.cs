using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.Rendering.HDROutputUtils;

public class RebindUIPrefab : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI actionNameText;
    [SerializeField] private Button triggerRebindButton;
    [SerializeField] private TextMeshProUGUI actionBindingText;

    [Header("Input Settings")]
    [SerializeField] private InputActionReference inputActionReference;
    [SerializeField] private string compositePart;
    [SerializeField] private string PlayerPrefsName;

    [Header("Disable Buttons From")]
    [SerializeField] private GameObject controlsPreferencesDialog;


    private string previousBindingPath;
    private InputAction inputAction;
    private bool isRebindingInProgress = false;
    private void Start()
    {
        if (inputActionReference != null)
        {
            inputAction = inputActionReference.action;

            if (inputAction == null)
            {
                Debug.LogError("InputActionReference is not assigned correctly.");
                return;
            }

            UpdateActionBindingText();
            triggerRebindButton.onClick.AddListener(StartRebinding);
        }
    }

    private void OnEnable()
    {
        PlayerPrefs.SetInt("IsRebindingInProgress", 0);
        PlayerPrefs.Save();
        isRebindingInProgress = false;
    }

    private void StartRebinding()
    {

        if (isRebindingInProgress)
        {
            return;
        }

        isRebindingInProgress = true;

        PlayerPrefs.SetInt("IsRebindingInProgress", isRebindingInProgress ? 1 : 0);
        PlayerPrefs.Save();

        DisableOtherButtons();

        if (inputAction == null)
        {
            Debug.LogError("InputAction is not assigned.");
            isRebindingInProgress = false;

            PlayerPrefs.SetInt("IsRebindingInProgress", isRebindingInProgress ? 1 : 0);
            PlayerPrefs.Save();

            EnableAllButtons();

            return;
        }

        int bindingIndex = GetBindingIndex();
        if (bindingIndex == -1)
        {
            isRebindingInProgress = false;

            PlayerPrefs.SetInt("IsRebindingInProgress", isRebindingInProgress ? 1 : 0);
            PlayerPrefs.Save();

            EnableAllButtons();

            return;
        }

       
        bool wasActionEnabled = inputAction.enabled;
       

        actionBindingText.text = "Waiting...";
        

        inputAction.Disable();

        InputSystem.onAnyButtonPress.CallOnce(control =>
        {
           
            if (!isRebindingInProgress) return;
            string newBindingPath = control.path;
            if (newBindingPath.ToLower().Contains("scroll")) //EAN VALEI RODELA NA MIN TIN DEXTHEI
            {
               
                isRebindingInProgress = false;
                PlayerPrefs.SetInt("IsRebindingInProgress", isRebindingInProgress ? 1 : 0);
                PlayerPrefs.Save();
                StartRebinding();
                return;
            }

            if (GameManager.Instance.InputManager.IsDuplicate(newBindingPath, inputAction, compositePart))
            {
             
                isRebindingInProgress = false;
                PlayerPrefs.SetInt("IsRebindingInProgress", isRebindingInProgress ? 1 : 0);
                PlayerPrefs.Save();
                StartRebinding();
                return;
            }

            PlayerPrefs.SetString(PlayerPrefsName, newBindingPath);
            PlayerPrefs.Save();

            if (!string.IsNullOrEmpty(compositePart)) //EINAI COMPOSITE
            {
                GameManager.Instance.InputManager.RebindCompositePart(inputAction, PlayerPrefsName, bindingIndex);
            }
            else // EINAI APLO ACTION
            {
                GameManager.Instance.InputManager.RebindInput(inputAction, PlayerPrefsName);
            }


            UpdateActionBindingText();

            if (wasActionEnabled)
            {
                inputAction.Enable();
               
            }

            StartCoroutine(FinishRebindingAfterDelay(0.2f));


        });


    }

    private int GetBindingIndex()
    {
        int bindingIndex = -1;

        if (!string.IsNullOrEmpty(compositePart))
        {
           
            string compositePartLower = compositePart.ToLower();
           

            for (int i = 0; i < inputAction.bindings.Count; i++)
            {

                string bindingNameLower = inputAction.bindings[i].name.ToLower();

               
       
                if (inputAction.bindings[i].isPartOfComposite && bindingNameLower == compositePartLower)
                {
                    bindingIndex = i;
                    break;
                }
            }

            if (bindingIndex == -1)
            {
                Debug.LogError($"Composite part '{compositePart}' not found for action '{inputAction.name}'.");
            }
        }
        else
        {
            bindingIndex = 0;
        }

        return bindingIndex;
    }


    public void UpdateActionBindingText()
    {
        if (inputAction == null)
        {
            return;
        }

        int bindingIndex = GetBindingIndex();
        if (bindingIndex == -1) return;

        string bindingPath = inputAction.bindings[bindingIndex].effectivePath;

        if (string.IsNullOrEmpty(bindingPath))
        {
            actionBindingText.text = "Unbound";
        }
        else
        {
            
            int lastSlashIndex = bindingPath.LastIndexOf('/');
            if (lastSlashIndex != -1 && lastSlashIndex + 1 < bindingPath.Length)
            {
                bindingPath = bindingPath.Substring(lastSlashIndex + 1);
            }
            actionBindingText.text = bindingPath;
        }

       
    }

    private void DisableOtherButtons()
    {
      
        if (controlsPreferencesDialog == null)
        {
          
            return;
        }

        Button[] allButtons = controlsPreferencesDialog.GetComponentsInChildren<Button>();

        foreach (Button button in allButtons)
        {
           
            if (button != triggerRebindButton) 
            {
                button.interactable = false;
            }
        }
    }


    private void FinishRebinding()
    {
       
        isRebindingInProgress = false;
        PlayerPrefs.SetInt("IsRebindingInProgress", 0);
        PlayerPrefs.Save();
        
        EnableAllButtons();
    }

    private void EnableAllButtons()
    {
       
        if (controlsPreferencesDialog == null)
        {
            Debug.LogError("ControlsPreferencesDialog reference is not set!");
           
            return;
        }

        Button[] allButtons = controlsPreferencesDialog.GetComponentsInChildren<Button>();

        foreach (Button button in allButtons)
        {
           
            if (button != triggerRebindButton)
            {
              
                button.interactable = true;
            }
        }
    }


    private System.Collections.IEnumerator FinishRebindingAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        FinishRebinding();
    }



}
