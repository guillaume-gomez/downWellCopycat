using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralStatistics
{
    public int score;
    public int maxCombo;
    public float money;
    public int experience;
    public float saveMoneyPercentage;


    public GeneralStatistics()
    {
        score = 0;
        maxCombo = 0;
        money = 0;
        experience = 0;
        saveMoneyPercentage = 0.1f;
    }

    public void AddDataFromPreviousRun(LevelSystem ls)
    {
        money += ls.money * saveMoneyPercentage;
        experience += ls.experience;
    }

    public string valueFromString(string key)
    {
        switch(key)
        {
            case "score":
                return score.ToString();
            case "maxCombo":
                return maxCombo.ToString();
            default:
                return "";
        }
    }
}