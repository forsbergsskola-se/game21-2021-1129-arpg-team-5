using System;
using System.Collections;
using Team5.Control;
using Team5.Core;
using Team5.Entities.Player;
using Team5.Movement;
using Team5.Ui;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KillQuestFinal : MonoBehaviour, IInteractable
{
    // positioning player
    [SerializeField] private float distanceToShop;
    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition;

    public string Name;
    public Sprite Head;
    private Image HeadUI;
    public int KillTarget;
    public int RewardMoney;
    private int PlayerKills;
    private int Coins;
   
    // mouse cursor logic
    public Texture2D mouseTexture => shopCursor;
    [SerializeField] private Texture2D shopCursor;
    
    // interactions
    private TMP_Text Dialogue;
    private TMP_Text NameTag;
    private Button Button1;
    private Button Button2;
    private Button Button3;

    // button text
    private GameObject button1;
    private GameObject button2;
    private GameObject button3;
    private TMP_Text button1Text;
    private TMP_Text button2Text;
    private TMP_Text button3Text;
    
    // bools
    private bool firstVisit = true;
    private bool QuestRulesRead;
    private bool QuestAccepted;
    private bool finishedQuest;
    private bool allDone;
    
    // DIALOGUE OPTIONS
    
    // First Encounter

    public string greetText;
    public string greetAccept;
    public string greetReject;
    
    // Generic Hello

    public string genericHello;
    public string helloAccept;
    public string helloReject;

    // Quest Offer 

    public string questOffer;
    public string questAccept;
    public string questReject;
    
    // Rules Offer

    public string rulesOffer;
    public string rulesAccept;
    public string rulesReject;
    public string rulesOptOut;
    
    // Rules

    public string rules1;
    public string rulesUnderstood;
    
    // Main Quest

    public string questNotFinished;
    public string questFinished;
    public string alreadyWon;
    public string hearRulesAgain;
    public string exit;

    // Quest win
    
    public string congratulations;
    public string congratsReply;
    
    // Goodbye

    public string byeQuestAccepted;
    public string byeQuestRejected;
    public string byeQuestFinished;

    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;
        NameTag = FindObjectOfType<HUD>().NPCName.GetComponent<TMP_Text>();
        HeadUI = FindObjectOfType<HUD>().DialogueHeadNPC.GetComponent<Image>();
        
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        
        Button1 = FindObjectOfType<HUD>().Button1;
        Button2 = FindObjectOfType<HUD>().Button2;
        Button3 = FindObjectOfType<HUD>().Button3;
        
        button1 = Button1.gameObject;
        button2 = Button2.gameObject;
        button3 = Button3.gameObject;
        
        button1Text = Button1.GetComponentInChildren<TextMeshProUGUI>();
        button2Text = Button2.GetComponentInChildren<TextMeshProUGUI>();
        button3Text = Button3.GetComponentInChildren<TextMeshProUGUI>();
        
        QuestRulesRead = false;
        QuestAccepted = false;
        allDone = false;
        finishedQuest = false;

    }
    
    // Activated when Player enters invisible collider

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Dialogue.text = "";
            NameTag.text = $"{Name}";
            HeadUI.sprite = Head;
            
            // multiple HUD elements enabled and disabled onEnter
            FindObjectOfType<HUD>().HudUIActive(false,false, false,
                false,false, true, true, true);      
            
            buttonActive(true, true, false);

            // Only first time dialogue
            if (firstVisit)
            {
                firstVisit = false;
                Dialogue.text = $"{greetText}";
                button1Text.text = $"{greetAccept}";
                button2Text.text = $"{greetReject}";
            }
            
            // Every other time
            else
            {
                Dialogue.text = $"{genericHello}";
                button1Text.text = $"{helloAccept}";
                button2Text.text = $"{helloReject}";
            }
            
            // Detects button clicks
            Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
    }
    
    // When walking away from shop
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            // Activates and deactivates HUD elements onExit
            FindObjectOfType<HUD>().HudUIActive(true,true, true,
                true,true, true, false, false);        }
    }

    
    // Button 1
    private void ParameterOnClickYesStart(string test)
    {
        // Quest Offered:
        if (QuestAccepted == false || QuestRulesRead == false)
        {
            buttonActive(true, true, false);
        
            Dialogue.text = $"{questOffer}";
            button1Text.text = $"{questAccept}";
            button2Text.text = $"{questReject}";
                    
            Button1.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
        
        // Shortcut to main interactions
        else if (QuestAccepted == true && QuestRulesRead == true)
        {
            Scenario();
        }
    }

    // Triggers exit text
    private void ParameterOnClickNoStart(string test)
    {
        buttonActive(false, false, false);
        WaitExit();
    }
    
    // Rules skip
    private void ParameterOnClickSkipRules(string test)
    {
        QuestAccepted = true;
        QuestRulesRead = false; 
        Scenario();
    }

    
    // Rule question
    private void ParameterOnClickYes(string test)
    {
        Dialogue.text = $"{rulesOffer}";
        buttonActive(true, true, true);
        
        {
            button1Text.text = $"{rulesAccept}";
            button2Text.text = $"{rulesReject}";
            button3Text.text = $"{rulesOptOut}";
            
            Button1.onClick.AddListener( () => ParameterOnClickYesRules($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickSkipRules($"{nameof(Button2)} was pressed!"));
            Button3.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button3)} was pressed!"));
        }
    }
    
    // Rules trigger
    private void ParameterOnClickYesRules(string test)
    {
        ScenarioRules();
    }

    // Rules text 1
    private void ScenarioRules()
    {
        buttonActive(true, false, false);
        QuestAccepted = true;
        Dialogue.text = $"{rules1}";
        button1Text.text = "Continue";
        
        Button1.onClick.AddListener( () => ParameterOnClickYesRules2($"{nameof(Button1)} was pressed!"));
    }

    // Rules text 2
    private void ParameterOnClickYesRules2(string test)
    {
        PlayerKills = player.GetComponent<PlayerController>().killCount;

        QuestRulesRead = true;

        if (PlayerKills < KillTarget)
        {
            Dialogue.text = $"You have killed {PlayerKills} out of {KillTarget}." +
                            $"You still need to kill {(KillTarget - PlayerKills)} more.";
        }

        else
        {
            Dialogue.text = $"You've already killed all {KillTarget} that I needed!";
        }
       
        
        button1Text.text = $"{rulesUnderstood}";
        Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
    }
    
    // Once finished criteria is set up
    private void ParameterOnClickYesFinished(string test)
    {
        Coins = player.GetComponent<Wallet>().Coins;
        buttonActive(false, true, false);

        allDone = true;
        Dialogue.text = $"{congratulations}";
        button2Text.text = $"{exit}";
        Coins += RewardMoney;
        Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
    }
    
    // Main Quest Interaction
    private void Scenario()
    {
        PlayerKills = player.GetComponent<PlayerController>().killCount;
        buttonActive(false, false, false);

        if (PlayerKills < KillTarget)
        {
            finishedQuest = false;
        }
        
        else if (PlayerKills >= KillTarget)
        {
            finishedQuest = true;
        }
        
        if (finishedQuest != true)
        {
            buttonActive(true, true, false);

            Dialogue.text = $"{questNotFinished}";
            button1Text.text = $"{hearRulesAgain}";
            button2Text.text = $"{exit}";

            Button1.onClick.AddListener( () => ParameterOnClickYesRules2($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
        
        else if (finishedQuest == true && allDone == false)
        {
            buttonActive(true, true, false);

            Dialogue.text = $"{questFinished}";
            button1Text.text = $"{congratsReply}";
            Button1.onClick.AddListener( () => ParameterOnClickYesFinished($"{nameof(Button1)} was pressed!"));
        }

        else
        {
            buttonActive(false, true, false);
            Dialogue.text = $"{alreadyWon}";
            button2Text.text = $"{exit}";
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
    }
    
    // Text trigger to identify shop
    public void OnHoverEnter()
    {
    }
    
    // Disables text when not hovering over
    public void OnHoverExit()
    {
    }

    // Player technicalities (burrowed from door/gate logic)
    public void OnClick(Vector3 mouseClickVector)
    {
        var reachable = GameObject.Find("Player").GetComponent<Move>().TargetReachable(playerTargetPosition.position);
        var reachableTwo = GameObject.Find("Player").GetComponent<Move>()
            .TargetReachable(playerTargetPositionTwo.position);

        if (reachable && reachableTwo)
        {
            var distance = Vector3.Distance(player.transform.position, playerTargetPosition.position);
            var distanceTwo = Vector3.Distance(player.transform.position, playerTargetPositionTwo.position);

            TargetPosition = distance < distanceTwo ? playerTargetPosition.position : playerTargetPositionTwo.position;
        }

        else if (reachable)
            TargetPosition = playerTargetPosition.position;

        else if (reachableTwo)
            TargetPosition = playerTargetPositionTwo.position;

        if (Vector3.Distance(player.transform.position, TargetPosition) > distanceToShop)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(TargetPosition);
    }
    
    // Multi-button quick set-active method for hiding and showing different combos
    private void buttonActive(bool buttonOne, bool buttonTwo, bool buttonThree)
    {
        button1.SetActive(buttonOne);
        button2.SetActive(buttonTwo);
        button3.SetActive(buttonThree);
    }
    
    // Exit goodbyes
    private void WaitExit()
    {
        if (QuestAccepted == false && finishedQuest == false)
        {
            QuestAccepted = false;
            Dialogue.text = $"{byeQuestRejected}";
        }

        else if (finishedQuest == true)
        {
            Dialogue.text = $"{byeQuestFinished}";
        }
            
        else if (QuestAccepted == true && finishedQuest == false)
        {
            Dialogue.text = $"{byeQuestAccepted}";
        }
    }
}
