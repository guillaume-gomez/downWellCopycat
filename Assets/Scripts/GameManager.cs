using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;                        //Time to wait before starting level, in seconds.
    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.

    private LevelGenerator levelScript;                        //Store a reference to our LevelGenerator which will set up the level.
    private int level = 1;                                    //Current level number, expressed in game as "Day 1".
    private bool doingSetup = true;                            //Boolean to check if we're setting up board, prevent Player from moving during setup.

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
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        level++;
        InitGame();
     }


    void InitGame()
    {
        doingSetup = true;
        Invoke("HideLevelImage", levelStartDelay);

        levelScript.SetupScene(level);

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
        //Disable this GameManager.
        enabled = false;
    }
}