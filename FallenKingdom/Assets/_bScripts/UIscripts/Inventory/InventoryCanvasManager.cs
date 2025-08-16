using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


public class InventoryCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject DeleteCanvas;

    private Dictionary<string, bool> savedInputStates;

    [SerializeField] private GameObject CameraPerspectives;

    private void OnEnable()
    {
        GameManager.Instance.Player.playerObject.transform.Find("QuickSlotCanvas").gameObject.SetActive(false);
        GameManager.Instance.Player.playerObject.transform.Find("SpellCanvas").gameObject.SetActive(false);

        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;


        PlayerPrefs.SetString("InventoryCanvasIsActive", "true");
        PlayerPrefs.Save();
        savedInputStates = GameManager.Instance.InputManager.GetInputsState();
        GameManager.Instance.InputManager.DisableAllExceptPauseAndInventory();

        //    GameManager.Instance.Player.GetInventory().DisplayInventory();
        //    GameManager.Instance.QuickSlotManager.PrintQuickSlots();

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

        // if (!IsAnyStoreOpen())
        // {
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        // }

        PlayerPrefs.SetString("InventoryCanvasIsActive", "false");
        PlayerPrefs.Save();
        GameManager.Instance.InputManager.SetInputsState(savedInputStates);

        GameManager.Instance.Player.playerObject.transform.Find("QuickSlotCanvas").gameObject.SetActive(true);
        GameManager.Instance.Player.playerObject.transform.Find("SpellCanvas").gameObject.SetActive(true);

        DeleteCanvas.SetActive(false);

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

    }


    bool IsAnyStoreOpen()
    {

        GameObject[] stores = GameObject.FindGameObjectsWithTag("Store");


        foreach (GameObject store in stores)
        {
            if (store.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }

}