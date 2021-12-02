using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Logic;
using Team5.Movement;
using UnityEngine;

public class DoorControll : MonoBehaviour, IInteractable
{
    [SerializeField] private Texture2D lockedCursor;
    [SerializeField] private Texture2D unlockedCursor;
    [SerializeField] private float DistanceToOpenDoor;
    [SerializeField] private float TimeBeforeEnter; // TODO: To be used to make the player wait a little before entering the new area.
    [SerializeField] private float TimeToCloseDoor;
    
    
    // private Animator Dooropen;
    [SerializeField] private GameObject animation;
    
    private GameObject player;
    private bool isLocked = true;
    private MouseController mouseController;
    private static readonly int IsOpenId = Animator.StringToHash("isOpen");
    
    
    public Texture2D mouseTexture => isLocked ? lockedCursor : unlockedCursor;


    // TODO LIST:
    // Check player distance to door. [DONE]
        
    // If door is close [DONE]
    // Open door [DONE]
        
    // If door not close [DONE]
        
    // Tell stupid player to get closer to door. [DONE]
        
    // Then if player is close enough to door [DONE]
    // Open door [DONE]
        
    // When door is open []
    // Tell player to move through the door []
        
    // When the player has moved through the door []
    // Close the door behind them. [Kind of done, it's on a timer.]
    
    
    
    void Awake()
    {
        goThroughDoor = GoThroughDoor();
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Dooropen = gameObject.GetComponent<Animator>();
        
        // Get the mousecontroller and subscribe to their event.
        mouseController = FindObjectOfType<MouseController>();
        mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        
        
        
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
        if (Vector3.Distance(player.transform.position, this.transform.position) > DistanceToOpenDoor)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(mouseClickVector);
    }

    
    
    private IEnumerator goThroughDoor;
    private IEnumerator GoThroughDoor()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Debug.Log("Hi times up");

            if (Vector3.Distance(player.transform.position, this.transform.position) < DistanceToOpenDoor)
            {
                OpenAndCloseDoor();
                
                // TODO: Somewhere here we need to move the player to the other side of the door.
                
                StopCoroutine(goThroughDoor);
            }
        }
    }

    
    
    // Event subscriber that will stop the system for entering the door, if the player clicked something else.
    void ChangedTarget(object sender, bool temp)
    {
        StopCoroutine(goThroughDoor);
    }

    
    
    IEnumerator CloseDoorOnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        
        
        // Dooropen.SetBool(IsOpenId, false);
    }

    
    
    void OpenAndCloseDoor()
    {
        // Dooropen.SetBool(IsOpenId, true);
        
        
        
        StartCoroutine(CloseDoorOnTimer(TimeToCloseDoor));
    }
    
    

    private bool waitForSound;
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
