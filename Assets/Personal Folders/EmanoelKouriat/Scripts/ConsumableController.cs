using System;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Ui.Hotbar
{
    public class ConsumableController : MonoBehaviour
    {
        public List<GameObject> hotbarSlots = new();
        
        private KeyCode[] keyInput =
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
            KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0
        };

        private void Start()
        {
            
            // Get all the hotbar slots and put them inside a list.
            Transform slotSearch = gameObject.transform;
            for (int i = 1; slotSearch != null; i++)
            {
                slotSearch = transform.Find($"HotbarSlot{i}");
                if (slotSearch != null)
                    hotbarSlots.Add(slotSearch.gameObject);
            }

            foreach (var VARIABLE in hotbarSlots)
            {
                Debug.Log("Found: " + VARIABLE.name);
            }
        }

        private void Update()
        {
            for (int i = 0; i < keyInput.Length; i++)
            {
                if (Input.GetKeyDown(keyInput[i]))
                {
                    Debug.Log($"Clicked {keyInput[i]}");
                    
                    if (hotbarSlots[i].transform.childCount == 1)
                    {
                        hotbarSlots[i].transform.GetChild(0).gameObject.GetComponent<IConsumable>().Consume();
                    }
                }
            }
        }
    }
}