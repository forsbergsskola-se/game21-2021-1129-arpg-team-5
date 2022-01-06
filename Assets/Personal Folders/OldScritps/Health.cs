using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Team5.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        public float healthPoint;
        bool isDead = false;
        public float maxHealth;
        public float damageAmount; // for enemy Health Script
        private float currentHealth;
        
        public float revivalHealth; 
        public float reviveHealthRegenPerSecond;
        public int reviveWaitTime = 8;
        public int reviveCounter = 0;

        NavMeshAgent Agent;
        private bool revive = false;
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Revive1 = Animator.StringToHash("revive");
        private static readonly int Dead = Animator.StringToHash("isDead");


        private void Awake()
        {
            this.healthPoint = maxHealth;
        }
        private void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            currentHealth = this.healthPoint;

            if (revive)
            {
                // prints revive count
                Agent.enabled = true;
                Debug.Log($"Number of revives: {reviveCounter}");

                if (this.gameObject.tag == "Player")
                {
                    // increments player health until it reaches max health
                    if (!currentHealth.Equals(maxHealth))
                    {
                        StartCoroutine(AddHealth());
                    }
                }
                revive = false;
            }
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
            // Gets value for Enemy Health script
            damageAmount = damage;
        }

        public void Death()
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger(Die);
            GetComponent<ActionScheduler>().CancelCurrentAction();

            // check if Player was killed so they can be revived
            if (this.CompareTag("Player")) // can add special enemies here also
            {
                Revive();
            }
        }
        
        // Start revival process
        public void Revive()
        {
            Agent.enabled = true;
            Debug.Log($"{this.name} will resurrect in 8 seconds...");
            StartCoroutine(WaitToRevive());
        }

        // Revival wait time
        private IEnumerator WaitToRevive()
        {
            // keeps navmesh agent active
            
            yield return new WaitForSeconds(reviveWaitTime);
            Debug.Log("Player resurrecting");
            Agent.enabled = true; 
            Agent.ResetPath();
            Debug.Log($"{this.name} agent enabled: {Agent.isActiveAndEnabled}");

            // resets health and starts revive animation
            this.healthPoint = this.revivalHealth;
            Debug.Log($"Revival Health: {this.healthPoint}");
            revive = true;
            isDead = false;
            GetComponent<Animator>().SetTrigger(Revive1);
            GetComponent<Animator>().SetBool(Dead, false);
            
            // revival debug + add to counter
            Debug.Log($"{this.name} successfully resurrected at timestamp: {Time.time} with {this.healthPoint} health!");
            reviveCounter++;
        }
        
        // Revive health method
        IEnumerator AddHealth()
        {
            float time1 = Time.time; // start time
            while (this.healthPoint < maxHealth){ // while health < 101.
                healthPoint += 1; // increase health and wait the specified time
                yield return new WaitForSeconds(reviveHealthRegenPerSecond);
            }
            float time2 = Time.time; // end time
            // how long process took in total:
            Debug.Log($"{this.name}: Health restored in {Math.Round(time2 - time1)} seconds");
        }
    }
    
}
