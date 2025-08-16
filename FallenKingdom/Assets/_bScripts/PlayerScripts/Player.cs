using System.Collections.Generic;

using UnityEngine;

public class Player
{
    
    public GameObject playerObject { get; set; }
    public Animator playerAnimator { get; set; }
    public CharacterController playerController { get; set; }

    private float walkSpeed = 6.5f;
    private float runSpeed = 13f;
    private float jumpSpeed = 1.3f;
    private float gravity = -25f;
    private float speed = 0f;

    private float health = 100f;
    private float mana = 100f;
    private float energy = 100f;

    private int blueGemQuantity = 0;
    private int purpleGemQuantity = 0;

    private Inventory inventory;

    private List<string> collectedItems;
    private List<string> killedEnemies;

    private bool playerIsSafe=false;
    public Player(GameObject playerObject)
    {
        this.playerObject = playerObject;
        this.playerController = playerObject.GetComponent<CharacterController>();
        this.playerAnimator = playerObject.GetComponent<Animator>();

        inventory = new Inventory();
        collectedItems = new List<string>();
        killedEnemies = new List<string>();
       
    }

 
    public float GetWalkSpeed() { return this.walkSpeed; }
    public float GetRunSpeed() { return this.runSpeed; }
    public float GetJumpSpeed() { return this.jumpSpeed; }
    public float GetGravity() { return this.gravity; }
    public float GetSpeed() { return this.speed; }   
    public float GetHealth() { return this.health; }
    public bool GetPlayerIsSafe() { return this.playerIsSafe; }
    public float GetEnergy() { return this.energy; }
    public float GetMana() { return this.mana; }
    public int GetBlueGems() { return this.blueGemQuantity; }
    public int GetPurpleGems() {return this.purpleGemQuantity; }
    public Inventory GetInventory(){ return this.inventory;}
    public void SetInventory(Inventory inventory){this.inventory = inventory;}
    public void SetSpeed(float speed) { this.speed = speed; }
    public void SetHealth(float health) { this.health = health; }
    public void SetEnergy(float energy) { this.energy = energy; }
    public void SetPlayerIsSafe(bool flag) { this.playerIsSafe = flag; }
    public void SetMana(float mana) { this.mana = mana; }
    public void SetBlueGems(int gems) { this.blueGemQuantity = gems; }
    public void SetPurpleGems(int gems) { this.purpleGemQuantity = gems; }
    public void IncreaseBlueGems() { this.blueGemQuantity +=10; }
    public void IncreasePurpleGems() { this.purpleGemQuantity++; }


    public void GetHit(float damage, bool playAnim)
    {
        // if (StateManager.isBlocking == IsBlockingState.No)
        // {
        this.health -= damage;
        if (StateManager.isFightingState == IsFightingState.No)
        {
            if (playAnim && damage < GameManager.Instance.Player.GetHealth())
            {

                if (StateManager.isCastingSpellState == IsCastingSpellState.No && StateManager.isFightingState == IsFightingState.No) GameManager.Instance.Player.playerAnimator.SetTrigger("takeDamage");

            }
        }
        //  }
    }

    public List<string> GetCollectedItems()
    {
        return this.collectedItems;
    }

   
    public void AddCollectedItem(string item)
    {
        if (!collectedItems.Contains(item))
        {
            collectedItems.Add(item);
        }
    }

    public void SetCollectedItems(List<string> newCollected) 
    {
        this.collectedItems = newCollected;
    }

    public List<string> GetKilledEnemies()
    {
        return this.killedEnemies;
    }


    public void AddKilledEnemies(string enemy)
    {
        if (!killedEnemies.Contains(enemy))
        {
            killedEnemies.Add(enemy);
        }
    }

    public void SetKilledEnemies(List<string> newKilledEnemies)
    {
        this.killedEnemies = newKilledEnemies;
    }



    public void PrintCollectedItems()
    {
        if (collectedItems.Count == 0)
        {
            Debug.Log("No items collected yet.");
        }
        else
        {
            Debug.Log("Collected Items: " + string.Join(", ", collectedItems));
        }
    }
    public void PrintKilledEnemies()
    {
        if (killedEnemies.Count == 0)
        {
            Debug.Log("No killedEnemies");
        }
        else
        {
            Debug.Log("KilledEnemies: " + string.Join(", ", killedEnemies));
        }
    }
}
