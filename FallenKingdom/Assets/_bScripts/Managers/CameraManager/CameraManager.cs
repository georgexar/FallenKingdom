
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }


    [Header("Cinemachine Cameras")]
    [SerializeField] private CinemachineCamera defaultCamera;
    [SerializeField] private CinemachineCamera combatCamera;


    [Header("Target Lock Settings")]

    private float maxLockDistance = 40f;
    private Transform currentTarget;
    private bool isLockedOn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateCameraFromState();
    }

    private void Update()
    {
        if (GameManager.Instance.InputManager.TargetLockAction.WasPressedThisFrame())
        {
            // Debug.Log("Target Lock Button Pressed");
            if (isLockedOn)
            {
                // Debug.Log("Unlocking Target...");
                UnlockTarget();
            }
            else
            {
                // Debug.Log("Attempting to Find Target...");
                FindTargetWithRaycast();
            }
        }

        if (isLockedOn && currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(Camera.main.transform.position, currentTarget.position);


            IEnemy enemy = currentTarget.GetComponent<IEnemy>();
            if (enemy != null)
            {
                if (enemy.Health <= 0)
                {
                    UnlockTarget();
                    return;
                }
            }

            if (distanceToTarget > maxLockDistance)
            {
                //Debug.Log($"Target too far! Distance: {distanceToTarget}, Unlocking...");
                UnlockTarget();
                return;
            }
            if (GameManager.Instance.InputManager.InteractAction.WasPressedThisFrame() && (GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").gameObject.activeSelf || GameManager.Instance.Player.playerObject.transform.Find("InteractCanvas").gameObject.activeSelf))
            {
                UnlockTarget();
                return;
            }
            if (GameManager.Instance.Player.GetHealth() <= 0f || GameManager.Instance.InputManager.PauseMenuAction.WasPressedThisFrame())
            {
                UnlockTarget();
                return;
            }

        }

    }

    private void FindTargetWithRaycast()
    {
        if (Camera.main == null) return;

        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        //  Debug.Log($"Raycast Fired from {rayOrigin} in direction {rayDirection}");


        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, maxLockDistance);


        System.Array.Sort(hits, (h1, h2) => h1.distance.CompareTo(h2.distance));





        foreach (RaycastHit hit in hits)
        {
            // Debug.Log($"Raycast Hit: {hit.collider.name} (Tag: {hit.collider.tag}) at distance {hit.distance}");


            if (hit.collider.CompareTag("Ignore"))
            {
                continue;
            }


            if (hit.collider.CompareTag("EnemyZone"))
            {
                Transform enemy = hit.collider.transform.parent;

                if (enemy == null)
                {
                    //Debug.Log("Enemy Parent Not Found!");
                    continue;
                }

                // Debug.Log($"Enemy Detected: {enemy.name}");
                Transform lockInPoint = FindChildByName(enemy, "LookAtEnemy");

                if (lockInPoint == null)
                {
                    //Debug.LogWarning("LookAtEnemy point not found!");
                    continue;
                }

                if (enemy.CompareTag("Enemy"))
                {
                    Transform enemyHealthBarCanvas = FindChildByName(enemy, "EnemyHealthBarCanvas");
                    if (enemyHealthBarCanvas != null)
                    {
                        Transform lockedObject = FindChildByName(enemyHealthBarCanvas, "Locked");
                        if (lockedObject != null)
                        {
                            Image lockedImage = lockedObject.GetComponent<Image>();
                            if (lockedImage != null)
                            {
                                lockedImage.enabled = true;
                            }
                        }
                    }
                }
                else if (enemy.CompareTag("Boss"))
                {
                    Transform lockedCanvas = FindChildByName(enemy, "LockedCanvas");
                    if (lockedCanvas != null)
                    {
                        Transform lockedObject = FindChildByName(lockedCanvas, "Locked");
                        if (lockedObject != null)
                        {
                            Image lockedImage = lockedObject.GetComponent<Image>();
                            if (lockedImage != null)
                            {
                                lockedImage.enabled = true;
                            }
                        }
                    }

                }
                //Debug.Log($"Locking onto: {lockInPoint.name}");
                currentTarget = enemy;
                combatCamera.LookAt = lockInPoint;

                combatCamera.Priority = 20;
                defaultCamera.Priority = 0;

                StateManager.cameraCurrentState = CameraState.Combat;
                isLockedOn = true;


                return;
            }


            return;
        }


        // Debug.Log("No valid hits found.");
    }

    // Helper method to find a child object by name
    private Transform FindChildByName(Transform parent, string childName)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child.name == childName)
            {
                return child;
            }
        }
        return null;
    }

    private void UnlockTarget()
    {
        if (currentTarget != null)
        {
            if (currentTarget.CompareTag("Enemy"))
            {
                Transform enemyHealthBarCanvas = FindChildByName(currentTarget, "EnemyHealthBarCanvas");
                if (enemyHealthBarCanvas != null)
                {
                    Transform lockedObject = FindChildByName(enemyHealthBarCanvas, "Locked");
                    if (lockedObject != null)
                    {
                        Image lockedImage = lockedObject.GetComponent<Image>();
                        if (lockedImage != null)
                        {
                            lockedImage.enabled = false;
                        }
                    }
                }
            }
            else if (currentTarget.CompareTag("Boss"))
            {
                Transform lockedCanvas = FindChildByName(currentTarget, "LockedCanvas");
                if (lockedCanvas != null)
                {
                    Transform lockedObject = FindChildByName(lockedCanvas, "Locked");
                    if (lockedObject != null)
                    {
                        Image lockedImage = lockedObject.GetComponent<Image>();
                        if (lockedImage != null)
                        {
                            lockedImage.enabled = false;
                        }
                    }
                }
            }
        }

        combatCamera.Priority = 0;
        defaultCamera.Priority = 10;

        StateManager.cameraCurrentState = CameraState.Default;
        currentTarget = null;
        isLockedOn = false;
    }

    private void UpdateCameraFromState()
    {

        if (StateManager.cameraCurrentState == CameraState.Default)
        {
            if (Instance.defaultCamera.Priority != 10)
            {
                Instance.defaultCamera.Priority = 10;
                Instance.combatCamera.Priority = 0;
            }
        }
        else if (StateManager.cameraCurrentState == CameraState.Combat)
        {
            if (Instance.combatCamera.Priority != 10)
            {
                Instance.combatCamera.Priority = 10;
                Instance.defaultCamera.Priority = 0;
            }
        }
    }

    public void CameraToDefault()
    {
        StateManager.cameraCurrentState = CameraState.Default;
        UpdateCameraFromState();
    }

    public void CameraToCombat()
    {
        StateManager.cameraCurrentState = CameraState.Combat;
        UpdateCameraFromState();
    }

}
