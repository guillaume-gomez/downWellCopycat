using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public LevelGenerator levelScript;
    public PlayerController playerScript;
    public CamerFollow cameraScript;
    public GameObject canvas;
    public GameObject[] bonusItems;

    private GameObject bordersObj;
    private float borderSizeY = 50.0f;
    private int nbBonusAvailable = 4;


    void Start()
    {
        levelScript.CreateBorders();
        bordersObj = GameObject.Find("Borders");

        Transform itemsParent = GameObject.Find("BonusPanel").transform;

        for(int i = 0; i < nbBonusAvailable; ++i)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            int bonusItemsIndex = Random.Range(0, bonusItems.Length);
            GameObject obj = Instantiate(bonusItems[bonusItemsIndex], position, transform.rotation);

            // get callback function
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(delegate { PickABonus(); });

            obj.transform.SetParent(itemsParent);
        }

    }

    void Update()
    {
        Vector3 position = bordersObj.transform.position;
        bordersObj.transform.position =  new Vector3(position.x, position.y + 0.25f, position.z);
        if (position.y > borderSizeY)
        {
            bordersObj.transform.position = new Vector3(position.x, 10.0f, position.z);
        }

    }

    void MovePlayer()
    {
        cameraScript.Unfollow();
        playerScript.gravityModifier = 3.0f;
        Invoke("GoToLevel", 1.0f);
    }

    void GoToLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        return null;
    }

    public void PickABonus()
    {
        MovePlayer();
        canvas.SetActive(false);
    }
}
