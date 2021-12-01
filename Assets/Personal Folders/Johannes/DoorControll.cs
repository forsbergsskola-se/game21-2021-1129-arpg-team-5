using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Logic;
using Team5.Movement;
using UnityEngine;

public class DoorControll : MonoBehaviour , IInteractable
{
    [SerializeField] private Texture2D lockedCursor;
    [SerializeField] private Texture2D unlockedCursor;

    private Animator Dooropen;
    private GameObject player;


    private bool isOpen;


    private bool isLocked = true;

    private MouseController mouseController;

    private static readonly int IsOpenId = Animator.StringToHash("isOpen");




    [SerializeField] private float DistanceToOpenDoor;
    [SerializeField] private float TimeToOpenDoor;
    [SerializeField] private float TimeToCloseDoor;
    
    
    
    
    void Awake()
    {
        Dooropen = gameObject.GetComponent<Animator>();
        
        
        // Get the mousecontoller and subscribe to their event.
        mouseController = FindObjectOfType<MouseController>();
        mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        
        StartCoroutine(UnlockDoor());



        goThroughDoor = GoThroughDoor();
        
        

        player = GameObject.FindGameObjectWithTag("Player");
    }
    

    public Texture2D mouseTexture => isLocked ? lockedCursor : unlockedCursor;


    public void OnHover()
    {
        if (isLocked)
        {
            if (!waitForSound)
            {
                GetComponent<AudioSource>().Play();
                StartCoroutine(WaitForSound());
            }
        }
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        if (isLocked)
            return;

        Debug.Log("Door clicked!");
        // if (!isOpen)
        // {
        //     isOpen = true;
        //     Dooropen.SetBool(IsOpen, true);
        //     Debug.Log("does this work");
        // }
        // else
        // {
        //     isOpen = false;
        //     Dooropen.SetBool(IsOpen, false);
        //     Debug.Log("does this work");
        // }

        if (Vector3.Distance(player.transform.position, this.transform.position) < DistanceToOpenDoor)
        {
            // if (!isOpen)
            // {
            //     isOpen = true;
            //     Dooropen.SetBool(IsOpen, true);
            //     Debug.Log("does this work");
            // }
            // else
            // {
            //     isOpen = false;
            //     Dooropen.SetBool(IsOpen, false);
            //     Debug.Log("does this work");
            // }
            
            StartCoroutine(goThroughDoor);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(mouseClickVector);
            StartCoroutine(goThroughDoor);
        }
        
        // Check player distance to door.
        
        // If door is close
        // Open door
        
        // If door not close
        
        // Tell stupid player to get closer to door.
        
        // Then if player is close enough to door
        // Open door
        
        // When door is open
        // Tell player to move through the door
        
        // When the player has moved through the door
        // Close the door behind them.
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
                StopCoroutine(goThroughDoor);
            }
        }
        
        // OpenAndCloseDoor();

        for (int i = 0; i < 100; i++)
        {
            // Open the door and enter.
        }
    }

    void ChangedTarget(object sender, bool temp)
    {
        Debug.Log("Changed target!");
        // isLocked = false;
        StopCoroutine(goThroughDoor);
    }

    IEnumerator CloseDoorOnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Dooropen.SetBool(IsOpenId, false);
    }

    void OpenAndCloseDoor()
    {
        Dooropen.SetBool(IsOpenId, true);
        StartCoroutine(CloseDoorOnTimer(TimeToCloseDoor));
    }

    private bool waitForSound;
    IEnumerator WaitForSound()
    {
        waitForSound = true;
        yield return new WaitForSeconds(1);
        waitForSound = false;
    }
    
    
    
    // TEMPORARY TIMER FOR UNLOCK!
    IEnumerator UnlockDoor()
    {
        yield return new WaitForSeconds(1);
        isLocked = false;
    }
}
