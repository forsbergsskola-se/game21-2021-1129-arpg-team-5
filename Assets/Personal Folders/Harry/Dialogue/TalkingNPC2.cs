using Team5.Core;
using Team5.Movement;
using Team5.Ui;
using UnityEngine;
using UnityEngine.UI;

public class TalkingNPC2 : MonoBehaviour, IInteractable
{
    // positioning player
    [SerializeField] private float distanceToNPC;
    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition;
    private GameObject DialogueButton;
    private GameObject RepeatButton;
    private Button Repeat;

    private GameObject Button1;
    private GameObject Button2;
    private GameObject Button3;

    // mouse cursor logic
    public Texture2D mouseTexture => shopCursor;
    [SerializeField] private Texture2D shopCursor;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;

        DialogueButton = FindObjectOfType<HUD>().ContinueButton.gameObject;
        Repeat = FindObjectOfType<HUD>().RepeatButton;
        RepeatButton = Repeat.gameObject;
        
        Button1 = FindObjectOfType<HUD>().Button1.gameObject;
        Button2 = FindObjectOfType<HUD>().Button2.gameObject;
        Button3 = FindObjectOfType<HUD>().Button3.gameObject;
    }
    
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            buttonActive(false, false, false);
            FindObjectOfType<HUD>().HudUIActive(true,true, true,false,true, true);
            DialogueButton.SetActive(true);
            this.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
    
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

    public void ReplayOption()
    {
        RepeatButton.SetActive(true);
        Repeat.onClick.AddListener( () => ParameterOnClick($"{nameof(Repeat)} was pressed!"));
    }


    private void ParameterOnClick(string test)
    {
        RepeatButton.SetActive(false);
        this.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void StopTalk()
    {
        DialogueButton.SetActive(false);
        RepeatButton.SetActive(false);
        FindObjectOfType<HUD>().HudUIActive(true,true, true,false,false, true);
    }
    
    private void buttonActive(bool buttonOne, bool buttonTwo, bool buttonThree)
    {
        Button1.SetActive(buttonOne);
        Button2.SetActive(buttonTwo);
        Button3.SetActive(buttonThree);
    }
}