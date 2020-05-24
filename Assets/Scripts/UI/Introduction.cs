using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public LevelGenerator levelScript;
    public PlayerController playerScript;
    public GameObject canvas;
    private GameObject bordersObj;
    private float borderSizeY = 50.0f;

    
    void Awake()
    {
        levelScript.CreateBorders();
        //Invoke("MovePlayer", 0.75f);
    }

    void Start()
    {
        bordersObj = GameObject.Find("Borders");

    }

    void Update()
    {
        Vector3 position = bordersObj.transform.position;
        bordersObj.transform.position =  new Vector3(position.x, position.y + 0.25f, position.z);
        if (position.y > borderSizeY)
        {
            bordersObj.transform.position = new Vector3(position.x, 10.0f, position.z);
        }

        if (playerScript.transform.position.y < -10.0f - borderSizeY)
        {
            //StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    void MovePlayer()
    {
        playerScript.gravityModifier = 1.0f;
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        Debug.Log("routine");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        return null;
    }

    public void PickABonus(int index)
    {
        MovePlayer();
        canvas.SetActive(false);
    }
}
