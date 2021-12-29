using System.Collections;
using FMODUnity;
using Team5.Control;
using Team5.Core;
using Team5.Inventories.Control.sample;
using Team5.Movement;
using Team5.Ui;
using UnityEngine;

public class InteractableBarrierController : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    
    [SerializeField] private Texture2D lockedCursor;
    [SerializeField] private Texture2D unlockedCursor;
    [SerializeField] private float distanceToOpenDoor;
    [SerializeField] private RoomController ConnectedRoom1;
    [SerializeField] private RoomController ConnectedRoom2;


    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private IOpenLogic openLogicScript;
    private GameObject player;
    private MouseController mouseController;
    
    private Vector3 TargetPosition;

    public StudioEventEmitter LockedDoor;


    private bool isLocked = false;
    private bool waitForSound;
    public Texture2D mouseTexture => isLocked ? lockedCursor : unlockedCursor;
    
    // This is a variable holding the specific coroutine, allowing us to cancel it.
    private IEnumerator goToAndOpen;


    
    public bool IsLocked
    {
        set
        {
            if (TryGetComponent(out OutlineController outlineController))
            {
                // If the object is locked we use the alternate color.
                outlineController.UseAlternateColor(value);
            }

            isLocked = value;
        }
    }
    
    
    
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
    }
    
    
    
    public void OnHoverEnter()
    {
    }
    
    public void OnHoverExit()
    {
    }

    

    public void OnClick(Vector3 mouseClickVector)
    {
        if (isLocked)
        {
            if (!waitForSound)
            {
                waitForSound = true;
                StartCoroutine(WaitForSound());
            }
            //TODO Fix So the gate does not sound same as the door. 
            LockedDoor.Play();
            return;
        }

        // #############################################################################################################
        // TODO: THIS IS UGLY. LOOK INTO GETTING THE SHORTEST PATH INSTEAD.
        // Very ugly
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



    private IEnumerator GoToAndOpen()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.25f);

            if (Vector3.Distance(player.transform.position, TargetPosition) < distanceToOpenDoor)
            {
                openLogicScript.Open();
                
                TryToActivateRooms();
                
                // Deactivate so the barrier can no longer be interacted with.
                GetComponent<BoxCollider>().enabled = false;
                unlockedCursor = null;
                lockedCursor = null;
                
                break;
            }
        }
    }



    void TryToActivateRooms()
    {
        if (ConnectedRoom1 != null)
            ConnectedRoom1.ActivateRoom();
        if (ConnectedRoom2 != null)
            ConnectedRoom2.ActivateRoom();
    }
    
    
    
    // Event subscriber that will stop the system for entering the door, if the player clicked something else.
    void ChangedTarget(object sender, bool temp)
    {
        StopCoroutine(goToAndOpen);
    }
     
    
    
    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(1);
        waitForSound = false;
    }
}
