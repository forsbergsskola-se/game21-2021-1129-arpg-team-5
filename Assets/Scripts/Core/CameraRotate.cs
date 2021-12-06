using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;
   
    private Vector3 offset;
   
    void Start ()
    {
        offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);
    }
   
    void LateUpdate()
    {
        transform.position = player.position + offset;
        transform.LookAt (player.position);
 
 
        if (Input.GetMouseButton (1))
        {
            offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offset;
        }
    }
}
