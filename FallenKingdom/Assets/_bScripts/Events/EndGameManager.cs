using NUnit.Framework;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject appearGO;
    [SerializeField] private GameObject[] disableGo;
    public void TriggerEndGameFunct() 
    {
        if (appearGO != null) 
        {
            appearGO.SetActive(true);
        }
        foreach (GameObject obj in disableGo) 
        {
            if (obj.activeSelf) 
            {
                obj.SetActive(false);
            }
        }
    
    }
}
