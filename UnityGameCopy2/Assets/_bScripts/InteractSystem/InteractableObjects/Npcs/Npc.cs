using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    public string InteractionText => interactText;
    public bool IsInteractable => isInteractable;
    public bool TalkToPlayer => talkToPlayer; //MILAEI ME TON PAIKTH

    private bool isInteractable = true;

    private bool talkToPlayer = true;


    public string NpcId => npcId;
    public bool GiveItem => giveItem;
    public bool GiveBlueGems => giveBlueGems;
    public bool GivePurpleGems => givePurpleGems;
    public int CurrentSentenceIndex => currentSentenceIndex;
    public bool EnableGameObject => enableGameObject;
    public bool DisableGameObject => disableGameObject;
    public bool KeepGameObjectEnabled => keepGameObjectEnabled;
    public bool KeepGameObjectDisabled => keepGameObjectDisabled;

    [Header("Npc UniqueID")]
    [SerializeField] private string npcId;

    [Header("Npc GiveItem")]
    [SerializeField] private bool giveItem = false;
    [SerializeField] private int itemId = 0;
    [SerializeField] private int itemQuantity = 0;


    [Header("Npc GiveMoney")]
    [SerializeField] private bool giveBlueGems=false;
    [SerializeField] private bool givePurpleGems=false;
    [SerializeField] private int blueGemsQuantity=0;
    [SerializeField] private int purpleGemsQuantity=0;

    [Header("Enable GameObject")]
    [SerializeField] private bool enableGameObject = false;
    [SerializeField] private bool disableGameObject = false;
    [SerializeField] private GameObject gameObjectToEnable;
    [SerializeField] private GameObject gameObjectToDisable;
    [SerializeField] private bool keepGameObjectEnabled = false;
    [SerializeField] private bool keepGameObjectDisabled = false;

    [Header("Npc InteractText")]
    [SerializeField] private string interactText = "Talk to NPC";

    [Header("Interaction Options")]
    [SerializeField] private bool interactOnce = false;

    [Header("Sentences for the dialog / Disable Inputs in which sentence")]
    [SerializeField] private List<string> sentences;
    [SerializeField] private List<int> disableInputsInSentence;
    private int currentSentenceIndex = 0;
    private int sentenceIndexShown = 0;

    private string talkText;



    private void Start()
    {
        LoadNpcState();
    }

    public void Interact()
    {
        if (isInteractable)
        {
            SetTalkText();
            ShowDialog();
            GiveItemMethod();
            GiveGems();
            EnableGameObjectFunct();
            DisableGameObjectFunct();
            FixInteract();
        }
    }

    private void GiveGems() 
    {
        if (giveBlueGems) 
        {
            GameManager.Instance.Player.SetBlueGems(GameManager.Instance.Player.GetBlueGems()+blueGemsQuantity);
        }
        if (givePurpleGems) 
        {
            GameManager.Instance.Player.SetPurpleGems(GameManager.Instance.Player.GetPurpleGems() + purpleGemsQuantity);
        }
        giveBlueGems = false;
        givePurpleGems = false;
    }

    private void GiveItemMethod()
    {
        if (giveItem)
        {
            if ( itemId != 0 &&  itemQuantity!=0) 
            {
                bool gaveitem =GameManager.Instance.Player.GetInventory().AddItem(GameManager.Instance.ItemsManager.ReturnItem(itemId),itemQuantity);
              /*  if (gaveitem) 
                {
                    //PAIKSE ANIMATION GIA GIVE
                }*/
                giveItem = false;

            }
        }                                                                 
        
    }
    private void ShowDialog()
    {
        bool disableInput = disableInputsInSentence.Contains(sentenceIndexShown);
        if (disableInput)
        {
            GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").GetComponentInChildren<ChatDialogManager>().ShowDialogAndDisableAllInputsExceptPause(talkText, true);
        }
        else 
        {
            GameManager.Instance.Player.playerObject.transform.Find("ChatDialogCanvas").GetComponentInChildren<ChatDialogManager>().ShowDialog(talkText);
        }
    }

    private void SetTalkText()
    {
        if (sentences != null && sentences.Count > 0)
        {
            talkText = sentences[currentSentenceIndex];
            sentenceIndexShown = currentSentenceIndex + 1;
            if (currentSentenceIndex < sentences.Count - 1)
            {
                currentSentenceIndex++;
            }
        }
        else
        {
            talkText = "Ehmm dont annoy me. Go away!";
        }
    }

    private void FixInteract() 
    {
        if (interactOnce)
        {
            isInteractable = false;
        }
    }

    private void EnableGameObjectFunct() 
    {
        if (enableGameObject) 
        {
           if(gameObjectToEnable != null) gameObjectToEnable.SetActive(true);
        }
        enableGameObject = false;
    }
    private void DisableGameObjectFunct() 
    {
        if (disableGameObject)
        {
            if (gameObjectToDisable != null) gameObjectToDisable.SetActive(false);
        }
        disableGameObject = false;
    }


    private void LoadNpcState()
    {
        List<NpcState> savedStates = SaveLoadManager.LoadNpcState();

        foreach (NpcState state in savedStates)
        {
            if (state.npcId == npcId)
            {
                giveItem = !state.hasGivenItem;
                giveBlueGems = !state.hasGivenBlueGems;
                givePurpleGems = !state.hasGivenPurpleGems;
                isInteractable = state.isInteractable;
                currentSentenceIndex = state.currentSentenceIndex;
                enableGameObject = !state.hasEnabledGameObject;
                disableGameObject = !state.hasDisabledGameObject;

                if (keepGameObjectEnabled && state.hasEnabledGameObject)
                {
                    gameObjectToEnable.SetActive(true);
                }

                if(keepGameObjectDisabled && state.hasDisabledGameObject) 
                {
                    gameObjectToDisable.SetActive(false);
                }

            }
        }
    }

}

