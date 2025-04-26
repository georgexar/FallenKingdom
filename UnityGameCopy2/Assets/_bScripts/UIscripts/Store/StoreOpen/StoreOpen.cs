using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class StoreOpen : MonoBehaviour
{
    private Dictionary<string, bool> savedInputStates;

    private GameObject CameraPerspectives;

    [SerializeField] private GameObject BuyDialog;
    [SerializeField] private GameObject NotEnoughDialog;
    [SerializeField] private GameObject StoreViewport;


    private void OnEnable()
    {
        
        savedInputStates = GameManager.Instance.InputManager.GetInputsState();
        GameManager.Instance.InputManager.DisableAllExceptPauseAndInventory();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //    GameManager.Instance.Player.GetInventory().DisplayInventory();
        //    GameManager.Instance.QuickSlotManager.PrintQuickSlots();

        if (GameManager.Instance.Player.playerObject != null)
        { CameraPerspectives = GameManager.Instance.Player.playerObject.transform.Find("CameraPerspectives")?.gameObject; }
        if (CameraPerspectives != null)
        {
            CinemachineInputAxisController[] inputControllers = CameraPerspectives.GetComponentsInChildren<CinemachineInputAxisController>(true);

            foreach (var controller in inputControllers)
            {
                if (controller != null)
                {
                    controller.enabled = false;
                }
            }
        }

    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (GameManager.Instance.Player.playerObject != null)
        { CameraPerspectives = GameManager.Instance.Player.playerObject.transform.Find("CameraPerspectives")?.gameObject; }
        GameManager.Instance.InputManager.SetInputsState(savedInputStates);

        if (CameraPerspectives != null)
        {
            CinemachineInputAxisController[] inputControllers = CameraPerspectives.GetComponentsInChildren<CinemachineInputAxisController>(true);

            foreach (var controller in inputControllers)
            {
                if (controller != null)
                {
                    controller.enabled = true;
                }
            }
        }

        BuyDialog.SetActive(false);
        StoreViewport.SetActive(true);
        NotEnoughDialog.SetActive(false);

    }
}
