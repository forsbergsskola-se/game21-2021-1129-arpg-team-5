using Team5.Core;
using Team5.Movement;
using Team5.Ui;
using UnityEngine;

public class TalkingNPC : MonoBehaviour, IInteractable
{
    // positioning player
    [SerializeField] private float distanceToNPC;
    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition;
    private GameObject DialogueButton;
    
    private GameObject button1;
    private GameObject button2;
    private GameObject button3;

    // mouse cursor logic
    public Texture2D mouseTexture => shopCursor;
    [SerializeField] private Texture2D shopCursor;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        var dialogueBox =  FindObjectOfType<HUD>().ShopDialogue.gameObject;
        
        DialogueButton = dialogueBox.transform.Find("Continue Button").gameObject;
        button1 = dialogueBox.transform.Find("Button 1").gameObject;
        button2 = dialogueBox.transform.Find("Button 2").gameObject;
        button3 = dialogueBox.transform.Find("Button 3").gameObject;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {            
            buttonActive(false, false, false);
            FindObjectOfType<HUD>().HudUIActive(true,true, true,false,true, true);
            DialogueButton.SetActive(true);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }
    
    // When walking away from shop
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            // Activates and deactivates HUD elements onExit
            FindObjectOfType<HUD>().HudUIActive(true,true, true,false,false, true);
            DialogueButton.SetActive(false);
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

        if (Vector3.Distance(player.transform.position, TargetPosition) > distanceToNPC)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(TargetPosition);
    }
    
    private void buttonActive(bool buttonOne, bool buttonTwo, bool buttonThree)
    {
        button1.SetActive(buttonOne);
        button2.SetActive(buttonTwo);
        button3.SetActive(buttonThree);
    }
}