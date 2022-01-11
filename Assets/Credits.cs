using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject CreditsMenu;
    public GameObject Grumpkin;
    public GameObject Dog;

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (Grumpkin.activeInHierarchy == true)
            {
                Grumpkin.SetActive(false);
            }

            else
            {
                Grumpkin.SetActive(true);
            }
            
        }
        
        if (Input.GetKeyDown("g"))
        {
            if (Dog.activeInHierarchy == true)
            {
                Dog.SetActive(false);
            }

            else
            {
                Dog.SetActive(true);
            }
            
        }
    }
    
    public void Hide()
    {
        Grumpkin.SetActive(false);
        Dog.SetActive(false);
    }
}
