using UnityEngine;

public class AxeDealDamageManager : MonoBehaviour
{

    private Collider swordCollider;

    [SerializeField] private GameObject enemyGameObject;
    IEnemy enemy;
    private void Start()
    {
        swordCollider = GetComponent<Collider>();
        enemy = enemyGameObject.GetComponent<IEnemy>();
        // Debug.Log("EnemyI :" + enemy != null);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Shield"))
        {
            // Debug.Log("Hello Shield");

            float sustainDmg = 0;
            float enemyDmg = 0;
            if (StateManager.isBlocking == IsBlockingState.Yes)
            {
                GameManager.Instance.Player.playerAnimator.SetTrigger("ShieldBlock");
                GameManager.Instance.SoundsFxManager.PlaySoundAtIndex(17);

                // var enemy = enemyGameObject.GetComponent<IEnemy>();

                if (enemy != null)
                {

                    enemyDmg = enemy.Damage;

                }

                var item = GameManager.Instance.QuickSlotManager.GetQuickSlot(2).Item;

                if (item is ShieldSO shield)
                {
                    sustainDmg = shield.sustain;

                }

                float totalDmg = enemyDmg - sustainDmg *0.5f;
                if (totalDmg < 0) totalDmg = 0;

                GameManager.Instance.Player.GetHit(totalDmg, false);


                swordCollider.enabled = false;
                swordCollider.isTrigger = false;

                return;
            }
        }
        if (other.CompareTag("Player"))
        {

            //Debug.Log("Hello Player");
            // Debug.Log("HI2");

            //var enemy = transform.root.GetComponent<IEnemy>();
            //Debug.Log(enemy==null);
            if (enemy != null)
            {

                // Debug.Log("enemy!=null");
                float damage = enemy.Damage;
                // Debug.Log(damage);

                GameManager.Instance.Player.GetHit(damage, true);
                //Debug.Log(GameManager.Instance.Player.GetHealth());
            }
        }
    }
}

