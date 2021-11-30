using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour, IInteractable
{
    public void OnHover()
    {
        Debug.Log("I hover over the Circle");
    }
    public void OnClick()
    {
        Debug.Log("I clicked on the Circle");
    }
}
