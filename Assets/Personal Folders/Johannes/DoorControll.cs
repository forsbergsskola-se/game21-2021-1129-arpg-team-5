using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Logic;
using UnityEngine;

public class DoorControll : MonoBehaviour , IInteractable
{
    private Animator Dooropen;

    private GameObject player;
    // public GameObject cube;
    private readonly bool dooropenBool = false;
    // Start is called before the first frame update
    void Awake()
    {
        Dooropen = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D mouseTexture { get; }
    public void OnHover()
    {
        Debug.Log("works");
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        Debug.Log("does this work");
        // GameObject.Find("GateTurnPosition").GetComponent<Animator>().SetTrigger("openDoor");
        // Dooropen.Play("DoorAnimation", 0, 0.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("works");
            GetComponent<Animator>().SetTrigger("openDoor");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
