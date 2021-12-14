using Team5.Core;
using Team5.Movement;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTeleporter : MonoBehaviour, IInteractable
{
    public Texture2D mouseTexture => cursorTexture;
    public Texture2D cursorTexture;
    public string SceneToLoad;
    public void OnHover()
    {
        //Debug.Log("I hover over the Floor");
    }
    
    public void OnClick(Vector3 mouseClickVector)
    {
        Debug.Log("I clicked on the Floor");
        GameObject.Find("Player").GetComponent<Move>().StartMoveAction(mouseClickVector);
    }

    private void OnTriggerStay(Collider collidedObject)
    {
        if (collidedObject.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the teleporter!");
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}