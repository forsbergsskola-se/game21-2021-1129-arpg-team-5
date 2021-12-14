using System;
using System.Collections.Generic;
using UnityEngine;
using Team5.Ui.Hotbar;
using Unity.VisualScripting;
using UnityEditor;

public class InventoryTemp : MonoBehaviour
{
    private GameObject item;
    private ConsumableController consumableController;
    public GameObject itemPrefab;

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
                
                if (consumableController.hotbarSlots[i] != null)
                {
                    if (item.IsDestroyed())
                    {
                        Instantiate(itemPrefab);
                        item = GameObject.Find("Item(Clone)");
                    }
                    
                    item.transform.position = consumableController.hotbarSlots[i].transform.position;
                    item.transform.SetParent(consumableController.hotbarSlots[i].transform);
                }
            }
        }
    }
}