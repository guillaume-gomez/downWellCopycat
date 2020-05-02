using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;                        //Time to wait before starting level, in seconds.
    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    private ComboText comboText;

    private LevelGenerator levelScript;                        //Store a reference to our LevelGenerator which will set up the level.
    private GameData gamedata;
    private bool pauseGame = false;
    private int currentCombo;


    public GameData Gamedata
    {
        get => gamedata;
    }

    public bool PauseGame
    {
        get => pauseGame;

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
        InitGame();
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
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        Debug.Log("OnEnable");
     }
 
     void OnDisable()
     {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        Debug.Log("OnDisable");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }
 
     void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
     {
        Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        gamedata.level++;
        //InitGame();
     }


    void InitGame()
    {
        gameObject.SetActive(true);
        currentCombo = 0;
        Load();
        levelScript.SetupScene(gamedata.level);
    }

    //Update is called every frame.
    void Update()
    {
    }

    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
        Debug.Log("GameOver");
        //Disable this GameManager.
        //gameObject.SetActive(false);
        Save();
    }

    public void WinLevel()
    {
        Debug.Log("WinLevel");
        //gameObject.SetActive(false);
        Save();
    }

    public void Save()
    {
        SaveSystem.SaveGame();
    }

    public void Load()
    {
        GameData data = SaveSystem.LoadGame();
        gamedata = data;
    }

    public void AddScore(int point)
    {
        gamedata.score += point;
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