using System.Collections;
using System.Collections.Generic;
using Team5.Movement;
using Team5.Core;
using UnityEngine;

namespace Team5.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        float weaponRange = 2f;
        [SerializeField]
        float timeBetweenAttacks = 1f;
        [SerializeField]
        float weaponDamage = 1f;
        float timeSinceLastAttack;
        Transform target;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
            {
                return;
            }
                
           

            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack"); //triggering Hit() from animation
                timeSinceLastAttack = 0;
              
            }
            
           
        }
        // Animation Event 
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

       
    }
}
