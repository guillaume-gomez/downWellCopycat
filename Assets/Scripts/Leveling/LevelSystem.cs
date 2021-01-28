using System;
using UnityEngine;

[System.Serializable]
public class LevelSystem
{

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    public int level;
    public int levelPlayer;
    public int currentLife;
    public int experience;
    public int experienceToNextLevel;
    public float money;
    public int currentCombo;
    public int maxCombo;
    public int nbKilled;
    public int score;
    public float saveMoneyPercentage;
    [Space]
    [Header("Enemy")]
    public int minEnemyLife;
    public int maxEnemyLife;
    public int minEnemySpeed;
    public int maxEnemySpeed;
    [Space]
    [Header("Level")]
    public int nbRooms;
    public float percentageCenter;
    public float percentageSide;


    public LevelSystem()
    {
        money = 0;
        currentLife = 4;
        level = 1;
        levelPlayer = 1;
        experience = 0;
        experienceToNextLevel = 100;
        currentCombo = 0;
        maxCombo = 0;
        nbKilled = 0;
        score = 0;
        saveMoneyPercentage = 0.1f;

        minEnemyLife = 1;
        minEnemyLife = 2;
        minEnemySpeed = 2;
        maxEnemySpeed = 5;
        
        nbRooms = 20;
        percentageCenter = 0.40f;
        percentageSide = 0.90f;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if(experience >= experienceToNextLevel)
        {
            levelPlayer++;
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

    public void MergeLevelSystem(LevelSystem ls)
    {
        money += ls.money * ls.saveMoneyPercentage;
        experience += ls.experience;

        score += ls.score;
        if(maxCombo < ls.maxCombo)
        {
            maxCombo = ls.maxCombo;
        }
        nbKilled += ls.nbKilled;
    }

}