using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject Grumpkin;

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (Grumpkin.activeInHierarchy)
            {
                Grumpkin.SetActive(false);
            }

            else
            {
                Grumpkin.SetActive(true);
            }
            
        }
    }
}
