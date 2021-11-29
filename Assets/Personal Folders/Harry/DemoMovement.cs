using System;
using UnityEngine;
using UnityEngine.AI;

public class DemoMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent player;
    public GameObject targetDest;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                targetDest.transform.position = hitPoint.point;
                player.SetDestination(hitPoint.point);
            }
        }
    }
}

