using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class DoorControll : MonoBehaviour , IInteractable
{
    private Animator Dooropen;

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
        throw new System.NotImplementedException();
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        Debug.Log("does this work");
        GameObject.Find("GateTurnPosition").GetComponent<Animator>().Play("DoorAnimation",0, 0.0f);
        // Dooropen.Play("DoorAnimation", 0, 0.0f);
    }
}
