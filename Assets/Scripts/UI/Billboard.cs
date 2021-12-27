using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    void LateUpdate()
    {
        //rotate = cam.transform.rotation.z + 90;
        transform.LookAt(transform.position + cam.forward);
    }
}
