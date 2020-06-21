using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private CharacterStats characterStats;
    private GeneralStatistics generalStatistics;
    private LevelSystem levelSystem;

    public GeneralStatistics GeneralStatistics
    {
        get => generalStatistics;
        set => generalStatistics = value;
    }

    public CharacterStats CharacterStats
    {
        get => characterStats;
        set => characterStats = value;
    }

    public LevelSystem LevelSystem
    {
        get => levelSystem;
        set => levelSystem = value;
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
        Load();
    }

    public void Load()
    {
        CharacterStats = new CharacterStats();

        generalStatistics = SaveSystem.LoadGame();

        levelSystem = SaveSystem.LoadLevelSystem();
    }

    public void Save(int scoreLastGame, int moneyLastGame)
    {
        AddScore(scoreLastGame);
        AddMoney(moneyLastGame);

        SaveSystem.SaveGame();
        SaveSystem.SaveLevelSystem();
    }

    public void AddScore(int points)
    {
        generalStatistics.score += points;
        levelSystem.AddExperience(points);
    }

    public void AddMoney(int money)
    {
        levelSystem.SaveMoney(money);
    }

}