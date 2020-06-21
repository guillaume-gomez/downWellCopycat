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
    private int money;

    private float saveMoneyPercentage = 0.10f;

    public LevelSystem()
    {
        money = 0;
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

    public void SaveMoney(int _money)
    {
        money = (int)(_money * saveMoneyPercentage);
    }

    public int GetLevelNumber()
    {
        return level;
    }

}