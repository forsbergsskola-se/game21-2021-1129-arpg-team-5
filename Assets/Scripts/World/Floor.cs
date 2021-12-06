using Logic;
using Team5.Movement;
using UnityEngine;

namespace World
{
    public class Floor : MonoBehaviour, IInteractable
    {
        public Texture2D mouseTexture => cursorTexture;
        public Texture2D cursorTexture;
        public void OnHover()
        {
            //Debug.Log("I hover over the Floor");
        }
        public void OnClick(Vector3 mouseClickVector)
        {
            // Debug.Log("I clicked on the Floor");
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(mouseClickVector);
        }
    }
}
