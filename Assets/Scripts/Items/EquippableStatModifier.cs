using System;
using Team5.Combat;
using Team5.Entities;
using UnityEngine;

namespace Team5.Inventories.Items
{
    public class EquippableStatModifier : MonoBehaviour, IEquippable
    {
        [SerializeField] private float MaxHealthBoost;
        [SerializeField] private float MovementSpeedBoost;
        [SerializeField] private float ArmorBoost;

        [SerializeField] private float AccuracyChanceBoost;
        [SerializeField] private float CriticalChanceBoost;
        [SerializeField] private float DamageBoost;
        [Tooltip("Does not always work. Animation sets a maximum attack speed, but with this you can make it a bit faster, but not much. Slowing it down is fine though.")]
        [SerializeField] private float AttackSpeedBoostSeconds;

        private Entity playerEntity;
        private Fighter playerFighter;

        private float attackSpeedBoost;

        private void Start()
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
            playerFighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();

            attackSpeedBoost = AttackSpeedBoostSeconds * -1;
        }

        public void Equip()
        {
            UpdateValues();
            
            Debug.Log("Equipped " + name);
            playerEntity.ModifyStats(MovementSpeedBoost,MaxHealthBoost,ArmorBoost,1);
            playerFighter.ModifyStats(AccuracyChanceBoost, CriticalChanceBoost, DamageBoost,
                attackSpeedBoost, 1);
        }

        public void UnEquip()
        {
            UpdateValues();
            
            Debug.Log("UnEquipped " + name);
            playerEntity.ModifyStats(MovementSpeedBoost,MaxHealthBoost,ArmorBoost,-1);
            playerFighter.ModifyStats(AccuracyChanceBoost, CriticalChanceBoost, DamageBoost,
                attackSpeedBoost, -1);
        }
    }
}