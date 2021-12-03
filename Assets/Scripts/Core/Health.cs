using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team5.Movement;
using UnityEngine.AI;

namespace Team5.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        public float healthPoint;
        bool isDead = false;
        public float maxHealth;
        public GameObject Player;
        NavMeshAgent Agent;
        public bool revive = false;
        public int reviveWaitTime = 8;
        public int reviveCounter = 0;
        
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
            if (revive == true)
            {
                // prints revive count
                Agent.enabled = true;
                Debug.Log($"Number of revives: {reviveCounter}");
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

            // check if Player was killed so they can be revived
            if (this.tag == "Player") // can add special enemies here also
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
            this.healthPoint = maxHealth;
            revive = true;
            isDead = false;
            GetComponent<Animator>().SetTrigger("revive");
            GetComponent<Animator>().SetBool("isDead", false);
            
            // revival debug + add to counter
            Debug.Log($"{this.name} successfully resurrected at timestamp: {Time.time} with {maxHealth} health!");
            reviveCounter++;
        }
    }
}
