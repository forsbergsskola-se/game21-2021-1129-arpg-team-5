using System.Collections;
using System.Collections.Generic;
using Team5.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace Team5.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        Animator animator;
        NavMeshAgent agent;
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
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