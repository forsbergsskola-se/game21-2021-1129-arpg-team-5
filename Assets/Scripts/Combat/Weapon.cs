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

        public void Spawn(Transform handposition, Animator animator)
        {
            Instantiate(weaponPrefab, handposition);
            animator.runtimeAnimatorController = overrideAnim;
        }
    }
}
