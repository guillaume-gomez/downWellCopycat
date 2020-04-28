using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;                        //Time to wait before starting level, in seconds.
    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.

    private LevelGenerator levelScript;                        //Store a reference to our LevelGenerator which will set up the level.
    private GameData gamedata;
    private bool doingSetup = true;                            //Boolean to check if we're setting up board, prevent Player from moving during setup.
    private bool pauseGame = false;

    public GameData Gamedata
    {
        get => gamedata;
    }

    public bool PauseGame
    {
        get => pauseGame;

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

     void OnEnable()
     {
      //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
         SceneManager.sceneLoaded += OnLevelFinishedLoading;
     }
 
     void OnDisable()
     {
     //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
         SceneManager.sceneLoaded -= OnLevelFinishedLoading;
     }
 
     void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
     {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        gamedata.level++;
     }


    void InitGame()
    {
        doingSetup = true;
        Load();
        Invoke("HideLevelImage", levelStartDelay);
        levelScript.SetupScene(gamedata.level);
    }

    //Hides black image used between levels
    void HideLevelImage()
    {
        doingSetup = false;
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
        enabled = false;
        Save();
    }

    public void WinLevel()
    {
        Debug.Log("WinLevel");
        enabled = true;
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

}