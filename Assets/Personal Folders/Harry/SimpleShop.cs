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

public class SimpleShop : MonoBehaviour, IInteractable
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
    
    // interactions
    private TMP_Text Dialogue;
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
    private bool canPurchase = false;
    
    // First Encounter

    public string greetText;
    public string greetAccept;
    public string greetReject;
    
    // Generic Hello

    public string genericHello;
    public string helloAccept;
    public string helloReject;
    
    // Shop Main

    public string checkWares;
    public string shopAgree;
    public string goBack;
    public string leaveShop;
    
    // Buy Menu
    // TODO: Add Out of stock

    public string goToPurchase;
    public string checkPuchase;
    public string confirmPurchase;
    public string stopPurchase;
    public string notEnoughMoney;
    public string goBackToShop;

    private int playerMoney;
    
    // Purchase success menu

    public string purchaseSuccess;
    public string carryOnShopping;
    public string thatsAllThanks;

    // Items

    public string item1;
    public int item1stock;
    public int item1cost;
    private bool item1selected = false;
    
    public string item2;
    public int item2stock;
    public int item2cost;
    private bool item2selected = false;
    
    public string item3;
    public int item3stock;
    public int item3cost;
    private bool item3selected = false;
    
    public string item4;
    public int item4stock;
    public int item4cost;
    private bool item4selected = false;

    // Goodbye

    public string goodbye;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;

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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Refresh();
            
            // multiple HUD elements enabled and disabled onEnter
            FindObjectOfType<HUD>().HudUIActive(false,false, true, false, true, false);
            buttonActive(true, true, false);

            // Only first time dialogue
            if (firstVisit == true)
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
            FindObjectOfType<HUD>().HudUIActive(true,true, true,false,false, true);
        }
    }
    
    // Button 1
    private void ParameterOnClickYesStart(string test)
    {
        buttonActive(true, true, false);
        
        Dialogue.text = $"{checkWares}";
        button1Text.text = $"{shopAgree}";
        button2Text.text = $"{leaveShop}";
                    
        Button1.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
    }
    
    // Shop Main
    private void ParameterOnClickYes(string test)
    {
        // Changes formatting + adds icons
        Dialogue.fontSize = 30;

        Dialogue.text = $"{item1}   # {item1stock}    £ {item1cost} \n" +
                        $"{item2}   # {item2stock}    £ {item2cost} \n" +
                        $"{item3}   # {item3stock}    £ {item3cost} \n" +
                        $"{item4}   # {item4stock}    £ {item4cost} \n";
        
        // need to add UI for enabling items here
        
        // need logic for shop buttons/ single purchases & out of stock here

        // TODO: this is dummy text before method is implemented
        
        item1selected = true;
        
        buttonActive(true, true, false);
        button1Text.text = $"{goToPurchase}";
        button2Text.text = $"{goBack}";
        
        Button1.onClick.AddListener( () => ParameterOnClickPurchase($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickYesStart($"{nameof(Button2)} was pressed!"));
        
    }

    // Purchase screen
    private void ParameterOnClickPurchase(string test)
    {
        // need to add UI for disabling items here
        
        //Check Player's Wallet
        playerMoney = player.GetComponent<Wallet>().Coins;
        
        if (item1selected)
        {
            if (playerMoney >= item1cost)
            {
                buttonActive(true, true, false);
                canPurchase = true;
                
                Dialogue.text = $"So you want to buy {item1} for {item1cost} coins? \n" + 
                                $"You will have {(playerMoney - item1cost)} coins left.";
                
                button1Text.text = $"{goToPurchase}";
                button2Text.text = $"{goBackToShop}";
                Button1.onClick.AddListener( () => ParameterOnClickConfirmPurchase($"{nameof(Button1)} was pressed!"));
                Button2.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button2)} was pressed!"));
            }

            else
            {
                NotEnoughMoney();
            }
        }
        
        else if (item2selected)
        {
            if (playerMoney >= item2cost)
            {
                buttonActive(true, true, false);
                canPurchase = true;
                
                Dialogue.text = $"So you want to buy {item2} for {item2cost} coins? \n" + 
                                $"You will have {(playerMoney - item2cost)} coins left.";
                
                button1Text.text = $"{goToPurchase}";
                button2Text.text = $"{goBackToShop}";
                Button1.onClick.AddListener( () => ParameterOnClickConfirmPurchase($"{nameof(Button1)} was pressed!"));
                Button2.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button2)} was pressed!"));
            }

            else
            {
                NotEnoughMoney();
            }
        }
        
        else if (item3selected)
        {
            if (playerMoney >= item3cost)
            {
                buttonActive(true, true, false);
                canPurchase = true;
                
                Dialogue.text = $"So you want to buy {item3} for {item3cost} coins? \n" + 
                                $"You will have {(playerMoney - item3cost)} coins left.";
                
                button1Text.text = $"{goToPurchase}";
                button2Text.text = $"{goBackToShop}";
                Button1.onClick.AddListener( () => ParameterOnClickConfirmPurchase($"{nameof(Button1)} was pressed!"));
                Button2.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button2)} was pressed!"));
            }

            else
            {
                NotEnoughMoney();
            }
        }
        
        else if (item4selected)
        {
            if (playerMoney >= item4cost)
            {
                buttonActive(true, true, false);
                canPurchase = true;
                
                Dialogue.text = $"So you want to buy {item4} for {item4cost} coins? \n" + 
                                $"You will have {(playerMoney - item4cost)} coins left.";
                
                button1Text.text = $"{goToPurchase}";
                button2Text.text = $"{goBackToShop}";
                Button1.onClick.AddListener( () => ParameterOnClickConfirmPurchase($"{nameof(Button1)} was pressed!"));
                Button2.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button2)} was pressed!"));
            }

            else
            {
                NotEnoughMoney();
            }
        }
    }

    private void NotEnoughMoney()
    {
        canPurchase = false;
        buttonActive(true, false, false);
        Dialogue.text = $"{notEnoughMoney}";
        Button1.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button1)} was pressed!"));
    }

    private void ParameterOnClickConfirmPurchase(string test)
    {
        buttonActive(true, true, false);
        Dialogue.text = $"{checkPuchase}";
        button1Text.text = $"{confirmPurchase}";
        button2Text.text = $"{stopPurchase}";
        
        Button1.onClick.AddListener( () => ParameterOnClickPurchaseSuccess($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button2)} was pressed!"));
    }

    private void ParameterOnClickPurchaseSuccess(string test)
    {
        if (item1selected || item2selected || item3selected || item4selected)
        {
            FinishPurchase();
        }
        
        buttonActive(true, true, false);
        Dialogue.text = $"{purchaseSuccess}";
        button1Text.text = $"{carryOnShopping}";
        button2Text.text = $"{thatsAllThanks}";
        
        Button1.onClick.AddListener( () => ParameterOnClickYes($"{nameof(Button1)} was pressed!"));
        Button2.onClick.AddListener( () => ParameterOnClickNoStart($"{nameof(Button2)} was pressed!"));
    }


    private void FinishPurchase()
    {
        if (item1selected)
        {
            //TODO: being called too many times somehow
            
            player.GetComponent<Wallet>().SubtractMoney(item1cost);
            item1stock -= 1;
            // give the player the item
            item1selected = false;
            return;
        }
        
        if (item2selected)
        {
            player.GetComponent<Wallet>().SubtractMoney(item2cost);
            item2stock -= 1;
            // give the player the item
            item2selected = false;
            return;
        }
        
        if (item3selected)
        {
            player.GetComponent<Wallet>().SubtractMoney(item3cost);
            item3stock -= 1;
            // give the player the item
            item3selected = false;
            return;
        }
        
        if (item4selected)
        {
            player.GetComponent<Wallet>().SubtractMoney(item4cost);
            item4stock -= 1;
            // give the player the item
            item4selected = false;
            return;
        }
    }
    

    
    // Triggers exit text
    private void ParameterOnClickNoStart(string test)
    {
        Refresh();
        buttonActive(false, false, false);
        Dialogue.text = $"{goodbye}";
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
    

    // Formatting soft reset
    private void Refresh()
    {
        Dialogue.fontSize = 40;
    }
}
