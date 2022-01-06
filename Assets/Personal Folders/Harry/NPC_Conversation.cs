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

public class NPC_Conversation : MonoBehaviour, IInteractable
{
    // positioning player
    [SerializeField] private float distanceToNPC;
    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition;
   
    // mouse cursor logic
    public Texture2D mouseTexture => shopCursor;
    [SerializeField] private Texture2D shopCursor;
    
    // interactions
    private TMP_Text Dialogue;
    
    private Button Button1;
    private GameObject button1;
    private TMP_Text button1Text;
    
    private Button Button2;
    private GameObject button2;
    private TMP_Text button2Text;
    
    private Button Button3;
    private GameObject button3;
    
    // bools
    private bool firstVisit = true;

    // DIALOGUE OPTIONS
    
    // First Encounter

    private GameObject NPCNameUI;
    public string npcName;

    public string greetText;
    public string greetReply;
    public string genericHello;
    public string genericReply;
    public string firstLine;
    public string secondLine;
    public string thirdLine;
    public string repeatButton;
    public string exitButton;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;
        NPCNameUI = FindObjectOfType<HUD>().NPCName;
        npcName = NPCNameUI.GetComponentInChildren<TMP_Text>().text;
        
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
    }
    
    // Activated when Player enters invisible collider

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Refresh();
            // multiple HUD elements enabled and disabled onEnter
            FindObjectOfType<HUD>().HudUIActive(false,false, false, false,true, false);
            buttonActive(true, false, false);

            // Only first time dialogue
            if (firstVisit == true)
            {
                firstVisit = false;
                Dialogue.text = $"{greetText}";
                button1Text.text = $"{greetReply}";
            }
            
            // Every other time
            else
            {
                Dialogue.text = $"{genericHello}";
                button1Text.text = $"{genericReply}";
            }
            
            // Detects button clicks
            Button1.onClick.AddListener( () => ParameterOnClick1($"{nameof(Button1)} was pressed!"));
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
    
    // Rules text 1
    private void ParameterOnClick1(string test)
    {
        buttonActive(true, false, false);
        firstVisit = false;
        
        Dialogue.text = $"{firstLine}";
        button1Text.text = "Continue";
        
        Button1.onClick.AddListener( () => ParameterOnClick2($"{nameof(Button1)} was pressed!"));
    }

    private void ParameterOnClick2(string test)
    {
        Dialogue.text = $"{secondLine}";
        Button1.onClick.AddListener( () => ParameterOnClick3($"{nameof(Button1)} was pressed!"));

    }
    
    private void ParameterOnClick3(string test)
    {
        buttonActive(true, true, false);
        Dialogue.text = $"{thirdLine}";
        button1Text.text = $"{repeatButton}";
        button2Text.text = $"{exitButton}";

        Button1.onClick.AddListener( () => ParameterOnClick1($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickExit($"{nameof(Button2)} was pressed!"));
    }

    private void ParameterOnClickExit(string test)
    {
        buttonActive(false, false, false);
        FindObjectOfType<HUD>().HudUIActive(true,true, true,false,false, true);
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

        if (Vector3.Distance(player.transform.position, TargetPosition) > distanceToNPC)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(TargetPosition);
    }
    
    // Multi-button quick set-active method for hiding and showing different combos
    private void buttonActive(bool buttonOne, bool buttonTwo, bool buttonThree)
    {
        button1.SetActive(buttonOne);
        button2.SetActive(buttonTwo);
        button3.SetActive(buttonThree);
    }
    

    // Formatting soft reset
    private void Refresh()
    {
        Dialogue.fontSize = 40;
    }
}
