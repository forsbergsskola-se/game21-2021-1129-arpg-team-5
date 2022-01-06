using System;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryController : MonoBehaviour
{
    public GameObject accessory;

    public GameObject Accessory
    {
        set
        { 
            if (accessory != null)
            {
                accessory.GetComponent<IAccessory>().oldSlot = null;
                accessory.GetComponent<IAccessory>().UnEquip();

                // TODO: make this old accessory held by the mouse instead
                // Destroy(accessory);
            }

            accessory = value;
            
            if (value != null)
            {
                accessory.transform.SetParent(transform);
                accessory.transform.position = transform.position;

                if (accessory.GetComponent<IAccessory>().oldSlot != null)
                {
                    accessory.GetComponent<IAccessory>().oldSlot.Accessory = null;
                }
                accessory.GetComponent<IAccessory>().oldSlot = this;

                accessory.GetComponent<IAccessory>().Equip(); 
            }
        }
    }
}
