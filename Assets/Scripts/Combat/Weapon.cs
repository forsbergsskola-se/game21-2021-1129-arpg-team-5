using System.Collections;
using System.Collections.Generic;
using Team5.Inventories;
using UnityEngine;

namespace Team5.Combat
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Team5/Combat/New Weapon")]
    public class Weapon : EquipedItem
    {
        [SerializeField] AnimatorOverrideController overrideAnim;
        [SerializeField] GameObject equippedPrefab = null;

        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 1f;

        public void Spawn(Transform handposition, Animator animator)
        {
            if(equippedPrefab!=null)
                Instantiate(equippedPrefab, handposition);
            if(overrideAnim != null)
                animator.runtimeAnimatorController = overrideAnim;

        }

        public void SetDamage(float damage)
        {
            weaponDamage += damage;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public void SetRange(float range)
        {
            weaponRange += range;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}
