using System;
using FMODUnity;
using Team5.Core;
using Team5.Entities;
using Team5.Entities.Player;
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
        public StudioEventEmitter MoveSound;
        
        private void Start()
        {
            entity = GetComponent<Entity>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            
            player = GameObject.FindWithTag("Player");
            enemyMaterial = (Material) Resources.Load("EnemyIndicator");
            waypointMaterial = (Material) Resources.Load("Waypoint");
            
            targetDest = GameObject.Find("Target Animation00");
        }



        void Update()
        {
            agent.enabled = !entity.IsDead;

            UpdateAnimator();
            
            // indicates player has reached destinaton with sound and visual
            if (CompareTag("Player"))
            {
                if (agent.isStopped || DistanceToMarker() < 0.2f)
                {
                    agent.angularSpeed = 10;
                    oldPlayerRotation = newPlayerRotation;
                    targetDest.GetComponent<SpriteRenderer>().enabled = false;
                    if (!canPlaySound)
                        return;
                    canPlaySound = false;
                }
                else if (!agent.isStopped && DistanceToMarker() > 0.2f)
                {
                    canPlaySound = true;
                    agent.angularSpeed = 5000;
                    targetDest.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            
            
            
            else if (this.gameObject != player)
            {
                var targetDestLocation = Math.Round(targetDest.transform.position.x, 1);
                var enemyLocation = Math.Round(this.gameObject.transform.position.x, 1);
                
                if (enemyLocation == targetDestLocation)
                    targetDest.GetComponent<SpriteRenderer>().material = enemyMaterial;
                else
                    targetDest.GetComponent<SpriteRenderer>().material = waypointMaterial;
            }
        }
        void WalkingSound()
        {
            MoveSound.Play();
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
            if (CompareTag("Player"))
                targetDest.transform.position = new Vector3(targetDest.transform.position.x, targetDest.transform.position.y + 0.01f, targetDest.transform.position.z);

        }

        
        
        public void MoveTo(Vector3 destination)
        {
            // can't set target dest if dead
            if (player.GetComponent<PlayerController>().reviving == true)
            {
                // TODO: This should probably be done from the player itself. And this magic number here is bad. If we change how much health we regen or start at, the player might be able to move to early, or to late.
                if (this.entity.Health < 40)
                {
                    targetDest.SetActive(false);
                    agent.isStopped = true;
                }
            }
            
            if (this.entity.IsDead)
            {
                agent.isStopped = true;
                
                if (agent.tag == "Player")
                {
                    targetDest.SetActive(false);
                }
                //Debug.Log(name + " is dead and can't move. <color=cyan>[ Why is move called when this entity is dead? ]</color>");
            }
            // can't move if reviving and standing up
            else if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Revive"))
            {
                agent.isStopped = true;
                //Debug.Log("Can't move yet bro, I'm reviving");
            }
            
            //otherwise can move
            else
            {
                agent.destination = destination; // Move agent to the target position
                agent.isStopped = false;
                if (agent.tag == "Player")
                {
                    targetDest.SetActive(true);
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
                //Debug.Log("Still dead");
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