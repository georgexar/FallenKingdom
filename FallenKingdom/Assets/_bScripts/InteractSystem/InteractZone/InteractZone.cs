using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    [SerializeField]private GameObject interactCanvas;
    [SerializeField]private TextMeshProUGUI interactText;
    private string InteractableLayerName = "Interactable";

    private List<IInteractable> interactables = new List<IInteractable>();

    private IInteractable currentClosestInteractable = null;


    private void Update()
    {
        
        IInteractable closestInteractable = GetClosestInteractable();
        UpdateInteractCanvas();
        if (closestInteractable != currentClosestInteractable)
        {
            currentClosestInteractable = closestInteractable;
           
        }
        if (interactCanvas.activeSelf && GameManager.Instance.InputManager.InteractAction.WasPressedThisFrame())
        {
            if (currentClosestInteractable != null)
            {
                //Gia kathe antikeimeno kane kati sto interact
                currentClosestInteractable.Interact();
                if (GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").gameObject.activeSelf) 
                {
                    interactCanvas.SetActive(false);
                }
                
            }
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == InteractableLayerName)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null && !interactables.Contains(interactable) && interactable.IsInteractable)
            {
                interactables.Add(interactable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == InteractableLayerName)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if(interactable != null && interactable.TalkToPlayer) 
            {
                TurnOffChatDialogCanvas();
            }

            if (interactable != null && interactables.Contains(interactable))
            {
                interactables.Remove(interactable);
            }
        }
    }

    private void UpdateInteractCanvas()
    {
        if (!GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").gameObject.activeSelf && !(Time.timeScale == 0) && !(PlayerPrefs.GetString("InventoryCanvasIsActive", "false") == "true") && !(StateManager.playerDeadState == PlayerDeadState.Dead))
        {
            if (currentClosestInteractable != null)
            {

                interactText.text = "" + currentClosestInteractable.InteractionText;
                interactCanvas.SetActive(true);
            }
            else
            {
                interactCanvas.SetActive(false);
            }
        }
        else
        {
            if (interactCanvas.activeSelf)
            {
                interactCanvas.SetActive(false);
            }
        }
    }

    private IInteractable GetClosestInteractable()
    {
        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        interactables.RemoveAll(interactable => interactable == null || ((MonoBehaviour)interactable) == null || !interactable.IsInteractable); // REMOVE ALL DESTROYED||NULL ITEMS FROM LIST

        foreach (var interactable in interactables)
        {
            Collider collider = ((MonoBehaviour)interactable).GetComponent<Collider>();
            if (collider != null)
            {

                Vector3 closestPoint = collider.ClosestPoint(GameManager.Instance.Player.playerObject.transform.position);


                float distance = Vector3.Distance(GameManager.Instance.Player.playerObject.transform.position, closestPoint);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }
        }

        return closest;
    }


    private void TurnOffChatDialogCanvas() 
    {
        GameObject chatDialogCanvas = GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").gameObject;
        if (chatDialogCanvas.activeSelf)
        {
            chatDialogCanvas.SetActive(false);
        }
    }

}
