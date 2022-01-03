using System;
using Team5.Entities;
using UnityEngine;

namespace Team5.Inventories.Items
{
    public class EquippableStatModifier : MonoBehaviour, IEquippable
    {
        [SerializeField] private float MaxHealthBoost;
        [SerializeField] private float MovementSpeedBoost;
        [SerializeField] private float ArmorBoost;

        public void Equip()
        {
            Debug.Log("Equipped " + name);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().Armor += 20;
            
            // TODO: Make a nice way to modify the stat of choice.
        }

        public void UnEquip()
        {
            Debug.Log("UnEquipped " + name);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().Armor -= 20;
        }
    }
}