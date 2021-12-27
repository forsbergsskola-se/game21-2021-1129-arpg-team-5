using System;
using System.Collections;
using Team5.Control;
using Team5.Core;
using Team5.Ui;
using Team5.Ui.ExpSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team5.Entities.Enemies
{
    public class Enemy : Entity, IInteractable
    {
        // TODO: Update this with designer choice. Maybe a healthbar instead, or something else.
        private TMP_Text healthText;
        private TMP_Text hurtText;
        [SerializeField] private Texture2D MouseTexture;
        private ParticleSystem blood;
        public float damageHealthDecay = 0.5f;
        
        public float dustSpawnTime;
        public float corpseStayTime;
        private ParticleSystem deathCloud;
        public SkinnedMeshRenderer mesh;
        private MeshRenderer enemyIndicator2;
        private MeshRenderer enemyIndicator1;
       
        private bool textEnabled = false;
        public Canvas canvas;
        public Image healthBar;
        private float currentHealthBar;

        public void Start()
        {
            // UI game start settings
            healthBar.fillAmount = 1;
            currentHealthBar = 1;
            canvas.enabled = true;
        }

        public void Update()
        {
            // sets Healthbar fill
            currentHealthBar = this.Health / this.MaxHealth;
            healthBar.fillAmount = currentHealthBar;

            // sets Healthbar colour to red if health is high
            if (currentHealthBar < 0.333)
            {
                healthBar.color = Color.red;
            }

            // sets Healthbar colour to green if health is high
            else if (currentHealthBar > 0.666)
            {
                healthBar.color = Color.green;
            }
            else
            {
                healthBar.color = Color.yellow;
            }
        }


        void ChangedTarget(object sender, bool temp)
        {
            if (textEnabled)
            {
                textEnabled = false;
                healthText.enabled = false;
            }
        }

        public override float Health
        {
            get => base.Health;
            protected set
            {
                float hurt = (base.Health - value);
                base.Health = value;
                hurtText.SetText(Mathf.RoundToInt(hurt).ToString());
                healthText.SetText(Mathf.RoundToInt(Health).ToString());
                
                // blood.gameObject.SetActive(true);
                // blood.Play();
                hurtText.enabled = true;
                StartCoroutine(WaitAndDisableHurtHealth());
            }
        }
        
        protected override void Awake()
        {
            healthText = GetComponentInChildren<TMP_Text>();
            hurtText = transform.Find("Hurt Health Value (TMP)").GetComponent<TMP_Text>();
            // blood = transform.Find("Blood").GetComponent<ParticleSystem>();
            // deathCloud = transform.Find("Dust Cloud").GetComponent<ParticleSystem>();
            // enemyIndicator1 = transform.Find("Enemy Indicator").GetComponent<MeshRenderer>();
            // enemyIndicator2 = transform.Find("Enemy Indicator2").GetComponent<MeshRenderer>();
            base.Awake();
            
            var mouseController = FindObjectOfType<MouseController>();
            mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.

            healthText.enabled = false;
        }

        protected override void OnDeath()
        {
            //gameObject.GetComponent<OutlineController>().DisableOutlineController();
            canvas.enabled = false;

            if (gameObject.TryGetComponent(out OutlineController outlineController))
                outlineController.DisableOutlineController();

            
            // if (enemyIndicator2.enabled == true)
            // {
            //     enemyIndicator2.enabled = false;
            //     enemyIndicator1.enabled = false;
            // }
            
            StartCoroutine(WaitAndDisableDeath());
            base.OnDeath();
        }
        
        IEnumerator WaitAndDisableHurtHealth()
        {
            yield return new WaitForSeconds(damageHealthDecay);
            hurtText.enabled = false;
            // blood.Stop();
            // blood.gameObject.SetActive(false);
        }
        
        private IEnumerator WaitAndDisableDeath()
        {
            Debug.Log($"Destroy {this.name} in {dustSpawnTime + corpseStayTime} seconds");
            
            // Dust cloud spawns
            yield return new WaitForSeconds(dustSpawnTime);
            deathCloud.gameObject.SetActive(true);
            deathCloud.Play();
            deathCloud.transform.position = this.gameObject.transform.position;
            
            // Enemy mesh is disabled
            yield return new WaitForSeconds(corpseStayTime);
            mesh.GetComponent<Renderer>().enabled = false;

            // Enemy game object fully disabled
            yield return new WaitForSeconds(10);
            this.gameObject.SetActive(false);
            deathCloud.gameObject.SetActive(false);
        }
        
        

        public Texture2D mouseTexture => MouseTexture;

        public void OnHoverEnter()
        {
            healthText.enabled = true;
        }

        public void OnHoverExit()
        {
            if (!textEnabled) 
                healthText.enabled = false;
        }

        public void OnClick(Vector3 mouseClickVector)
        {
            textEnabled = true;
            healthText.enabled = true;
        }
    }
}

