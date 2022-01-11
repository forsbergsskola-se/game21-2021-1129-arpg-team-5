using System;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using Team5.Combat;
using Team5.Ui;
using TMPro;
using UnityEngine.UI;

namespace Team5.Entities.Player
{
    public class PlayerController : Entity
    {
      
        //[SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] private float healthOnRevive;
        [SerializeField] private float healthRegenPerSecond;
        [SerializeField] private float timeToRevive;
        public float reviveHealthCapPercentage;

        private NavMeshAgent agent;
        private Animator animator;
        private bool stopRegen;
        public bool reviving {get; private set; } = false ;
        public int reviveCounter;
        
        // Health UI
        public Image healthBar;
        public GameObject healthEffects;
        public Image lowHealthEffect;
        public Image veryLowHealthEffect;
        private float currentHealthBar;
        //private bool lowHealth; - could be used for ui warnings/ damage multipliers
        
        //TODO To put Ui stuff here.
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI ArmorText;
        public TextMeshProUGUI AccuracyText;
        public TextMeshProUGUI CriticalChansText;
        public TextMeshProUGUI CriticalDamageText;
        public TextMeshProUGUI DamageText;
        public TextMeshProUGUI SpeedText;
        
        private Fighter fighter;

        public int killCount;
        private string killText;
        private TMP_Text killCounter;


        protected override void Awake()
        {
            killCounter = FindObjectOfType<HUD>().KillCountText;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            base.Awake();
            agent.speed = MovementSpeed;
            // UI game start settings
            healthBar.fillAmount = 1;
            currentHealthBar = 1;
            healthEffects.SetActive(true);
            lowHealthEffect.enabled = false;
            veryLowHealthEffect.enabled = false;
            fighter = GetComponent<Fighter>();
            StartCoroutine(WaitBeforeRevive());
        }


        void Update()
        {
            HealthText.text = MaxHealth.ToString();
            ArmorText.text = Armor.ToString();
            SpeedText.text = MovementSpeed.ToString();
            AccuracyText.text = fighter.AccuracyPercentage.ToString();
            CriticalChansText.text = fighter.CriticalChance.ToString();
            CriticalDamageText.text = (fighter.criticalDamageMultiplier * fighter.GetTotalDamage()).ToString();
            DamageText.text = (fighter.currentWeapon.GetDamage() + fighter.BonusDamage).ToString();
            killText = $"Kills: " + $"{killCount}";
            killCounter.text = killText;

            // Debug.Log(weaponDamage.ToString());
            // suggestion for how lowHealth bool can be utilized
            
            // TODO: Do NOT spam debug logs!
            // if (lowHealth == true)
            // {
            //     Debug.Log("Use a potion to regain health!");
            // }
            
            // sets Healthbar fill

            currentHealthBar = this.Health / this.MaxHealth;
            healthBar.fillAmount = currentHealthBar;

            // sets Healthbar colour to red if health is low
            if (currentHealthBar < 0.333)
            {
                //lowHealth = true;
                healthBar.color = Color.red;

                // triggers low Health overlay
                if (currentHealthBar < 0.333 && currentHealthBar > 0.111)
                {
                    veryLowHealthEffect.enabled = false;
                    lowHealthEffect.enabled = true;
                }
                // triggers very low Health overlay
                else
                {
                    lowHealthEffect.enabled = false;
                    veryLowHealthEffect.enabled = true;
                }
            }
            
            // sets Healthbar colour to green if health is high
            else if (currentHealthBar > 0.666)
            {
                //lowHealth = false;
                healthBar.color = Color.green;
                lowHealthEffect.enabled = false;
                veryLowHealthEffect.enabled = false;
            }
            
            // sets Healthbar colour to orange if health is in-between high & low
            else
            {
                //lowHealth = false;
                healthBar.color = Color.yellow;
                lowHealthEffect.enabled = false;
                veryLowHealthEffect.enabled = false;
            }
            
            if (IsDead) 
                return;

            if (InteractWithCombat()) 
                return;

            // if (InteractWithMovement()) 
            //     return;

            //if (InteractWithUI()) return;
        }
       


        protected override void OnDeath()
        {
            EntityLevel += 1;
            Debug.Log("Player died!");
            base.OnDeath();
            Debug.Log("Play death audio");
            Revive();
        }

        
        public override void TakeDamage(float damageTaken)
        {
            base.TakeDamage(damageTaken);
            stopRegen = true;
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
            IsDead = true;
            animator.SetTrigger("revive");
            animator.SetBool("isDead", false);
            StartCoroutine(ReviveDone());
            
            reviveCounter++;
            
            StartCoroutine(PlayerRegenHealth());
        }

        
        private IEnumerator PlayerRegenHealth()
        {
            reviving = true;
            stopRegen = false;

            // var regenPercent = (MaxHealth / 100) * reviveHealthCapPercentage;
            // while (Health < regenPercent && !stopRegen)
            while (Health<MaxHealth&&!stopRegen)
            {
                yield return new WaitForSeconds(1);
                Health += healthRegenPerSecond;
            }

            stopRegen = false;
            reviving = false;

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

        public IEnumerator ReviveDone()
        {
            Debug.Log("Is this running");
            yield return new WaitForSeconds(7);
            IsDead = false;
            
        }
        public IEnumerator WaitBeforeRevive()
        {
            IsDead = true;
            yield return new WaitForSeconds(8);
            IsDead = false;
        }
    }
}