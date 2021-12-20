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

    [SerializeField] private float distanceToShop;
    [SerializeField] private Texture2D shopCursor;

    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition;
    public Texture2D mouseTexture => shopCursor;
    private GameObject NameTag;
    private GameObject ShopTalk;
    private int Skulls;
    public GameObject GoldenSkull;
    private TMP_Text Dialogue;

    [SerializeField] public Button yesButton = null;
    [SerializeField] public Button noButton = null;
    private GameObject button1;
    private GameObject button2;
    private bool firstVisit = true;

    private TMP_Text button1Text;
    private TMP_Text button2Text;
    private bool skullQuestOffered = false;
    private bool skullQuestAccepted = false;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ShopTalk = FindObjectOfType<HUD>().ShopText;
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        NameTag = this.gameObject.transform.Find("Name Tag").gameObject;
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;
        
        button1 = yesButton.gameObject;
        button2 = noButton.gameObject;

        button1Text = yesButton.GetComponentInChildren<TextMeshProUGUI>();
        button2Text = noButton.GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Update()
    {
        Skulls = player.GetComponent<PlayerUI>().skullCount;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            ShopTalk.SetActive(true);
            
            buttonActive(true, true);

            if (firstVisit == true)
            {
                firstVisit = false;
                Dialogue.text = "Hi there welcome!";
                button1Text.text = "Hello friend!";
                button2Text.text = "Not interested.";
            }

            else
            {
                Dialogue.text = "Hi again!";
                button1Text.text = "Oh hai!";
                button2Text.text = "... nope.";
            }
                
            yesButton.onClick.AddListener(delegate { ParameterOnClickYesStart("Yes button was pressed!"); });
            noButton.onClick.AddListener(delegate { ParameterOnClickNoStart("No button was pressed!"); });
        }
    }
    
    private void ParameterOnClickYesStart(string test)
    {
        bool skullQuestOffered = true;

        if (skullQuestAccepted == false)
        {
            Dialogue.text = "Want to do the skull quest?";
            button1Text.text = "Sure!";
            button2Text.text = "Maybe later.";
            
            yesButton.onClick.AddListener(delegate { ParameterOnClickYes("Yes Button was pressed!"); });
            noButton.onClick.AddListener(delegate { ParameterOnClickNoStart("No button was pressed!"); });
        }

        else
        {
            SkullScenario();
        }
        
    }

    private void ParameterOnClickNoStart(string test)
    {
        buttonActive(false, false);
        WaitExit();
    }
    
    private void ParameterOnClickYes(string test)
    {
        skullQuestAccepted = true;
        Dialogue.text = "Want to hear the rules?";
        
        { 
            button1Text.text = "Sure";
            button2Text.text = "No thanks";
            
            yesButton.onClick.AddListener(delegate { ParameterOnClickYesRules("Yes Button was pressed!"); });
            noButton.onClick.AddListener(delegate { ParameterOnClickYesStart("No button was pressed!"); });
        }
    }
    
    private void ParameterOnClickYesRules(string test)
    {
        SkullScenarioRules();
    }

    private void SkullScenarioRules()
    {
        buttonActive(true, false);
        
        Dialogue.text = "See this big golden skull behind me? If you want it collect 22 skulls for me.";
        button1Text.text = "Continue";
        
        yesButton.onClick.AddListener(delegate { ParameterOnClickYesRules2("Yes Button was pressed!"); });
    }

    private void ParameterOnClickYesRules2(string test)    {

        Dialogue.text = "White skulls = 1 \nRed skulls = 5 \nPurple skulls = 10";
        button1Text.text = "Okay. I got it.";
        
        yesButton.onClick.AddListener(delegate { ParameterOnClickYesStart("Yes Button was pressed!"); });
    }
    
    private void SkullScenario()
    {
        buttonActive(false, false);
        
        if (Skulls == 0)
        {
            buttonActive(true, true);
            
            Dialogue.text = "Come back with some skulls if you want to trade!";
            button1Text.text = "Rules please";
            button2Text.text = "Okay";
            
            yesButton.onClick.AddListener(delegate { ParameterOnClickYesRules("Yes Button was pressed!"); });
            noButton.onClick.AddListener(delegate { ParameterOnClickNoStart("No button was pressed!"); });
        }

        else if (Skulls == 22)
        {
            Dialogue.text = $"A-ha so you've finally collected {Skulls} skulls! Well... a deals a deal, eh?";
           

            // need to fix a way of specifying no. of skulls to subtract here:
            player.GetComponent<PlayerUI>().SubtractSkulls();
            StartCoroutine(Wait(5));
        }

        else if (Skulls > 0 && Skulls < 22)
        {
            buttonActive(true, true);

            Dialogue.text = $"You've found some skulls! But you still need {(22 - Skulls)} more to trade!";
            button1Text.text = "Rules please";
            button2Text.text = "I'll be back.";
            
            yesButton.onClick.AddListener(delegate { ParameterOnClickYesRules("Yes Button was pressed!"); });
            noButton.onClick.AddListener(delegate { ParameterOnClickNoStart("No button was pressed!"); });
        }

        else
        {
            buttonActive(false, true);

            Dialogue.text = "Sorry, but I'm fresh out of stock! Thanks for the spicy trade though... heheheh...";
            button2Text.text = "Too bad! Bye!";
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
    
    private void buttonActive(bool buttonOne, bool buttonTwo)
    {
        button1.SetActive(buttonOne);
        button2.SetActive(buttonTwo);
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
            Dialogue.text = "Too bad. Come back if you change your mind";
        }
        
        else
        {
            Dialogue.text = $"Bye! \nRemember: \n{(22 - Skulls)} more Skulls!";
        }
    }
}
