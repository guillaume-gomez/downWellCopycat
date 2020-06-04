using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public event EventHandler OnWin;
    public event EventHandler OnLose;
    public event EventHandler OnUpdateCombo;

    private ComboText comboText;
    private LevelGenerator levelScript;
    private bool pauseGame = false;
    private int currentCombo;
    private int score;
    private int level;

    public bool PauseGame
    {
        get => pauseGame;
    }

    public int CurrentCombo
    {
        get => currentCombo;
    }

    public int Level
    {
        get => level;
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

        levelScript = GetComponent<LevelGenerator>();
        // for debugging only InitGame();
    }

    void Start()
    {
        GameObject comboTextObj = GameObject.Find("ComboText");
        if(comboTextObj)
        {
            comboText = comboTextObj.GetComponent<ComboText>();
        }
        level = 0;
        score = 0;
    }

     void OnEnable()
     {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        Debug.Log("OnEnable");
     }
 
     void OnDisable()
     {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        Debug.Log("OnDisable");
     }
 
     void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
     {
        Debug.Log("Level Loaded");
        InitGame();
     }


    void InitGame()
    {
        gameObject.SetActive(true);
        currentCombo = 0;
        levelScript.SetupScene(level);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        OnLose(this, EventArgs.Empty);
        Save();
        Invoke("GoBackMenu", 5.0f);

    }

    public void WinLevel()
    {
        Debug.Log("WinLevel");
        OnWin(this, EventArgs.Empty);
        level = level + 1;
        Save();
        Invoke("LoadIntroScene", 2.0f);
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void GoBackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Save()
    {
        GameManager.instance.Save(score);
    }

    public void AddScore(int point)
    {
        score += point;
    }

    public void IncCombo()
    {
        currentCombo = currentCombo + 1;
        OnUpdateCombo(this, EventArgs.Empty);
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        OnUpdateCombo(this, EventArgs.Empty);
    }

}