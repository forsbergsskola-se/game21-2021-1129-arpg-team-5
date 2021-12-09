using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using Team5.Combat;
using Team5.EntityBase;


namespace Team5.Entities.Player
{
    public class PlayerController : Entity
    {
        [SerializeField] private float healthOnRevive;
        [SerializeField] private float healthRegenPerSecond;
        [SerializeField] private float timeToRevive;

        private NavMeshAgent agent;
        private Animator animator;
        
        
        protected override void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            base.Awake();
            agent.speed = MovementSpeed;
        }
        
        
        
        void Update()
        {
            if (IsDead) 
                return;

            if (InteractWithCombat()) 
                return;
        
            // if (InteractWithMovement()) 
            //     return;
        }


        protected override void OnDeath()
        {
            Debug.Log("Player died!");
            base.OnDeath();
            Revive();
        }


        private void Revive()
        {
            Debug.Log("Player reviving");
            agent.enabled = true;
            StartCoroutine(WaitToRevive());
        }

        private IEnumerator WaitToRevive()
        {
            yield return new WaitForSeconds(timeToRevive);

            Debug.Log("Player wakes up");
            
            agent.enabled = true;
            agent.ResetPath();

            Health = healthOnRevive;
            IsDead = false;
            
            animator.SetTrigger("revive");
            animator.SetBool("isDead", false);
            
            StartCoroutine(PlayerRegenHealth());
        }

        private IEnumerator PlayerRegenHealth()
        {
            // TODO: Fix health regen not stopping after taking damage.
            while (Health < maxHealth && !takeDamageOnCooldown)
            {
                yield return new WaitForSeconds(1);
                Health += healthRegenPerSecond;
            }

            Debug.Log("Player finished regen.");
        }
        
        
        

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if (target == null) 
                    continue;
            
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                    continue;
            
                if (Input.GetMouseButtonDown(0)) 
                    GetComponent<Fighter>().Attack(target.gameObject);

                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}