using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExpSystem : MonoBehaviour
{
    public int maxExp; 
    public float updatedExp;

    public Image ExpBar;

    //test
    public float expIncreasedPerSecond;
    public int playerLevel;
    public Text levelText;
    
    void Start()
    {
        playerLevel = 1;
        expIncreasedPerSecond = 5f;
        maxExp = 25;
        updatedExp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updatedExp += expIncreasedPerSecond * Time.deltaTime;
        ExpBar.fillAmount = updatedExp / maxExp;

        levelText.text = playerLevel + "";

        if (updatedExp >= maxExp)
        {
            playerLevel++;
            updatedExp = 0;
            maxExp += maxExp;

        }
    }
}
