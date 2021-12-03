using System.Collections;
using Entities.Player;
using Logic;
using Team5.Movement;
using UnityEngine;

public class InteractableBarrierController : MonoBehaviour, IInteractable
{
    [SerializeField] private Texture2D lockedCursor;
    [SerializeField] private Texture2D unlockedCursor;
    [SerializeField] private float distanceToOpenDoor;


    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private IOpenLogic openLogicScript;
    private GameObject player;
    private bool isLocked = true;
    private MouseController mouseController;
    private bool waitForSound;


    private Vector3 TargetPosition;
    
    
    public Texture2D mouseTexture => isLocked ? lockedCursor : unlockedCursor;


    private void Awake()
    {
        goToAndOpen = GoToAndOpen();
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Get the mousecontroller and subscribe to their event.
        mouseController = FindObjectOfType<MouseController>();
        mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        
        openLogicScript = GetComponent<IOpenLogic>();
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        
        
        
        // Temporary 
        StartCoroutine(UnlockDoor());
    }

    
    
    public void OnHover()
    {
        if (isLocked && !waitForSound)
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(WaitForSound());
        }
    }

    
    
    public void OnClick(Vector3 mouseClickVector)
    {
        if (isLocked)
            return;
        


        // #############################################################################################################
        // TODO: THIS IS UGLY. LOOK INTO GETTING THE SHORTEST PATH INSTEAD.
        // Very ugly
        // Very inefficient
        // Very very bad

        var reachable = GameObject.Find("Player").GetComponent<Move>().TargetReachable(playerTargetPosition.position);
        var reachableTwo = GameObject.Find("Player").GetComponent<Move>().TargetReachable(playerTargetPositionTwo.position);

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

        // #############################################################################################################
        
        
        
        StartCoroutine(goToAndOpen);
        if (Vector3.Distance(player.transform.position, TargetPosition) > distanceToOpenDoor)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(TargetPosition);
    }

    
    
    private IEnumerator goToAndOpen;
    private IEnumerator GoToAndOpen()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Debug.Log("Hi times up");

            if (Vector3.Distance(player.transform.position, TargetPosition) < distanceToOpenDoor)
            {
                openLogicScript.Open();

                unlockedCursor = null;
                lockedCursor = null;
                
                break;
            }
        }
    }

    
    
    // Event subscriber that will stop the system for entering the door, if the player clicked something else.
    void ChangedTarget(object sender, bool temp)
    {
        StopCoroutine(goToAndOpen);
    }
    
    
    
    IEnumerator WaitForSound()
    {
        waitForSound = true;
        yield return new WaitForSeconds(1);
        waitForSound = false;
    }
    
    
    
    // TEMPORARY TIMER FOR UNLOCKING!
    IEnumerator UnlockDoor()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
    }
}
