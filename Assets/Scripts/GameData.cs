using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int score;

    public GameData(int _level, int _score)
    {
        level = _level;
        score = _score;
    }
}