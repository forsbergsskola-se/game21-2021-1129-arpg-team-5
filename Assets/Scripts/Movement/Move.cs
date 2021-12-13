using System;
using Team5.Core;
using Team5.Entities;
using UnityEngine;
using UnityEngine.AI;


namespace Team5.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        private GameObject targetDest;
        private GameObject player;

        private AudioSource audio;
        Animator animator;
        NavMeshAgent agent;
        private Entity entity;
        private Material enemyMaterial;
        private Material waypointMaterial;
        private Quaternion oldPlayerRotation;
        private static Quaternion newPlayerRotation;
        private float oldPlayerZAxis;
        private static float newPlayerZAxis;
        
        
        
        private void Start()
        {
            entity = GetComponent<Entity>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            
            player = GameObject.FindWithTag("Player");
            audio = player.GetComponent<AudioSource>();
            enemyMaterial = (Material) Resources.Load("EnemyIndicator");
            waypointMaterial = (Material) Resources.Load("Waypoint");


            targetDest = GameObject.Find("Navigation Sphere");
            audio = player.GetComponent<AudioSource>();
        }

        
        
        void Update()
        {
            agent.enabled = !entity.IsDead;

            UpdateAnimator();
            
            // indicates player has reached destinaton with sound and visual
            if(Vector3.Distance(player.transform.position, targetDest.transform.position) < 0.2)
            {
                if (agent.tag == "Player")
                    agent.angularSpeed = 10;
                
                    oldPlayerRotation = newPlayerRotation;

                    //Hide click-marker when destination is met.
                    targetDest.GetComponent<MeshRenderer>().enabled = false; 

                    audio.Play();
                    Debug.Log("target reach");
            }

            // changes destination colour if enemy
            else if (this.gameObject != player)
            {
                var targetDestLocation = Math.Round(targetDest.transform.position.x, 1);
                var enemyLocation = Math.Round(this.gameObject.transform.position.x, 1);

                if (enemyLocation == targetDestLocation)
                {
                    targetDest.GetComponent<MeshRenderer>().material = enemyMaterial;
                    // Debug.Log("Target: Enemy");
                }
                else
                {
                    targetDest.GetComponent<MeshRenderer>().material = waypointMaterial;
                }
            }
            else
            {
                if (agent.tag == "Player")
                {
                    agent.angularSpeed = 5000;
                    //enable click marker when player is at a higher distance from it.
                    targetDest.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }

        
        
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        
        
        public void MoveTo(Vector3 destination)
        {
            // can't move if dead
            if (this.entity.IsDead)
            {
                Debug.Log("Can't move yet bro, I'm dead");
            }
            // can't move if reviving and standing up
            else if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Revive"))
            {
                agent.isStopped = true;
                Debug.Log("Can't move yet bro, I'm reviving");
            }
            //otherwise can move
            else
            {
                agent.destination = destination;  // Move agent to the target position
                agent.isStopped = false;
                
                if (agent.tag == "Player")
                {
                    
                    oldPlayerRotation = this.gameObject.transform.rotation;
                    
                    targetDest.transform.position = destination;
                    newPlayerRotation = oldPlayerRotation;
                }
            }
        }
        
        

        public void Cancel()
        {
            //targetDest.transform.position = new Vector3(0, -50, 0);
            
            if (this.entity.IsDead)
            {
                Debug.Log("Still dead");
            }
            else
            {
                agent.isStopped = true;
            }
        }

        
        
        public void UpdateAnimator()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        
        
        public bool TargetReachable(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(destination, path);
            return path.status != NavMeshPathStatus.PathPartial;
        }
    }
}