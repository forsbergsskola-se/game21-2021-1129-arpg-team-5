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

    private static readonly int IsOpen = Animator.StringToHash("isOpen");




    [SerializeField] private float DistanceToOpenDoor;
    [SerializeField] private float TimeToOpenDoor;
    
    
    
    
    
    void Awake()
    {
        Dooropen = gameObject.GetComponent<Animator>();
        
        
        // Get the mousecontoller and subscribe to their event.
        mouseController = FindObjectOfType<MouseController>();
        mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        
        StartCoroutine(UnlockDoor());



        temp2 = GoThroughDoor();
        
        

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
            if (!isOpen)
            {
                isOpen = true;
                Dooropen.SetBool(IsOpen, true);
                Debug.Log("does this work");
            }
            else
            {
                isOpen = false;
                Dooropen.SetBool(IsOpen, false);
                Debug.Log("does this work");
            }
        }
        else
        {
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(mouseClickVector);
            StartCoroutine(temp2);
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

    private IEnumerator temp2;

    private static IEnumerator GoThroughDoor()
    {
        // Gör saker.
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("Hi times up");
        }
    }


    void ChangedTarget(object sender, bool temp)
    {
        Debug.Log("Changed target!");
        // isLocked = false;
        StopCoroutine(temp2);
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
