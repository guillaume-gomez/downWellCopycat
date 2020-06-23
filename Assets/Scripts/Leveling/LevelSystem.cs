using System;
using UnityEngine;

[Serializable]
public class LevelSystem
{

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public int level;
    public int experience;
    public int experienceToNextLevel;
    public int money;
    public int currentCombo;
    public int maxCombo;
    public int nbKilled;
    public int score;

    public LevelSystem()
    {
        money = 0;
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
        currentCombo = 0;
        maxCombo = 0;
        nbKilled = 0;
        score = 0;
    }

    public void InitGame()
    {
        money = 0;
        level = 1;
        currentCombo = 0;
        maxCombo = 0;
        nbKilled = 0;
        score = 0;
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

}