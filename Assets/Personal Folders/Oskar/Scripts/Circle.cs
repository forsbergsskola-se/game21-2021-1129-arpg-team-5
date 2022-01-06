using Team5.Core;
using UnityEngine;

namespace Personal_Folders.Oskar.Scripts
{
    public class Circle : MonoBehaviour, IInteractable
    {
        public Texture2D mouseTexture => cursorTexture;
        public Texture2D cursorTexture;
        public void OnHoverEnter()
        {
            Debug.Log("I hover over the Circle");
        }

        public void OnHoverExit()
        {
            
        }

        public void OnClick(Vector3 mouseClickVector)
        {
            Debug.Log("I clicked on the Circle");
        }
    }
}
