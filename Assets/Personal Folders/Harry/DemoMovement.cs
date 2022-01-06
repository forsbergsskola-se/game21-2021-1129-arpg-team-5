using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DemoMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent player;
    public GameObject targetDest;
    public float movementSpeed = 28f; // temp value, but works well

    //main method called

    private void Update()
    {
        MoveTo();
    }
    
    public void MoveTo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // mouse click point
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                // game object shows mouse click poimt
                targetDest.transform.position = hitPoint.point; //vector 3 pos
                
                // player moves to click point
                player.speed = movementSpeed; // change later to use entity speed value;
                player.SetDestination(hitPoint.point);

                return;
            }
        }
    }
}

