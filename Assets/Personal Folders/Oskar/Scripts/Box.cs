using Logic;
using UnityEngine;

namespace Personal_Folders.Oskar.Scripts
{
    public class Box : MonoBehaviour, IInteractable
    {
        public Texture2D mouseTexture => cursorTexture;
        public Texture2D cursorTexture;
        public void OnHover()
        {
            Debug.Log("I hover over the Box");
        }
        public void OnClick(Vector3 mouseClickVector)
        {
            Debug.Log("I clicked on the Box");
        }
    }
}
