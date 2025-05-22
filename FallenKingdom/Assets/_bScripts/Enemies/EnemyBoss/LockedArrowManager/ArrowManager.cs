using UnityEngine;

public class ArrowManager : MonoBehaviour
{
   

    private void LateUpdate()
    {
        if (Camera.main == null) return;

        Transform cameraTransform = Camera.main.transform;

        
        Vector3 direction = cameraTransform.position - transform.position;

        
        direction.y = 0;

       
        transform.forward = -direction.normalized;
    }
}
