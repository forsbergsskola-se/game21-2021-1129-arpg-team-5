using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    public Texture2D mouseTexture => cursorTexture;
    public Texture2D cursorTexture;
    public void OnHover(MouseController mouseController)
    {
        Debug.Log("I hover over the Box");
    }
    public void OnClick(MouseController mouseController)
    {
        Debug.Log("I clicked on the Box");
    }
}
