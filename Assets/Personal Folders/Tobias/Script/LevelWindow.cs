using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    private Text _levelText;
    private Image _experienceBarImage;
    private LevelSystem _levelSystem;

    private void Awake()
    {
        _levelText = transform.Find("levelText").GetComponent<Text>();
        _experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();
        
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        _experienceBarImage.fillAmount = experienceNormalized;
    }



    private void SetLevelNumber(int levelNumber)
    {
        _levelText.text = "LEVEL\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this._levelSystem = levelSystem;
        
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
        
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }
    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        SetLevelNumber(_levelSystem.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        SetExperienceBarSize(_levelSystem.GetExperienceNormalized());
    }
}
