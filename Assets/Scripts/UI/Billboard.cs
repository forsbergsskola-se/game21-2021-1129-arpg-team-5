using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        //rotate = cam.transform.rotation.z + 90;
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
