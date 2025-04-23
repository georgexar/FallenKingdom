using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{

    [SerializeField] private string interactText = "Begin Adventure";

    [SerializeField] private GameObject portalTo;

    [SerializeField] private bool ReturnFromBoss;

    [SerializeField] private bool GoToBoss;
    [SerializeField] private GameObject activateBossRoom;

    private float delayBeforeTeleport = 1.2f;

    private bool isInteractable = true;

    private bool talkToPlayer = false;
    public string InteractionText => interactText;
    public bool IsInteractable => isInteractable;
    public bool TalkToPlayer => talkToPlayer;

    public void Interact()
    {
        if (isInteractable)
        {
            //PLAY ANIMATION FOR PORTAL TOO
            GameManager.Instance.Player.playerAnimator.SetTrigger("Open");

            if (ReturnFromBoss) 
            {
                GameObject.Find("EndGameManager").GetComponent<EndGameManager>().TriggerEndGameFunct();
            }
           
            GameManager.Instance.StartCoroutine(DelayedTeleport());


        }
    }
    private IEnumerator DelayedTeleport()
    {

        CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = false;
            GameManager.Instance.InputManager.DisableAllInputs();
        }

        yield return new WaitForSeconds(delayBeforeTeleport);


        if (GameManager.Instance.Player.playerController != null)
        {
            GameManager.Instance.Player.playerController.enabled = false;
        }


        Vector3 portalPosition = portalTo.transform.position;
        portalPosition.y += 2f;

        GameManager.Instance.Player.playerObject.transform.SetPositionAndRotation(portalPosition, portalTo.transform.rotation);



        if (GameManager.Instance.Player.playerController != null)
        {
            GameManager.Instance.Player.playerController.enabled = true;
        }

        yield return new WaitForSeconds(0.1f);

        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = true;
            GameManager.Instance.InputManager.EnableInputActions();
        }

        if (GoToBoss)
        {
            activateBossRoom.SetActive(true);
        }

    }

}