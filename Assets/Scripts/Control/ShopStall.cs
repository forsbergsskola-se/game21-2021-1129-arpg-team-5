using System;
using System.Collections;
using Team5.Control;
using Team5.Core;
using Team5.Movement;
using Team5.Ui;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopStall : MonoBehaviour, IInteractable
{
    // positioning player
    [SerializeField] private float distanceToShop;
    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition;
   
    // mouse cursor logic
    public Texture2D mouseTexture => shopCursor;
    [SerializeField] private Texture2D shopCursor;
    private GameObject NameTag;
   
    // interactions
    public GameObject GoldenSkull;
    private TMP_Text Dialogue;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    
    // skulls
    private int Skulls;
    public int SkullsTotal = 22;
    private GameObject SkullIcons;
    private int whiteSkulls;
    private int whiteSkullsTotal = 2;
    private int redSkulls;
    private int redSkullsTotal = 2;
    private int purpleSkulls;
    private int purpleSkullsTotal = 1;
    private int goldSkulls;
    private int goldSkullsTotal = 1;

    // buttons
    private GameObject button1;
    private GameObject button2;
    private GameObject button3;
    private TMP_Text button1Text;
    private TMP_Text button2Text;
    private TMP_Text button3Text;
    
    // bools
    private bool firstVisit = true;
    private bool skullQuestRulesRead;
    private bool skullQuestAccepted;
    private bool finishedQuest;
    private bool subtractingFinished = false;


    
    private void Awake()
    {
        finishedQuest = false;
        player = GameObject.FindGameObjectWithTag("Player");
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;
        NameTag = this.gameObject.transform.Find("Name Tag").gameObject;
        //SkullIcons = FindObjectOfType<HUD>().SkullIcons;
        
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        
        button1 = Button1.gameObject;
        button2 = Button2.gameObject;
        button3 = Button3.gameObject;
        button1Text = Button1.GetComponentInChildren<TextMeshProUGUI>();
        button2Text = Button2.GetComponentInChildren<TextMeshProUGUI>();
        button3Text = Button3.GetComponentInChildren<TextMeshProUGUI>();
        
        skullQuestRulesRead = false;
        skullQuestAccepted = false;
    }
    
    // Activated when Player enters invisible collider

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Refresh();
            
            // multiple HUD elements enabled and disabled onEnter
            FindObjectOfType<HUD>().HudUIActive(false,false, false,false,false, false);
            buttonActive(true, true, false);
            NameTag.SetActive(true);

            // Only first time dialogue
            if (firstVisit == true)
            {
                firstVisit = false;
                Dialogue.text = "Hi there, welcome!";
                button1Text.text = "Hello friend!";
                button2Text.text = "Not interested";
            }
            
            // Every other time
            else
            {
                Dialogue.text = "Hi again, friend!";
                button1Text.text = "Oh hai!";
                button2Text.text = "Not today";
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
            FindObjectOfType<HUD>().HudUIActive(true,true, true,false,false, true);
        }
    }

    
    // Button 1
    private void ParameterOnClickYesStart(string test)
    {
        // Quest Offered:
        if (skullQuestAccepted == false )
        {
            buttonActive(true, true, false);
        
            Dialogue.text = "Want to do the skull quest?";
            button1Text.text = "Sure!";
            button2Text.text = "Maybe later";
                    
            Button1.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
        
        // Check for rules skip
        else if (skullQuestRulesRead == false)
        {
            SkullScenarioRules();
        }
        
        // Shortcut to main interactions
        else if (skullQuestAccepted == true && skullQuestRulesRead == true)
        {
            SkullScenario();
        }
    }

    // Triggers exit text
    private void ParameterOnClickNoStart(string test)
    {
        Refresh();
        buttonActive(false, false, false);
        WaitExit();
    }
    
    // Rule question
    private void ParameterOnClickYes(string test)
    {
        skullQuestAccepted = true;
        Dialogue.text = "Want to hear the rules?";
        
        {
            button1Text.text = "Okay";
            button2Text.text = "No thanks";
            
            Button1.onClick.AddListener( () => ParameterOnClickYesRules($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button2)} was pressed!"));
        }
    }
    
    // Rules trigger
    private void ParameterOnClickYesRules(string test)
    {
        SkullScenarioRules();
    }

    // Rules text 1
    private void SkullScenarioRules()
    {
        buttonActive(true, false, false);
        
        Dialogue.text = $"See this big golden skull behind me? If you want it collect {SkullsTotal} skulls for me.";
        button1Text.text = "Continue";
        
        Button1.onClick.AddListener( () => ParameterOnClickYesRules2($"{nameof(Button1)} was pressed!"));
    }

    // Rules text 2
    private void ParameterOnClickYesRules2(string test)
    {
        skullQuestRulesRead = true;
        
        Dialogue.text = "White skulls =	1 \nRed skulls =	5 \nPurple skulls =	10";
        button1Text.text = "Okay, I get it!";

        Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
    }
    
    // Skull totals list.
    private void ParameterOnClickTotals(string test)
    {
        // Updates int vales only here when it's needed.
        whiteSkulls = player.GetComponent<PlayerUI>().whiteSkulls;
        redSkulls = player.GetComponent<PlayerUI>().redSkulls;
        purpleSkulls = player.GetComponent<PlayerUI>().purpleSkulls;
        goldSkulls = player.GetComponent<PlayerUI>().goldSkulls;

        buttonActive(true, true, false);
        button1Text.text = "Return";
        button2Text.text = "Leave";
        
        // Changes formatting + adds icons
        SkullIcons.SetActive(true);
        Dialogue.fontSize = 30;
        
        // Skull print
        Dialogue.text = $"White skulls =	{whiteSkulls} / {whiteSkullsTotal} \n" +
                        $"Red Skulls =	{redSkulls} / {redSkullsTotal} \n" +
                        $"Purple Skulls =	{purpleSkulls} / {purpleSkullsTotal} \n" +
                        $"Gold Skulls =	{goldSkulls} / {goldSkullsTotal}";
        
        Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
    }
    
    private void ParameterOnClickYesFinished(string test)
    {
        subtractingFinished = false;
        Dialogue.text = "Okay, hand over the skulls and wait a mo...";
        
        if (finishedQuest == false)
        {
            SubtractSkulls(22);
            StartCoroutine(Wait(5));
            finishedQuest = true;
        }
    }

    private void SubtractSkulls(int skullsToSubtract)
    {
        player.GetComponent<PlayerUI>().SubtractSkulls(skullsToSubtract);
    }
    
    // Main Skull Quest Interaction
    private void SkullScenario()
    {
        Skulls = player.GetComponent<PlayerUI>().skullCount;
        Refresh();
        buttonActive(false, false, false);
        
        // If player hasn't any skulls yet
        if (Skulls == 0 && finishedQuest != true)
        {
            buttonActive(true, true, true);

            Dialogue.text = "Come back with some skulls if you want to trade!";
            button1Text.text = "Rules please";
            button2Text.text = "Okay";
            button3Text.text = "See totals";

            Button1.onClick.AddListener( () => ParameterOnClickYesRules($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
            Button3.onClick.AddListener( () => ParameterOnClickTotals($"{nameof(Button3)} was pressed!"));
        }

        // If player has collected all skulls
        if (Skulls == SkullsTotal)
        {
            buttonActive(true, false, false);

            Dialogue.text = $"A-ha so you've finally collected {Skulls} skulls! Well... a deals a deal, eh?";
            button1Text.text = "Hand it over";
            Button1.onClick.AddListener( () => ParameterOnClickYesFinished($"{nameof(Button1)} was pressed!"));
        }


        // If player has some skulls
        if (finishedQuest == false && subtractingFinished == false)
        {
            if (((Skulls > 0) && (Skulls < SkullsTotal) && (Skulls != SkullsTotal)))
            {
                buttonActive(true, true, true);

                Dialogue.text = $"You've found some skulls! But you still need {(SkullsTotal - Skulls)} more to trade!";
                button1Text.text = "Rules please";
                button2Text.text = "I'll be back.";
                button3Text.text = "See totals";
            
                Button1.onClick.AddListener( () => ParameterOnClickYesRules($"{nameof(Button1)} was pressed!"));
                Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
                Button3.onClick.AddListener( () => ParameterOnClickTotals($"{nameof(Button3)} was pressed!"));
            }
        }

        // If quest is completed
        else if (Skulls > 22 && finishedQuest == true)
        {
            buttonActive(false, true, true);

            Dialogue.text = "Sorry, but I'm fresh out of stock! Thanks for the spicy trade though... heheheh...";
            button2Text.text = "Too bad! Bye!";
            button3Text.text = "See totals";
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
            Button3.onClick.AddListener( () => ParameterOnClickTotals($"{nameof(Button3)} was pressed!"));
        }

        else
        {
            buttonActive(false, false, false);
            Dialogue.text = "Well go on, it's right there!";
        }
    }
    
    // Text trigger to identify shop
    public void OnHoverEnter()
    {
        NameTag.SetActive(true);
    }
    
    // Disables text when not hovering over
    public void OnHoverExit()
    {
        NameTag.SetActive(false);
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

    // Spawns rewards after skulls have been taken
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        GoldenSkull.transform.position = new Vector3(239, 5, -100);
        Dialogue.text = $"So ta-dah! One big shiny thing for lots of small shiny things. Enjoy, I guess?";
        finishedQuest = true;
        subtractingFinished = true;
    }
    
    // Exit goodbyes
    private void WaitExit()
    {
        if (skullQuestAccepted == false)
        {
            skullQuestAccepted = false;
            Dialogue.text = "Too bad. Come back if you change your mind";
        }
        
        else if (finishedQuest == false)
        {
            Dialogue.text = $"Bye! \nRemember: \n{(SkullsTotal - Skulls)} more Skulls!";
        }

        else
        {
            Dialogue.text = "See you around, friend!";
        }
    }

    // Formatting soft reset
    private void Refresh()
    {
        Dialogue.fontSize = 40;
        SkullIcons.SetActive(false);
    }
}
