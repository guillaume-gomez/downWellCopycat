using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private CharacterStats characterStats;
    // private GeneralStatistics generalStatistics;
    private LevelSystem levelSystem;
    private LevelSystem levelSystemRun;


    public CharacterStats CharacterStats
    {
        get => characterStats;
        set => characterStats = value;
    }

    public LevelSystem LevelSystemRun
    {
        get => levelSystemRun;
        set => levelSystemRun = value;
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
        characterStats = new CharacterStats();

        //generalStatistics = SaveSystem.LoadGame();

        levelSystem = SaveSystem.LoadLevelSystem();

        levelSystemRun = new LevelSystem();
    }

    public void EndRun()
    {
        Save();
    }

    public void StartRun()
    {
        LevelSystemRun = new LevelSystem();
        Debug.Log("StartRun");
    }

    public void Save()
    {
        levelSystem.MergeLevelSystem(levelSystemRun);
        SaveSystem.SaveLevelSystem(levelSystem);
        SaveSystem.SaveCharacterStat();
    }

}