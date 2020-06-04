using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralStatistics
{
    public int score;
    public int maxCombo;

    public GeneralStatistics(int _score, int _maxCombo)
    {
        score = _score;
        maxCombo = _maxCombo;
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