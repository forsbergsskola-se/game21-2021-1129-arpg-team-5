using System;
using System.Collections.Generic;
using UnityEngine;
using Team5.Ui.Hotbar;

public class InventoryTemp : MonoBehaviour
{
    private GameObject item;
    private ConsumableController consumableController;
    private List<KeyCode> keyInput = new List<KeyCode>(10);

    private void Start()
    {
        consumableController = FindObjectOfType<ConsumableController>();
    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            //if (Input.GetKeyDown()) 
        }
    }
}