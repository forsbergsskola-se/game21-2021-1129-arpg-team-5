using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Team5/Combat/New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController overrideAnim;
        [SerializeField] GameObject weaponPrefab = null;

        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 1f;

        public void Spawn(Transform handposition, Animator animator)
        {
            Instantiate(weaponPrefab, handposition);
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
