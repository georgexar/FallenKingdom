
using UnityEngine;

public class MovementManager : MonoBehaviour
{

    private Vector3 direction;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private Vector3 playerVelocity;

    


    private float rayLength = 3.5f;

    


    public void HandleAllMovement()
    {
        HandleMovement();
    }

    void HandleMovement() 
    {
        //Debug.Log($"DeltaPosition before move: {GameManager.Instance.Player.playerAnimator.deltaPosition}");

        if(GameManager.Instance.Player==null)return;
        if (GameManager.Instance.Player.playerController.enabled == false) return;

        SetPlayerStatesAndMoveSpeeds();

        HandleMovementInput();

        if (StateManager.cameraCurrentState == CameraState.Default)
        {
            HandleDefaultRotation();
        }
        else if (StateManager.cameraCurrentState == CameraState.Combat)
        {
            HandleCombatRotation();
        }


        if (!GameManager.Instance.Player.playerAnimator.applyRootMotion) // EAN EXW ROOT MOTION STON ANIMATOR
        {
            HandleJump();

            MovePlayer();
        }
       
        ApplyGravity();

    }

    private void HandleMovementInput()
    {
        cameraForward = Camera.main.transform.forward;
        cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        direction = (cameraForward * GameManager.Instance.InputManager.MoveAction.ReadValue<Vector2>().y + cameraRight * GameManager.Instance.InputManager.MoveAction.ReadValue<Vector2>().x);
        if (direction.sqrMagnitude > 1f) { direction.Normalize(); } //Stop Slide
    }

    void HandleDefaultRotation()
    {
        if (direction != Vector3.zero) //  Doesnt Move
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f); // 10f is rotation speed
        }
    }

    void HandleCombatRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 10f is rotation speed
    }
    void HandleJump()
    {
       
        if (GameManager.Instance.Player.playerController.isGrounded && GameManager.Instance.InputManager.JumpAction.WasPressedThisFrame())
        {
            playerVelocity.y = IsRunning() ? Mathf.Sqrt((GameManager.Instance.Player.GetJumpSpeed() * 1.5f) * -2f * GameManager.Instance.Player.GetGravity()) : Mathf.Sqrt(GameManager.Instance.Player.GetJumpSpeed() * -2f * GameManager.Instance.Player.GetGravity());
           
        }
    }
    bool IsRunning() 
    {
        return direction != Vector3.zero && GameManager.Instance.InputManager.RunAction.IsPressed() /* && player.playerController.isGrounded */ && GameManager.Instance.Player.GetEnergy()>0f && StateManager.isFightingState == IsFightingState.No;
    }

    bool IsWalking() 
    {
        return direction != Vector3.zero /* && player.playerController.isGrounded */ && (!GameManager.Instance.InputManager.RunAction.IsPressed() || GameManager.Instance.Player.GetEnergy() <= 0f || StateManager.isBlocking == IsBlockingState.Yes) ;
    }
    bool IsIdle() 
    {
        return direction == Vector3.zero && GameManager.Instance.Player.playerController.isGrounded && StateManager.playerCurrentState != MovementState.SitDown;
    }
    bool IsJumpingUp()
    {
        return GameManager.Instance.Player.playerController.isGrounded && GameManager.Instance.InputManager.JumpAction.WasPressedThisFrame();
    }

    //  bool IsFalling()
    // {
    //    return !GameManager.Instance.Player.playerController.isGrounded && playerVelocity.y < 0;
    // }

    bool IsFalling()
    {
        if (!GameManager.Instance.Player.playerController.isGrounded && playerVelocity.y < 0)
        {
            Vector3 origin = transform.position;
            RaycastHit hit;
           
            if (!Physics.Raycast(origin, Vector3.down, out hit, rayLength))
            {
                return true;
            }
        }
        return false;
    }

    void MovePlayer()
    {
        if (!GameManager.Instance.Player.playerController.isGrounded)
        {
            direction *= 0.8f;
            GameManager.Instance.Player.playerController.Move((direction * GameManager.Instance.Player.GetSpeed() + playerVelocity) * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.Player.playerController.Move(direction * GameManager.Instance.Player.GetSpeed() * Time.deltaTime);
        }
    }

  


    void SetPlayerStatesAndMoveSpeeds()
    {
        if (GameManager.Instance.Player.playerAnimator.applyRootMotion) 
        {
            StateManager.playerCurrentState = MovementState.Other;
            GameManager.Instance.Player.SetSpeed(0);
            return;
        }

        if (IsRunning())
        {
            if (GameManager.Instance.Player.playerController.isGrounded)
            {
                StateManager.playerCurrentState = MovementState.Running;
            }
            GameManager.Instance.Player.SetSpeed(GameManager.Instance.Player.GetRunSpeed());
        }
        if (IsWalking()) 
        {
            if (GameManager.Instance.Player.playerController.isGrounded)
            {
                StateManager.playerCurrentState = MovementState.Walking;
            }
            GameManager.Instance.Player.SetSpeed(GameManager.Instance.Player.GetWalkSpeed());
        }
        if (IsIdle()) 
        {
            StateManager.playerCurrentState = MovementState.Idle;
            GameManager.Instance.Player.SetSpeed(0);
        }
        if (IsJumpingUp()) 
        {
            StateManager.playerCurrentState = MovementState.JumpingUp;
        }
        if (IsFalling()) 
        { 
            StateManager.playerCurrentState = MovementState.Falling; 
        }
       if((StateManager.playerCurrentState == MovementState.JumpingUp || StateManager.playerCurrentState == MovementState.Falling) && GameManager.Instance.InputManager.RunAction.WasReleasedThisFrame()) 
        {
            GameManager.Instance.Player.SetSpeed(GameManager.Instance.Player.GetWalkSpeed());
        } 
    }
    void ApplyGravity()
    {
       

        if (!GameManager.Instance.Player.playerController.isGrounded)
        {
            playerVelocity.y += GameManager.Instance.Player.GetGravity() * Time.deltaTime;
        }
        else if (playerVelocity.y <= 0)
        {
            playerVelocity.y = -2; // Small Downforce to keep grounded
        }
        GameManager.Instance.Player.playerController.Move(playerVelocity * Time.deltaTime);
    }

   
}
