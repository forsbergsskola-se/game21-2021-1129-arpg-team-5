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

    public List<GameObject> accessorySlots = new();
    
    private KeyCode[] keyInput1 =
    {
        KeyCode.Q, KeyCode.W, KeyCode.E,
    };
    
    private KeyCode[] keyInput2 =
    {
        KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U
    };

    private void Start()
    {
        consumableController = FindObjectOfType<ConsumableController>();
        item = Instantiate(itemPrefab);
        item.transform.SetParent(this.transform);
        item.transform.position = this.transform.position;
        
        
        
        // Get all the hotbar slots and put them inside a list.
        Transform slotSearch = gameObject.transform;
        for (int i = 1; slotSearch != null; i++)
        {
            slotSearch = transform.Find($"AccessorySlot{i}");
            Debug.Log(slotSearch);
            if (slotSearch != null)
                accessorySlots.Add(slotSearch.gameObject);
        }
        
        foreach (var VARIABLE in accessorySlots)
        {
            Debug.Log("Found: " + VARIABLE.name);
        }
    }

    private void Update()
    {
        for (int i = 0; i < keyInput1.Length; i++)
        {
            if (Input.GetKeyDown(keyInput1[i]))
            {
                Debug.Log($"Clicked hehe {keyInput1[i]}");
                
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
        
        for (int i = 0; i < keyInput2.Length; i++)
        {
            if (Input.GetKeyDown(keyInput2[i]))
            {
                Debug.Log($"Clicked hehe {keyInput2[i]}");
                
                if (accessorySlots[i] != null)
                {
                    if (item.IsDestroyed())
                    {
                        item = Instantiate(itemPrefab);
                    }

                    accessorySlots[i].GetComponent<AccessoryController>().Accessory = item;

                    // item.transform.position = accessorySlots[i].transform.position;
                    // item.transform.SetParent(accessorySlots[i].transform);
                }
            }
        }
    }
}