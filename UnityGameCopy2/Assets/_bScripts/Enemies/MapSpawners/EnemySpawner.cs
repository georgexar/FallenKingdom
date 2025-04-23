using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnerBountaries;
    [SerializeField] private GameObject DisableGameObjectWhenDefeatsEnemies;
    private void Update()
    {
     
        if (transform.childCount > 0)
        {
          
            if (spawnerBountaries != null)
            {
                spawnerBountaries.SetActive(true);
            }
            
        }
        else
        {
    
            if (spawnerBountaries != null)
            {
                spawnerBountaries.SetActive(false);
            }
           
            gameObject.SetActive(false);
        }


    }
}
