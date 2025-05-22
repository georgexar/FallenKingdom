using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private float currentX = 0f;
    private float currentY = 0f;
    [SerializeField] private float smoothSpeed = 5f;

    private void Start()
    {
       
        
    }

    public void HandleAllAnimations()
    {
        GameManager.Instance.Player.SetPlayerIsSafe(GameManager.Instance.Player.playerAnimator.GetBool("SitDown") || GameManager.Instance.InputManager.AreAllInputsDisabled() || StateManager.safeZone);
        #region  DefaultParameters
        GameManager.Instance.Player.playerAnimator.SetBool("Idle", StateManager.playerCurrentState == MovementState.Idle);
        GameManager.Instance.Player.playerAnimator.SetBool("Walk", StateManager.playerCurrentState == MovementState.Walking);
        GameManager.Instance.Player.playerAnimator.SetBool("Run", StateManager.playerCurrentState == MovementState.Running);
        GameManager.Instance.Player.playerAnimator.SetBool("isJumpingUp", StateManager.playerCurrentState == MovementState.JumpingUp);
        GameManager.Instance.Player.playerAnimator.SetBool("isFalling", StateManager.playerCurrentState == MovementState.Falling);
        GameManager.Instance.Player.playerAnimator.SetBool("SitDown", StateManager.playerCurrentState == MovementState.SitDown);


        #endregion

        #region CombatParameters
        float targetX = GameManager.Instance.InputManager.MoveAction.ReadValue<Vector2>().x;
        float targetY = GameManager.Instance.InputManager.MoveAction.ReadValue<Vector2>().y;


        currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * smoothSpeed);
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * smoothSpeed);


        GameManager.Instance.Player.playerAnimator.SetFloat("x", currentX);//INPUTS BLEND TREE
        GameManager.Instance.Player.playerAnimator.SetFloat("y", currentY);//INPUTS BLEND TREE

        #endregion

        #region CameraStates
        if (StateManager.cameraCurrentState == CameraState.Default)
        {
            GameManager.Instance.Player.playerAnimator.SetLayerWeight(GameManager.Instance.Player.playerAnimator.GetLayerIndex("DefaultMovement"), 1);
            GameManager.Instance.Player.playerAnimator.SetLayerWeight(GameManager.Instance.Player.playerAnimator.GetLayerIndex("CombatMovement"), 0);

        }
        else if (StateManager.cameraCurrentState == CameraState.Combat)
        {
            GameManager.Instance.Player.playerAnimator.SetLayerWeight(GameManager.Instance.Player.playerAnimator.GetLayerIndex("DefaultMovement"), 0);
            GameManager.Instance.Player.playerAnimator.SetLayerWeight(GameManager.Instance.Player.playerAnimator.GetLayerIndex("CombatMovement"), 1);

        }
        #endregion
    }


   
}
