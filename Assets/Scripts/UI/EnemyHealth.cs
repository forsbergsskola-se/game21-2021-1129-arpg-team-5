using System.Collections;
using System.Collections.Generic;
using Team5.Core;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    private float health;
    private TMP_Text healthText;
    

    void Update()
    {
        health = this.GetComponent<Health>().healthPoint;
        healthText = this.GetComponentInChildren<TMP_Text>();
        healthText.text = "" + health;
    }
}
