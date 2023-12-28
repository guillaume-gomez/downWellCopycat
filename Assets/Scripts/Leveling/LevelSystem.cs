using System;
using System.Collections.Generic;
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
    [Space]
    [Header("Run Data")]
    public float money;
    public int currentCombo;
    public int maxCombo;
    public int nbKilled;
    public int score;
    public float saveMoneyPercentage;
    public List<Bonus> bonuses;
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

    public int Level
    {
        get => level;
        set {
            level = value;
            UpdateEnemyValues(level);
        }
    }

    public int Level {
        get => level;
        set {
            level = value;
            UpdateEnemySkills(level);
        }
    }

    public LevelSystem()
    {
        bonuses = new List<Bonus>();
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
    }

    private void UpdateEnemyValues(int level)
    {
        minEnemySpeed = (int) Mathf.Log(level, 2f) + 1;
        maxEnemySpeed = (int) Mathf.Log(level, 2f) + 5;

        minEnemyLife = (int) Mathf.Log(level, 10f);
        maxEnemyLife = (int) Mathf.Log(level, 10f) + 1;
    }

    private void UpdateEnemySkills(int _level)
    {
        minEnemyLife = (int) Mathf.Log(_level,10);
        maxEnemyLife = (int) Mathf.Log(_level,10) + 1;

        minEnemySpeed = (int) Mathf.Log(_level,2) + 2;
        maxEnemySpeed = (int) Mathf.Log(_level,2) + 5;
        Debug.Log(minEnemySpeed);
        Debug.Log(maxEnemySpeed);
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

    public void AddBonus(Bonus bonus)
    {
        bonuses.Add(bonus);
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