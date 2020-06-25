using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralStatistics
{
    public int score;
    public int maxCombo;
    public int money;
    public int experience;

    private float saveMoneyPercentage = 0.10f;


    public GeneralStatistics(int _score, int _maxCombo)
    {
        score = _score;
        maxCombo = _maxCombo;
    }

    public void SaveMoney(int _money)
    {
        money = (int)(_money * saveMoneyPercentage);
    }

    public void SaveExperience(int _experience)
    {
        experience = _experience;
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