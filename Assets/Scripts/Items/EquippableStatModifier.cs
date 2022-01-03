using System;
using Team5.Entities;
using UnityEngine;

namespace Team5.Inventories.Items
{
    public class EquippableStatModifier : MonoBehaviour, IEquippable
    {
        public void Equip()
        {
            Debug.Log("Equipped " + name);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().Armor += 20;
        }

        public void UnEquip()
        {
            Debug.Log("UnEquipped " + name);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().Armor -= 20;
        }
    }
}