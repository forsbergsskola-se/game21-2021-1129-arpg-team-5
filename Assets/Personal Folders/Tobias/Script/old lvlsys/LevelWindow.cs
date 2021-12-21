
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LevelWindow : MonoBehaviour {

    private Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;

    private void Awake() {
        levelText = transform.Find("levelText").GetComponent<Text>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();
        
        SetExperienceBarSize(.5f);
        SetLevelNumber(7);
    }

    private void SetExperienceBarSize(float experienceNormalized) {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber) {
        levelText.text = "LEVEL\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem) {
        this.levelSystem = levelSystem;
    }
}
