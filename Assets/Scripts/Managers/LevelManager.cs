using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class OnComboChangedEventArgs : EventArgs
{
    public int combo { get; set; }
}

public class OnMoneyChangedEventArgs : EventArgs
{
    public float money { get; set; }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public event EventHandler OnWin;
    public event EventHandler OnLose;
    public event EventHandler<OnComboChangedEventArgs> OnUpdateCombo;
    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChange;

    public AudioClip winSound;
    public AudioClip loseSound;

    private ComboText comboText;
    private LevelGenerator levelScript;
    public static bool PauseGame = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        levelScript = GetComponent<LevelGenerator>();
    }

    void Start()
    {
        GameObject comboTextObj = GameObject.Find("ComboText");
        if(comboTextObj)
        {
            comboText = comboTextObj.GetComponent<ComboText>();
        }
    }

     void OnEnable()
     {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        //Debug.Log("OnEnable");
     }

     void OnDisable()
     {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //Debug.Log("OnDisable");
     }

     void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
     {
        Debug.Log("Level Loaded");
        InitGame();
     }


    void InitGame()
    {
        gameObject.SetActive(true);
        levelScript.SetupScene(GameManager.instance.LevelSystemRun.level);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        Time.timeScale = 0.0f;
        PauseGame = true;
        if(OnLose != null)
        {
            OnLose(this, EventArgs.Empty);
        }
        SoundManager.instance.PlayAndMuteMusic(loseSound);
        StartCoroutine(GoBackMenu());
    }

    public void WinLevel()
    {
        Debug.Log("WinLevel");
        if(OnWin != null)
        {
            OnWin(this, EventArgs.Empty);
        }
        GameManager.instance.LevelSystemRun.level += 1;
        GameManager.instance.Save();

        SoundManager.instance.PlayAndMuteMusic(winSound);
        Invoke("LoadIntroScene", 2.0f);
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    IEnumerator GoBackMenu()
    {
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1.0f;
        PauseGame = false;
        GameManager.instance.EndRun();
        SceneManager.LoadScene(0);
    }

    public void AddScore(int point)
    {
        GameManager.instance.LevelSystemRun.score += point;
    }

    public void IncEnemyKill()
    {
        GameManager.instance.LevelSystemRun.nbKilled += 1;
    }

    public void UpdateLife(int newLifeValue)
    {
        GameManager.instance.LevelSystemRun.currentLife = newLifeValue;
    }

    public void UpdateMoney(float _money)
    {
        GameManager.instance.LevelSystemRun.money += _money;

        OnMoneyChangedEventArgs eventArgs = new OnMoneyChangedEventArgs();
        eventArgs.money = GameManager.instance.LevelSystemRun.money;
        OnMoneyChange(this, eventArgs);
    }

    public void IncCombo()
    {
        GameManager.instance.LevelSystemRun.currentCombo += 1;

        if(OnUpdateCombo != null)
        {
            OnComboChangedEventArgs eventArgs = new OnComboChangedEventArgs();
            eventArgs.combo = GameManager.instance.LevelSystemRun.currentCombo;
            OnUpdateCombo(this, eventArgs);
        }
    }

    public void ResetCombo()
    {
        GameManager.instance.LevelSystemRun.currentCombo = 0;

        if(OnUpdateCombo != null)
        {
            OnComboChangedEventArgs eventArgs = new OnComboChangedEventArgs();
            eventArgs.combo = GameManager.instance.LevelSystemRun.currentCombo;
            OnUpdateCombo(this, eventArgs);
        }
    }

}