using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private CharacterStat characterStat;
    private GameData gamedata;

    public GameData GameData
    {
        get => gamedata;
        set => gamedata = value;
    }

    public CharacterStat CharacterStat
    {
        get => characterStat;
        set => characterStat = value;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Save(int scoreLastGame, int level)
    {
        AddScore(scoreLastGame);
        if(level > gamedata.level)
        {
            gamedata.level = level;
        }
        SaveSystem.SaveGame();
    }

    public void AddScore(int point)
    {
        gamedata.score += point;
    }


}