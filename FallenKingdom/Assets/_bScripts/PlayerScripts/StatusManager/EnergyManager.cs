using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    

    private float runEnergy = 20f;
    
    private float regenEnergy = 30f;

    private float TimerRegen = 0f;



   public void HandleEnergy() 
   {
        //StateManager.spendingEnergyCurrentState = SpendingEnergyState.No;

        SpendEnergy();
        RegenEnergy();

   }

    void SpendEnergy()
    {
        if (StateManager.isFightingState == IsFightingState.Yes || StateManager.playerCurrentState == MovementState.Running || StateManager.isBlocking == IsBlockingState.Yes || StateManager.isCastingSpellState==IsCastingSpellState.Yes)  //POTE XALAEI ENERGEIA
        {
            StateManager.spendingEnergyCurrentState = SpendingEnergyState.Yes;
        }
        else
        {
            StateManager.spendingEnergyCurrentState = SpendingEnergyState.No;
        }


        if (GameManager.Instance.Player.GetEnergy() <= 0f)
        {
            GameManager.Instance.Player.SetEnergy(0f);
            return;
        }

        


        RunSpeedEnergy();
        BlockingEnergy();
    }


    void BlockingEnergy() 
    {
        if (StateManager.isBlocking == IsBlockingState.Yes) 
        {
            float currentEnergy = GameManager.Instance.Player.GetEnergy();
            currentEnergy -= 10f * Time.deltaTime;

            GameManager.Instance.Player.SetEnergy(currentEnergy);
        }
    }


    void RunSpeedEnergy() 
    {
        if (GameManager.Instance.Player.GetSpeed() == GameManager.Instance.Player.GetRunSpeed())
        {
            float currentEnergy = GameManager.Instance.Player.GetEnergy();
            currentEnergy -= runEnergy * Time.deltaTime;

            GameManager.Instance.Player.SetEnergy(currentEnergy);
            
        }
       
    }



    void RegenEnergy() 
    {

        if (GameManager.Instance.Player.GetEnergy() >= 100f)
        {

            GameManager.Instance.Player.SetEnergy(100f);
            return;
        }

        if (StateManager.spendingEnergyCurrentState == SpendingEnergyState.No) 
        {
           
            TimerRegen += Time.deltaTime;
            if (TimerRegen > 2f) 
            {
                float currentEnergy = GameManager.Instance.Player.GetEnergy();
                currentEnergy += regenEnergy * Time.deltaTime;

                GameManager.Instance.Player.SetEnergy(currentEnergy);
            }
            return;
        }
        TimerRegen = 0f;
    }

  
}
