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

            
            // check if Player was killed 
            if (this.tag == "Player")
            {
                Revive();
            }
        }
        
        // Revive Player
        public void Revive()
        {
            
            Agent.enabled = true;
            if (Agent.enabled) Debug.Log("Agent enabled");
            Debug.Log($"{this.name} will resurrect in 3 seconds...");
            StartCoroutine(WaitToRevive());
            //GetComponent<ActionScheduler>().StartAction(this);
        }
        
        
        // Revival wait time
        private IEnumerator WaitToRevive()
        {
            yield return new WaitForSeconds(3);
            this.healthPoint = maxHealth;
            
            
            Debug.Log($"{this.name} resurrected at timestamp: {Time.time} with {maxHealth} health!");
                
            
            // attempts to reset navmesh agent
            
            yield return new WaitForSeconds(5);
            Debug.Log("Player is active");
            Agent.enabled = true; 
            Agent.ResetPath();
            Debug.Log(Agent.isActiveAndEnabled);
            Debug.Log(!Agent.isActiveAndEnabled);
            revive = true;
            isDead = false;
            GetComponent<Animator>().SetTrigger("revive");
            GetComponent<Animator>().SetBool("isDead", false);
            reviveCounter++;
        }
    }
}
