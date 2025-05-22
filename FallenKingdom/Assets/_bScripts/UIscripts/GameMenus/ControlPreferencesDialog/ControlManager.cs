using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private Transform contentTransform;
    

    private void OnEnable() 
    {

    }

    public void OnResetAllButtonClicked()
    {
        

        GameManager.Instance.InputManager.ResetAllInputs();

        foreach (Transform child in contentTransform)
        {
            var prefabScript = child.GetComponent<RebindUIPrefab>();
            if (prefabScript != null)
            {
                
                prefabScript.UpdateActionBindingText();
            }
        }

       
    }
}
