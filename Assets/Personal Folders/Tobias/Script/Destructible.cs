using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Logic;


public class Destructible : MonoBehaviour, IInteractable
{
    public GameObject destroyedVersion;

    public Texture2D mouseTexture
    {
        get => cursorTexture;
        set => cursorTexture = value;
    }

    public Texture2D cursorTexture;

    private MeshRenderer gameObjectMesh;
    public Texture2D newCursor;
    private BoxCollider GameObjectCollider;
    private bool IsDestroyed=true;
    
    
    public void OnClick(Vector3 mouseClickVector)
    {
        if (IsDestroyed== true)
        {
            gameObjectMesh = GetComponent<MeshRenderer>();
            gameObjectMesh.enabled = !gameObjectMesh.enabled;
            GameObjectCollider = GetComponent<BoxCollider>();
            GameObjectCollider.enabled = !GameObjectCollider.enabled;
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            IsDestroyed = false;
        }
        
    }

    public void OnHover()
    {
        if (IsDestroyed== false)
        {
            mouseTexture = newCursor;
        }
    }
}
