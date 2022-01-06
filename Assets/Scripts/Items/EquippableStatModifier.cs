using System;
using Team5.Combat;
using Team5.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Team5.Inventories.Items
{
    public class EquippableStatModifier : MonoBehaviour, IEquippable
    {
        [SerializeField] private float MaxHealthBoost;
        [SerializeField] private float MovementSpeedBoost;
        [FormerlySerializedAs("ArmorBoost")] [SerializeField] private float DefenceBoost;

        [SerializeField] private float AccuracyChanceBoost;
        [SerializeField] private float CriticalChanceBoost;
        [SerializeField] private float CriticalDamageMultiplierBoost;
        [SerializeField] private float DamageBoost;
        [Tooltip("Does not always work. Animation sets a maximum attack speed, but with this you can make it a bit faster, but not much. Slowing it down is fine though.")]
        [SerializeField] private float AttackSpeedBoostSeconds;

        [SerializeField] private bool IsWeapon;

        private Entity playerEntity;
        private Fighter playerFighter;

        private float attackSpeedBoost;

        private void Start()
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (IsWeapon)
            {
                if (!TryGetComponent(out WeaponItem weapon))
                {
                    IsWeapon = false;
                    Debug.Log($"<color=cyan>No weapon.cs script found on {name}. Did you mark this item as weapon deliberately?</color>");
                }
            }
            
            
            
            playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
            playerFighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();

            attackSpeedBoost = AttackSpeedBoostSeconds * -1;
        }

        public void Equip()
        {
            UpdateValues();
            
            Debug.Log("Equipped " + name);
            playerEntity.ModifyStats(MovementSpeedBoost,MaxHealthBoost,DefenceBoost,1);
            playerFighter.ModifyStats(AccuracyChanceBoost, CriticalChanceBoost, CriticalDamageMultiplierBoost, DamageBoost,
                attackSpeedBoost, 1);
            
            if (IsWeapon)
                playerFighter.EquipWeapon(GetComponent<WeaponItem>());
        }

        public void UnEquip()
        {
            UpdateValues();
            
            Debug.Log("UnEquipped " + name);
            playerEntity.ModifyStats(MovementSpeedBoost,MaxHealthBoost,DefenceBoost,-1);
            playerFighter.ModifyStats(AccuracyChanceBoost, CriticalChanceBoost, CriticalDamageMultiplierBoost, DamageBoost,
                attackSpeedBoost, -1);
            
            if (IsWeapon)
                playerFighter.UnEquipWeapon(GetComponent<WeaponItem>());
        }
    }
}