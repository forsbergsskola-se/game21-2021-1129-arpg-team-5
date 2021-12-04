using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Entities.Player;
using Team5.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace Team5.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        private GameObject targetDest;
        private GameObject player;
        private GameObject enemy;
        private GameObject enemy2;

        private AudioSource audio;
        private AudioClip destReached;
        private Material enemyMaterial;
        private Material waypointMaterial;

        Animator animator;
        NavMeshAgent agent;
        Health health;
        private Transform enemyPosition;
        
        private void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            
            player = GameObject.FindWithTag("Player");
            audio = player.GetComponent<AudioSource>();
            destReached = gameObject.GetComponent<AudioClip>();
            
            
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemy2 = GameObject.FindGameObjectWithTag("Enemy2");


            targetDest = GameObject.Find("Navigation Sphere");
            audio = player.GetComponent<AudioSource>();
            enemyMaterial = (Material) Resources.Load("EnemyIndicator");
            waypointMaterial = (Material) Resources.Load("Waypoint");
        }

        void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
            if (player.transform.position.x == targetDest.transform.position.x)
            {
                targetDest.transform.position = new Vector3(0, -50, 0);
                audio.PlayOneShot(destReached);
                Debug.Log("target reach");
            }
            else if (targetDest.transform.position.x == enemy.transform.position.x 
                     || targetDest.transform.position.x == enemy2.transform.position.x)
            {
                targetDest.GetComponent<MeshRenderer>().material = enemyMaterial;
            }
            else
            {
                targetDest.GetComponent<MeshRenderer>().material = waypointMaterial;
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
            if (this.health.IsDead())
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
                    targetDest.transform.position = destination;
                }
            }
        }

        public void Cancel()
        {
            agent.isStopped = true;
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