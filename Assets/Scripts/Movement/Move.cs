using System;
using Team5.Core;
using Team5.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Object = System.Object;


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
        private bool canPlaySound;
        
        //TEMPORARY FOR OUR UPSCALED TEST SCENE!
        public bool isOnTestScene;
        private float differentdistance;
        //TEMPORARY FOR OUR UPSCALED TEST SCENE!
        
        
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

            //TEMPORARY FOR OUR UPSCALED TEST SCENE!
            differentdistance = !isOnTestScene ? 0.2f : 2f;
            //TEMPORARY FOR OUR UPSCALED TEST SCENE!

            // indicates player has reached destinaton with sound and visual
            if (CompareTag("Player"))
            {
                if (agent.isStopped || DistanceToMarker() < differentdistance)
                {
                    agent.angularSpeed = 10;
                    oldPlayerRotation = newPlayerRotation;
                    targetDest.GetComponent<MeshRenderer>().enabled = false;

                    if (!canPlaySound)
                        return;
                    canPlaySound = false;
                    audio.Play();
                }
                else if (!agent.isStopped && DistanceToMarker() > differentdistance)
                {
                    canPlaySound = true;
                    agent.angularSpeed = 5000;
                    targetDest.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            
            
            
            else if (this.gameObject != player)
            {
                var targetDestLocation = Math.Round(targetDest.transform.position.x, 1);
                var enemyLocation = Math.Round(this.gameObject.transform.position.x, 1);
                
                if (enemyLocation == targetDestLocation)
                    targetDest.GetComponent<MeshRenderer>().material = enemyMaterial;
                else
                    targetDest.GetComponent<MeshRenderer>().material = waypointMaterial;
            }
        }
        
        
        
        private float DistanceToMarker()
        {

            var dist = Vector3.Distance(player.transform.position, targetDest.transform.position);
            return dist;
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
                Debug.Log(name + " is dead and can't move. <color=cyan>[ Why is move called when this entity is dead? ]</color>");
                targetDest.transform.position = new Vector3(0, -50, 0);;
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