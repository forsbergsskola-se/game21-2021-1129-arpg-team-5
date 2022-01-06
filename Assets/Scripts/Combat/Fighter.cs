using FMODUnity;
using Team5.Movement;
using Team5.Core;
using Team5.Entities;
using Team5.Inventories.Items;
using Team5.Ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team5.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [HideInInspector] public int killCount;
        
        [SerializeField] private float baseAccuracyPercentage;
        [SerializeField] private float baseCriticalChance;
        [SerializeField] private float criticalDamageMultiplier;
        [SerializeField] private float timeBetweenAttacks = 1f;


        [SerializeField] Weapon defaultweapon = null;
        private Weapon currentWeapon = null;
        private float missedDamage = 0f;

        
        [SerializeField] Transform handTransform = null;

        

        private float accuracyPercentage;
        private float criticalChance;
        private float timeSinceLastAttack = Mathf.Infinity;
        public StudioEventEmitter attackSound;

        private GameObject player;
        private Entity thisEntity;
        private GameObject enemyIndicator;
        private Entity target;
        
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack1 = Animator.StringToHash("stopAttack");

        public float CriticalChance
        {
            get => criticalChance;
            set => criticalChance = Mathf.Min(value, 100);
        }
        
        public float AccuracyPercentage
        {
            get => accuracyPercentage;
            set => accuracyPercentage = Mathf.Min(value, 100);
        }
        
        
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            thisEntity = GetComponent<Entity>();

            AccuracyPercentage = baseAccuracyPercentage;
            CriticalChance = baseCriticalChance;

            EquipWeapon(defaultweapon);
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
                if(!target.IsDead)
                    GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }

            // if (target.IsDead)
            // {
            //     // Disables enemy indicator if enemy dies
            //     if (this.gameObject != player )
            //     {
            //         EnemyIndicatorInactive();
            //     }
            //
            //     // Disables enemy indicator if player dies
            //     else if (this.gameObject == player )
            //     {
            //         EnemyIndicatorInactiveTarget();
            //     }
            //     return;
            // }
            //
            //
            // // Activates Enemy Indicator if Player targets enemy
            // if (target.gameObject != player)
            // {
            //     EnemyIndicatorActiveTarget();
            // }
            //
            // // Activates Enemy Indicator if Enemy targets players
            // if (target.gameObject == player)
            // {
            //     EnemyIndicatorActive();
            // }
            
            // need to add logic for small enemy indicator to go away
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
            
        }

        private void AttackBehaviour()
        {
            if (!thisEntity.IsDead && !target.IsDead)
            {
                //Target Looking at Y position only.
                
                Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
                transform.LookAt(targetPosition);
                   
                   // keeps both entities exactly at mirror opposite locations (may cause problems with multiple entities)
                   //Quaternion targetRotation = target.transform.rotation;
                   //Quaternion rot180degrees = Quaternion.Euler(-targetRotation.eulerAngles);

                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    if (!target.IsDead)
                    {
                        TriggerAttack();
                        timeSinceLastAttack = 0;
                    }
                }
            }
            else
            {
                // keeps position and rotation instead of looking at corpse
                
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
                
                //Debug.Log(name + " is dead and can't attack. <color=cyan>[ Why is AttackBehaviour called when this entity is dead? ]</color>");
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
            if (target == null || target.IsDead) return;
            // Debug.Log($"{this.name} new attack");
            
            // random values for critical hit and accuracy between 0 and 9
            // critChance = 11 - Random.Range(1, 11);
            // accuracyChance = 11 - Random.Range(1, 11);

            // attack with critical hit if lower than critPercent value
            if (Random.Range(0, 100) < CriticalChance)
            {
                var totalAttackValue = currentWeapon.GetDamage() * criticalDamageMultiplier;
                
                if (target.CompareTag("Enemy"))
                {
                    Debug.Log($"{this.name} can land critical hit");
                }
                // hit accuracy higher than chance, can attack with critical hit
                if (Random.Range(0, 100) < accuracyPercentage)
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy > {accuracyChance}0% chance");
                    
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log($"{this.name} landed a CRITICAL HIT of {totalAttackValue} on {target.name}!!!");

                    }
                    Debug.Log("Play Audio");
                    attackSound.Play();
                    target.TakeDamage(totalAttackValue);
                }

                //  misses attack due to low accuracy
                else
                {
                    target.TakeDamage(missedDamage);
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy < {accuracyChance}0% chance");
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log($"{this.name}'s critical hit missed {target.name}!");
                    }
                }
            }
            // attack without critical hit
            else
            {
                if (Random.Range(0, 100) < accuracyPercentage)
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy > {accuracyChance}0% chance");
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log($"{this.name} dealt {currentWeapon.GetDamage()} damage to {target.name}");
                    }
                    attackSound.Play();
                    target.TakeDamage(currentWeapon.GetDamage());
                }
                
                //  misses attack due to low accuracy
                else
                {
                    target.TakeDamage(missedDamage);
                    //Debug.Log($"{this.name}'s {accuracyPercent}0% accuracy < {accuracyChance}0% chance");
                    if (target.CompareTag("Enemy"))
                    {
                        Debug.Log($"{this.name}'s attack missed {target.name}!");
                    }
                }
            }

            // Print death
            if (target.IsDead && target.CompareTag("Enemy"))
            {
                // Debug.Log($"{target.name}'s current health: {target.Health}");
                // Debug.Log($"{target.name} was defeated by {this.name} at {Time.time}");

                if (this.gameObject == player)
                {
                    killCount++;
                }
            }
        }

        
        
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        
        
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Entity targetToTest = combatTarget.GetComponent<Entity>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        
        
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Entity>();
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



        /// <summary>
        /// Modifies attack statistics.
        /// </summary>
        /// <param name="AccuracyChanceMOD"></param>
        /// <param name="DamageMOD"></param>
        /// <param name="AttacKSpeedMOD"></param>
        /// <param name="multiplier">Use to choose to add/subtract values. 1 = add, -1 = subtract.</param>
        public void ModifyStats(float AccuracyChanceMOD, float CritChanceMod, float CritDamageMod, float DamageMOD, float AttacKSpeedMOD, int multiplier)
        {
            if (AccuracyChanceMOD != 0) accuracyPercentage += AccuracyChanceMOD * multiplier;
            if (CritChanceMod != 0) criticalChance += CritChanceMod * multiplier;
            if (CritDamageMod != 0) criticalDamageMultiplier += CritDamageMod * multiplier;
            if (DamageMOD != 0) currentWeapon.SetDamage(DamageMOD * multiplier);
            if (AttacKSpeedMOD != 0) timeBetweenAttacks += AttacKSpeedMOD * multiplier;
        }

        

        public void EquipWeapon(WeaponItem weaponitem)
        {
            // weaponRange = weapon.WeaponRange;
            //weaponRange += weapon.WeaponRangeComparedToFists;
            currentWeapon.SetRange(weaponitem.WeaponRangeComparedToFists);

            // todo: Set the model of the player and weapon after the model attached to the weapon.
            // todo: Set the animation of the player to the weapons animations.

            Debug.Log("Equiped weapon");
        }

        public void UnEquipWeapon(WeaponItem weapon)
        {
            //weaponRange -= weapon.WeaponRangeComparedToFists;

            // todo: Revert back to the standard unarmed animation.
            // todo: Revert back to not render the weapon, and only empty hands.

            Debug.Log("Unequiped weapon");
        }


        //Set enemy indicator active
        // public void EnemyIndicatorActive()
        // {
        //     enemyIndicator = this.transform.Find("Enemy Indicator2").gameObject;
        //     enemyIndicator.SetActive(true); 
        // }
        //
        //
        //
        // public void EnemyIndicatorActiveTarget()
        // {
        //     if (target.CompareTag("Enemy"))
        //     {
        //         enemyIndicator = target.transform.Find("Enemy Indicator").gameObject;
        //         enemyIndicator.SetActive(true);    
        //     }
        // }
        
        
        
        //Set enemy indicator inactive
        // public void EnemyIndicatorInactive()
        // {
        //     if (this.gameObject != player && target.CompareTag("Enemy"))
        //     {
        //         enemyIndicator = this.transform.Find("Enemy Indicator2").gameObject;
        //         enemyIndicator.SetActive(false);
        //     } 
        // }
        
        
        
        // public void EnemyIndicatorInactiveTarget()
        // {
        //     if (target.CompareTag("Enemy"))
        //     {
        //         enemyIndicator = target.transform.Find("Enemy Indicator").gameObject;
        //         enemyIndicator.SetActive(false);
        //     }
        // }
    }
}
