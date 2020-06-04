using System;
using UnityEngine;

[Serializable]
public class LevelSystem
{

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if(experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            if(OnLevelChanged != null)
            {
                OnExperienceChanged(this, EventArgs.Empty);
            }
        }
        if(OnExperienceChanged != null)
        {
            OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

}