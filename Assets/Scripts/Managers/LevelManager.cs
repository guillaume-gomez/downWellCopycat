using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OnMoneyChangedEventArgs : EventArgs
{
    public int money { get; set; }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public event EventHandler OnWin;
    public event EventHandler OnLose;
    public event EventHandler OnUpdateCombo;
    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChange;

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
        DontDestroyOnLoad(gameObject);

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
        levelScript.SetupScene(GameManager.instance.LevelSystem.level);
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
        StartCoroutine(GoBackMenu());
    }

    public void WinLevel()
    {
        Debug.Log("WinLevel");
        if(OnWin != null)
        {
            OnWin(this, EventArgs.Empty);
        }
        GameManager.instance.LevelSystem.level += 1;
        GameManager.instance.Save();
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
        GameManager.instance.LevelSystem.score += point;
    }

    public void IncEnemyKill()
    {
        GameManager.instance.LevelSystem.nbKilled += 1;
    }

    public void TakeMoney(int _money)
    {
        GameManager.instance.LevelSystem.money += _money;

        OnMoneyChangedEventArgs eventArgs = new OnMoneyChangedEventArgs();
        eventArgs.money = GameManager.instance.LevelSystem.money;
        OnMoneyChange(this, eventArgs);
    }

    public void IncCombo()
    {
        GameManager.instance.LevelSystem.currentCombo += 1;
        if(GameManager.instance.LevelSystem.currentCombo > GameManager.instance.LevelSystem.maxCombo) {
            GameManager.instance.LevelSystem.maxCombo = GameManager.instance.LevelSystem.currentCombo;
        }
        OnUpdateCombo(this, EventArgs.Empty);
    }

    public void ResetCombo()
    {
        GameManager.instance.LevelSystem.currentCombo = 0;
        OnUpdateCombo(this, EventArgs.Empty);
    }

}