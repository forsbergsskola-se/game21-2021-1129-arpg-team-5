using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Core;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EnemyHealth : MonoBehaviour
{
    private Health healthStats;
    private float maxHealth;
    private float health;
    private float damage;
    private float hurtHealth;
    [SerializeField]
    public TMP_Text healthTextIndicator;
    [SerializeField]
    public TMP_Text hurtHealthTextIndicator;


    private void Start()
    {
        healthStats = this.GetComponent<Health>();
        maxHealth = healthStats.maxHealth;
    }


    void Update()
    {
        health = healthStats.healthPoint;
        healthTextIndicator.text = "" + health;
        hurtHealthTextIndicator.text = "";

        if (health < maxHealth)
        {
            damage = healthStats.damageAmount;
            hurtHealth = damage;
            hurtHealthTextIndicator.text = "-" + hurtHealth;
        }
    }
}
