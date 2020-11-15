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

   void Update()
   {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
            GoBackMainMenu();
        }
   }

    void GoBack()
   {
      GoBackMainMenu();
   }

    void GoBackMainMenu()
   {
      SceneManager.LoadScene(0);
   }

    public void Buy(float itemPrice)
    {
        levelSystem.money -= itemPrice;
        OnMoneyChangedEventArgs eventArgs = new OnMoneyChangedEventArgs();
        eventArgs.money = levelSystem.money;
        OnMoneyChange(this, eventArgs);
    }


    void Save()
    {
        SaveSystem.SaveLevelSystem(levelSystem);
        SaveSystem.SaveCharacterStat();
    }


}