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
    private GameObject ShopTalk;
    public GameObject GoldenSkull;
    private TMP_Text Dialogue;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    
    // skulls
    private int Skulls;
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
    private bool finishedQuest = false;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ShopTalk = FindObjectOfType<HUD>().ShopText;
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;
        SkullIcons = FindObjectOfType<HUD>().SkullIcons;
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        NameTag = this.gameObject.transform.Find("Name Tag").gameObject;
        
        button1 = Button1.gameObject;
        button2 = Button2.gameObject;
        button3 = Button3.gameObject;

        button1Text = Button1.GetComponentInChildren<TextMeshProUGUI>();
        button2Text = Button2.GetComponentInChildren<TextMeshProUGUI>();
        button3Text = Button3.GetComponentInChildren<TextMeshProUGUI>();
        
        skullQuestRulesRead = false;
        skullQuestAccepted = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        Refresh();
        
        if (other.gameObject == player)
        {
            ShopTalk.SetActive(true);
            buttonActive(true, true, false);

            if (firstVisit == true)
            {
                firstVisit = false;
                Dialogue.text = "Hi there, welcome!";
                button1Text.text = "Hello friend!";
                button2Text.text = "Not interested";
            }
            else
            {
                Dialogue.text = "Hi again, friend!";
                button1Text.text = "Oh hai!";
                button2Text.text = "Not today";
            }
            Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
    }
    
    private void ParameterOnClickYesStart(string test)
    {
        if (skullQuestAccepted == false )
        {
            buttonActive(true, true, false);
        
            Dialogue.text = "Want to do the skull quest?";
            button1Text.text = "Sure!";
            button2Text.text = "Maybe later";
                    
            Button1.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
        }
        
        else if (skullQuestRulesRead == false)
        {
            SkullScenarioRules();
        }
        
        else if (skullQuestAccepted == true && skullQuestRulesRead == true)
        {
            SkullScenario();
        }
    }

    private void ParameterOnClickNoStart(string test)
    {
        Refresh();
        buttonActive(false, false, false);
        WaitExit();
    }
    
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
    
    private void ParameterOnClickYesRules(string test)
    {
        SkullScenarioRules();
    }

    private void SkullScenarioRules()
    {
        buttonActive(true, false, false);
        
        Dialogue.text = "See this big golden skull behind me? If you want it collect 22 skulls for me.";
        button1Text.text = "Continue";
        
        Button1.onClick.AddListener( () => ParameterOnClickYesRules2($"{nameof(Button1)} was pressed!"));
    }

    private void ParameterOnClickYesRules2(string test)
    {
        skullQuestRulesRead = true;
        
        Dialogue.text = "White skulls =	1 \nRed skulls =	5 \nPurple skulls =	10";
        button1Text.text = "Okay, I get it!";

        Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
    }

    private void ParameterOnClickTotals(string test)
    {
        whiteSkulls = player.GetComponent<PlayerUI>().whiteSkulls;
        redSkulls = player.GetComponent<PlayerUI>().redSkulls;
        purpleSkulls = player.GetComponent<PlayerUI>().purpleSkulls;
        goldSkulls = player.GetComponent<PlayerUI>().goldSkulls;

        buttonActive(true, true, false);
        SkullIcons.SetActive(true);
        Dialogue.fontSize = 30;
        Dialogue.text = $"White skulls =	{whiteSkulls} / {whiteSkullsTotal} \n" +
                        $"Red Skulls =	{redSkulls} / {redSkullsTotal} \n" +
                        $"Purple Skulls =	{purpleSkulls} / {purpleSkullsTotal} \n" +
                        $"Gold Skulls =	{goldSkulls} / {goldSkullsTotal}";

        button1Text.text = "Return";
        button2Text.text = "Leave";


        Button1.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
    }
    
    private void SkullScenario()
    {
        Skulls = player.GetComponent<PlayerUI>().skullCount;
        Refresh();
        buttonActive(false, false, false);
        
        if (Skulls == 0)
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

        else if (Skulls == 22)
        {
            Dialogue.text = $"A-ha so you've finally collected {Skulls} skulls! Well... a deals a deal, eh?";
            finishedQuest = true;

            // need to fix a way of specifying no. of skulls to subtract here:
            player.GetComponent<PlayerUI>().SubtractSkulls();
            StartCoroutine(Wait(5));
        }

        else if (Skulls > 0 && Skulls < 22)
        {
            buttonActive(true, true, true);

            Dialogue.text = $"You've found some skulls! But you still need {(22 - Skulls)} more to trade!";
            button1Text.text = "Rules please";
            button2Text.text = "I'll be back.";
            button3Text.text = "See totals";
            
            Button1.onClick.AddListener( () => ParameterOnClickYesRules($"{nameof(Button1)} was pressed!"));
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
            Button3.onClick.AddListener( () => ParameterOnClickTotals($"{nameof(Button3)} was pressed!"));
        }

        else
        {
            buttonActive(false, true, true);

            Dialogue.text = "Sorry, but I'm fresh out of stock! Thanks for the spicy trade though... heheheh...";
            button2Text.text = "Too bad! Bye!";
            button3Text.text = "See totals";
            Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
            Button3.onClick.AddListener( () => ParameterOnClickTotals($"{nameof(Button3)} was pressed!"));

        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            ShopTalk.SetActive(false);
        }
    }
    
    public void OnHoverEnter()
    {
        NameTag.SetActive(true);
    }
    
    public void OnHoverExit()
    {
    }

    void OnMouseExit()
    {
        NameTag.SetActive(false);
    }
    
    
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
    
    private void buttonActive(bool buttonOne, bool buttonTwo, bool buttonThree)
    {
        button1.SetActive(buttonOne);
        button2.SetActive(buttonTwo);
        button3.SetActive(buttonThree);
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        GoldenSkull.transform.position = new Vector3(239, 5, -100);
        Dialogue.text = $"So ta-dah! One big shiny thing for lots of small shiny things. Enjoy, I guess?";
    }

    private void WaitExit()
    {
        if (skullQuestAccepted == false)
        {
            skullQuestAccepted = false;
            Dialogue.text = "Too bad. Come back if you change your mind";
        }
        
        else if (finishedQuest == false)
        {
            Dialogue.text = $"Bye! \nRemember: \n{(22 - Skulls)} more Skulls!";
        }

        else
        {
            Dialogue.text = "See you around, friend!";
        }
    }

    private void Refresh()
    {
        Dialogue.fontSize = 40;
        SkullIcons.SetActive(false);
    }
}
