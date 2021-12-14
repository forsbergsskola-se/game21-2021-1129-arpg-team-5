using System;
using System.Collections.Generic;
using UnityEngine;
using Team5.Ui.Hotbar;

public class InventoryTemp : MonoBehaviour
{
    private GameObject item;
    private ConsumableController consumableController;

    private KeyCode[] keyInput =
    {
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
        KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };

    private void Start()
    {
        consumableController = FindObjectOfType<ConsumableController>();
    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            // if (Input.GetKeyDown()) 
        }
    }
}