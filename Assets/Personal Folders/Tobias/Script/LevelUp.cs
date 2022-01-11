using System.Collections;
using UnityEngine;
using Team5.Control;
using Team5.Core;
using Team5.Ui;
using TMPro;
using Team5.Entities;


public class LevelUp : MonoBehaviour
{
    public int level;
    private float experience;
    private float experienceRequierd;
    private Entity target;

    public float hp;
    
    
    void Start()
    {
        level = 1;
        hp = 100; //for testing
        experience = 0;
        
    }

    void Update()
    {
        Exp();

        if (Input.GetKeyDown(KeyCode.E))
        {
            experience += 100;
        }
    }

    void RankUp()
    {
        level += 1;
        experience = 0;

        switch (level)
        {
            case 2:
                hp = 200;
                break;
            
            case 3:
                hp = 300;
                break;
                
            
        }
    }


    void Exp()
    {
        if (experience >= experienceRequierd)
            RankUp();
    }
    
}
