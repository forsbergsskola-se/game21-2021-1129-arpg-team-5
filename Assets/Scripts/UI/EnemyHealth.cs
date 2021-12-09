using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Core;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    public float damageHealthDecay = 0.5f;
    private Health healthStats;
    private float maxHealth;
    private float health;
    private float oldHealth;
    private float damage;
    private float hurtHealth;
    private bool trigger;
    
    [SerializeField]
    public TMP_Text healthTextIndicator;
    [SerializeField]
    public TMP_Text hurtHealthTextIndicator;
    [SerializeField] public GameObject blood;



    private void Start()
    {
        healthStats = this.GetComponent<Health>();
        maxHealth = healthStats.maxHealth;
        damage = healthStats.damageAmount;
        oldHealth = maxHealth;
        hurtHealthTextIndicator.text = "";
    }
    void Update()
    {
        // Updates enemy health indicator
        health = healthStats.healthPoint;
        healthTextIndicator.text = "" + health;

        // Tiggers enemy take damage value
        if (oldHealth != health)
        {
            damage = healthStats.damageAmount;
            hurtHealth = damage;;
            hurtHealthTextIndicator.text = "-" + hurtHealth;
            hurtHealthTextIndicator.enabled = true;
            trigger = true;
            oldHealth = health;
        }
        
        // Calls disable take damage
        if (trigger == true)
        {
            blood.SetActive(true);
            Debug.Log($"Wait {damageHealthDecay} second(s)");
            StartCoroutine(WaitAndDisable());
            trigger = false;
        }
    }
    
    // Wait time to disable take damage
    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(damageHealthDecay);
        hurtHealthTextIndicator.enabled = false;
        blood.SetActive(false);
    }
}
