using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        public float healthPoint;
        bool isDead = false;
        public float maxHealth;

        private void Awake()
        {
            this.healthPoint = maxHealth;
        }

        public bool IsDead() 
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if(healthPoint <= 0)
            {
                Death();
            }
            
            // Print current health if alive
            else
            {
                Debug.Log($"{this.name}'s current health: {this.healthPoint}");
            }
        }

        public void Death()
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();

            
            // check if Player was killed 
            if (this.name == "Player")
            {
                Revive();
            }
        }
        
        // Revive Player
        public void Revive()
        {
            Debug.Log($"{this.name} will resurrect in 3 seconds...");
            StartCoroutine(WaitToRevive());
        }
        
        
        // Revival wait time
        private IEnumerator WaitToRevive()
        {
            yield return new WaitForSeconds(3);
            this.healthPoint = maxHealth;
            GetComponent<Animator>().SetTrigger("revive");
            Debug.Log($"{this.name} resurrected at timestamp: {Time.time} with {maxHealth} health!");
        }
    }
}
