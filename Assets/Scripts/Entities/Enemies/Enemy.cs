using System;
using System.Collections;
using FMODUnity;
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
        private TMP_Text hurtText;
        [SerializeField] private Texture2D MouseTexture;
        private ParticleSystem blood;
        public float damageTextDecayTime = 0.5f;
        
        public float dustSpawnTime;
        public float corpseStayTime;
        private ParticleSystem deathCloud;

        private bool textEnabled = false;
        private Canvas canvas;
        public Image healthBar;
        private float healthbarValue;
        private const float oneThird = 0.333f;
        private const float twoThirds = 0.666f;
        
        
        public void Start()
        {
            // UI game start settings
            healthBar.fillAmount = 1; // FillAmount only supports values between 0 and 1.
            healthbarValue = 1;
        }

        
        
        public void UpdateUi()
        {
            healthbarValue = Health / MaxHealth;
            healthBar.fillAmount = healthbarValue;
            
            // sets Healthbar colour to red if health is high
            if (healthbarValue < oneThird)
            {
                healthBar.color = Color.red;
            }
        
            // sets Healthbar colour to green if health is high
            else if (healthbarValue > twoThirds)
            {
                healthBar.color = Color.green;
            }
            else
            {
                healthBar.color = Color.yellow;
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
                UpdateUi();

                // blood.gameObject.SetActive(true);
                // blood.Play();
                hurtText.enabled = true;
                StartCoroutine(WaitAndDisableHurtHealth());
            }
        }
        
        
        
        protected override void Awake()
        {
            Transform enemyUi = transform.Find("EntityUiElements").transform;
            hurtText = enemyUi.Find("Hurt Health Value (TMP)").GetComponent<TMP_Text>();
            blood = transform.Find("Blood").GetComponent<ParticleSystem>();
            deathCloud = transform.Find("Dust Cloud").GetComponent<ParticleSystem>();
            base.Awake();
            
            var mouseController = FindObjectOfType<MouseController>();
            mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
            
            canvas = GetComponentInChildren<Canvas>();
            canvas.enabled = false;
        }

        
        
        protected override void OnDeath()
        {
            if (gameObject.TryGetComponent(out OutlineController outlineController))
                outlineController.DisableOutlineController();

            StartCoroutine(WaitAndDisableDeath());
            base.OnDeath();
        }
        
        
        
        IEnumerator WaitAndDisableHurtHealth()
        {
            yield return new WaitForSeconds(damageTextDecayTime);
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
            GetComponentInChildren<Renderer>().enabled = false;

            // Enemy game object fully disabled
            yield return new WaitForSeconds(10);
            this.gameObject.SetActive(false);
            deathCloud.gameObject.SetActive(false);
        }
        
        

        public Texture2D mouseTexture => MouseTexture;

        
        
        public void OnHoverEnter()
        {
            canvas.enabled = true;
        }

        
        
        public void OnHoverExit()
        {
            if (!textEnabled)
                canvas.enabled = false;
        }

        
        
        public void OnClick(Vector3 mouseClickVector)
        {
            textEnabled = true;
            canvas.enabled = true;
        }
        
        
        
        void ChangedTarget(object sender, bool temp)
        {
            if (textEnabled)
            {
                textEnabled = false;
                canvas.enabled = false;
            }
        }
    }
}

