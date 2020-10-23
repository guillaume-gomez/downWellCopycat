using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MarketManager : MonoBehaviour
{
    public static MarketManager instance = null;
    private CharacterStats characterStats;
    private LevelSystem levelSystem;
    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChange;

    public float Money
    {
        get => levelSystem.money;
    }

    public float Score
    {
        get => levelSystem.score;
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

    void Load()
    {
        characterStats = new CharacterStats();
        levelSystem = SaveSystem.LoadLevelSystem();
    }

    public void TakeMoney(float _money)
    {
        levelSystem.money += _money;
        OnMoneyChangedEventArgs eventArgs = new OnMoneyChangedEventArgs();
        eventArgs.money = levelSystem.money;
        OnMoneyChange(this, eventArgs);
    }


    public void Save()
    {
        // SaveSystem.SaveLevelSystem();
        // SaveSystem.SaveCharacterStat();
    }


}