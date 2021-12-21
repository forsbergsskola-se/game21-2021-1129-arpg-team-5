using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int _level;
    private int _experience;
    private int _experienceToNextLevel;

    public LevelSystem()
    {
        _level = 0;
        _experience = 0;
        _experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        _experience += amount;
        if (_experience >= _experienceToNextLevel)
        {
            //Enough exp to lvl up
            _level++;
            _experience -= _experienceToNextLevel;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);

        }

        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);

    }

    public int GetLevelNumber()
    {
        return _level;
    }

    public float GetExperienceNormalized()
    {
        return (float) _experience / _experienceToNextLevel;

    }
}
