using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Logic;


public class Destructible : MonoBehaviour, IInteractable
{
    public GameObject destroyedVersion;

    public Texture2D mouseTexture => throw new System.NotImplementedException();

    public void OnClick(Vector3 mouseClickVector)
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        GameObject.set

    }

    public void OnHover()
    {
        throw new System.NotImplementedException();
    }
}
