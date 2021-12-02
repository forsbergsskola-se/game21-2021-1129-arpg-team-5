using System.Collections;
using Entities.Player;
using Logic;
using Team5.Movement;
using UnityEngine;

public class DoorControll : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform playerTargetPosition;
    [SerializeField] private Texture2D lockedCursor;
    [SerializeField] private Texture2D unlockedCursor;
    [SerializeField] private float distanceToOpenDoor;



    private IOpenLogic openLogicScript;
    private GameObject player;
    private bool isLocked = true;
    private MouseController mouseController;
    private bool waitForSound;
    
    
    
    public Texture2D mouseTexture => isLocked ? lockedCursor : unlockedCursor;
    
    
    
    void Awake()
    {
        goThroughDoor = GoThroughDoor();
        player = GameObject.FindGameObjectWithTag("Player");
        
        
        // Get the mousecontroller and subscribe to their event.
        mouseController = FindObjectOfType<MouseController>();
        mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.


        openLogicScript = GetComponent<IOpenLogic>();
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        
        
        
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
        
        StartCoroutine(goThroughDoor);
        if (Vector3.Distance(player.transform.position, this.transform.position) > distanceToOpenDoor)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(playerTargetPosition.position);
    }

    
    
    private IEnumerator goThroughDoor;
    private IEnumerator GoThroughDoor()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Debug.Log("Hi times up");

            if (Vector3.Distance(player.transform.position, playerTargetPosition.position) < distanceToOpenDoor)
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
        StopCoroutine(goThroughDoor);
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
        yield return new WaitForSeconds(1);
        isLocked = false;
    }
}
