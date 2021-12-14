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
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y,
        KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P
    };

    private void Start()
    {
        consumableController = FindObjectOfType<ConsumableController>();
        item = GameObject.Find("Item");
    }

    private void Update()
    {
        for (int i = 0; i < keyInput.Length; i++)
        {
            if (Input.GetKeyDown(keyInput[i]))
            {
                Debug.Log($"Clicked hehe {keyInput[i]}");
                // TODO: Move the item to the slot corresponding to the key number.

                if (consumableController.hotbarSlots[i] != null)
                {
                    item.transform.position = consumableController.hotbarSlots[i].transform.position;
                    item.transform.SetParent(consumableController.hotbarSlots[i].transform);
                }
            }
        }
    }
}