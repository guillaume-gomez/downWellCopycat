using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int score;
    public int maxCombo;

    public GameData(int _level, int _score, int _maxCombo)
    {
        level = _level;
        score = _score;
        maxCombo = _maxCombo;
    }

    public string valueFromString(string key)
    {
        switch(key)
        {
            case "level":
                return level.ToString();
            case "score":
                return score.ToString();
            case "maxCombo":
                return maxCombo.ToString();
            default:
                return "";
        }
    } 
}