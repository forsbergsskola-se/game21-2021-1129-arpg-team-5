using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Logic;
using UnityEngine;

public class DoorControll : MonoBehaviour , IInteractable
{
    private Animator Dooropen;
    bool isOpen = false;
    private GameObject player;
    // public GameObject cube;
    private readonly bool dooropenBool = false;
    // Start is called before the first frame update
    void Awake()
    {
        Dooropen = gameObject.GetComponent<Animator>();
    }
    

    public Texture2D mouseTexture { get; }
    public void OnHover()
    {
        Debug.Log("works");
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        if (!isOpen)
        {
            isOpen = true;
            GetComponent<Animator>().SetBool("isOpen", true);
            Debug.Log("does this work");
        }
        else
        {
            isOpen = false;
            GetComponent<Animator>().SetBool("isOpen", false);
            Debug.Log("does this work");
        }
       
        // Yes!
       

    }
}
