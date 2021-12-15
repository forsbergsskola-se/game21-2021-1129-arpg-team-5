using System;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryController : MonoBehaviour
{
    // public List<GameObject> accessorySlots = new();
    //     
    // private KeyCode[] keyInput =
    // {
    //     KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7
    // };
    //
    // private void Start()
    // {
    //         
    //     // Get all the hotbar slots and put them inside a list.
    //     Transform slotSearch = gameObject.transform;
    //     for (int i = 1; slotSearch != null; i++)
    //     {
    //         slotSearch = transform.Find($"AccessorySlot{i}");
    //         if (slotSearch != null)
    //             accessorySlots.Add(slotSearch.gameObject);
    //     }
    //
    //     foreach (var VARIABLE in accessorySlots)
    //     {
    //         Debug.Log("Found: " + VARIABLE.name);
    //     }
    // }
    //
    // private void Update()
    // {
    //     for (int i = 0; i < keyInput.Length; i++)
    //     {
    //         if (Input.GetKeyDown(keyInput[i]))
    //         {
    //             Debug.Log($"Clicked {keyInput[i]}");
    //                 
    //             if (accessorySlots[i].transform.childCount == 1)
    //             {
    //                 // accessorySlots[i].transform.GetChild(0).gameObject.GetComponent<IAccessory>().Consume();
    //             }
    //         }
    //     }
    // }


    public GameObject accessory;

    public GameObject Accessory
    {
        set
        { 
            if (accessory != null)
            {
                accessory.GetComponent<IAccessory>().UnEquip();

                // TODO: make this old accessory held by the mouse instead
                // Destroy(accessory);
            }
            value.transform.SetParent(transform);
            value.transform.position = transform.position;
            accessory = value;

            if (accessory.GetComponent<IAccessory>().oldSlot != null)
            {
                accessory.GetComponent<IAccessory>().oldSlot.Accessory = null;
            }
            accessory.GetComponent<IAccessory>().oldSlot = this;

            value.GetComponent<IAccessory>().Equip(); 
        }
    }
}
