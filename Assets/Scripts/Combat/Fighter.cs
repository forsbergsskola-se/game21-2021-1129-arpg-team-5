using Team5.Movement;
using Team5.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team5.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        float weaponRange = 2f;
        [SerializeField]
        float timeBetweenAttacks = 1f;
        [SerializeField]
        float weaponDamage = 1f;
        float timeSinceLastAttack = Mathf.Infinity;

        private float critAttackMultiplier = 1.2f;
        private int critChance;
        public float critPercent = 30f; // 20% chance 
        private int accuracyChance;
        public float accuracyPercent = 90f; // 90% chance
        
        private GameObject player;
        private GameObject enemyIndicator;
        Health target;
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack1 = Animator.StringToHash("stopAttack");

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
            {
                return;
            }
            
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
            
            if (target.IsDead())
            {
                // Disables enemy indicator if enemy dies
                if (this.gameObject != player )
                {
                    EnemyIndicatorInactive();
                }

                // Disables enemy indicator if player dies
                else if (this.gameObject == player )
                {
                    EnemyIndicatorInactiveTarget();
                }
                return;
            }
            
            
            // Activates Enemy Indicator if Player targets enemy
            if (target.gameObject != player)
            {
                EnemyIndicatorActiveTarget();
            }

            // Activates Enemy Indicator if Enemy targets players
            if (target.gameObject == player)
            {
                EnemyIndicatorActive();
            }
            
            // need to add logic for small enemy indicator to go away
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                if (!target.IsDead())
                {
                    TriggerAttack();
                    timeSinceLastAttack = 0;
                }
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger(StopAttack1);
            GetComponent<Animator>().SetTrigger(Attack1);//triggering Hit() from animation
        }
 
        // Animation Event 
        void Hit()
        {
            if (target == null) return;   //Bug fixed!
            Debug.Log($"{this.name} new attack");
            
            // random values for critical hit and accuracy between 0 and 9
            critChance = 11 - Random.Range(1, 11);
            accuracyChance = 11 - (Random.Range(1, 11));

            // attack with critical hit if lower than critPercent value
            if (critChance >= critPercent/10)
            {
                var totalAttackValue = weaponDamage * critAttackMultiplier;
                Debug.Log($"{this.name} can land critical hit");
                // hit accuracy higher than chance, can attack with critical hit
                if (accuracyChance > accuracyPercent/10)
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy > {accuracyChance}0% chance");
                    Debug.Log($"{this.name} landed a CRITICAL HIT of {totalAttackValue} on {target.name}!!!");
                    target.TakeDamage(totalAttackValue);
                }

                //  misses attack due to low accuracy
                else
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy < {accuracyChance}0% chance");
                    Debug.Log($"{this.name}'s critical hit missed {target.name}!");
                }
            }

            // attack without critical hit
            else if (critChance <= critPercent/10)
            {
                if (accuracyPercent > accuracyChance)
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy > {accuracyChance}0% chance");
                    Debug.Log($"{this.name} dealt {weaponDamage} damage to {target.name}");
                    target.TakeDamage(weaponDamage);
                }
                
                //  misses attack due to low accuracy
                else

                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}0% accuracy < {accuracyChance}0% chance");
                    Debug.Log($"{this.name}'s attack missed {target.name}!");
                }
            }

            // Print death
            if (target.IsDead())
            {
                Debug.Log($"{target.name}'s current health: {target.healthPoint}");
                Debug.Log($"{target.name} was defeated by {this.name} at {Time.time}");
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead() ;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger(Attack1);
            GetComponent<Animator>().SetTrigger(StopAttack1);
        }

        //Set enemy indicator active
        public void EnemyIndicatorActive()
        {
            enemyIndicator = this.transform.Find("Enemy Indicator2").gameObject;
            enemyIndicator.SetActive(true); 
        }
        public void EnemyIndicatorActiveTarget()
        {
            enemyIndicator = target.transform.Find("Enemy Indicator").gameObject;
            enemyIndicator.SetActive(true); 
        }
        
        //Set enemy indicator inactive
        public void EnemyIndicatorInactive()
        {
            if (this.gameObject != player)
            {
                enemyIndicator = this.transform.Find("Enemy Indicator2").gameObject;
                enemyIndicator.SetActive(false);
            } 
        }
        public void EnemyIndicatorInactiveTarget()
        {
            {
                enemyIndicator = target.transform.Find("Enemy Indicator").gameObject;
                enemyIndicator.SetActive(false); 
            }
        }
    }
}
