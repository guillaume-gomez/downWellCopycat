using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public CamerFollow cameraScript;
    private ComboText comboText;

    private LevelGenerator levelScript;
    private bool pauseGame = false;
    private bool endGamePanel = false;
    private int currentCombo;
    private int score;
    private int level;

    public bool PauseGame
    {
        get => pauseGame;

    }

    public bool EndGamePanel
    {
        get => endGamePanel;
    }

    public int CurrentCombo
    {
        get => currentCombo;
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
        level = 0; // todo fix that GameManager.instance.GameData.level;
        InitGame();
     }


    void InitGame()
    {
        gameObject.SetActive(true);
        currentCombo = 0;
        endGamePanel = false;
        levelScript.SetupScene(level);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        Save();
    }

    public void WinLevel()
    {
        cameraScript.Unfollow();
        Debug.Log("WinLevel");
        endGamePanel = true;
        level = level + 1;
        Save();
        Invoke("LoadIntroScene", 2.0f);
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void Save()
    {
        GameManager.instance.Save(score, level);
    }

    public void AddScore(int point)
    {
        score += point;
    }

    public void IncCombo()
    {
        currentCombo = currentCombo + 1;
        comboText.UpdateCombo(currentCombo);
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        comboText.UpdateCombo(currentCombo);
    }

}