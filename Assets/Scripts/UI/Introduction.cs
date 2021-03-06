using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Introduction : MonoBehaviour
{
    public LevelGenerator levelScript;
    public GameObject player;
    public GameObject canvas;
    public GameObject[] bonusItems;

    private GameObject bordersObj;
    private float borderSizeY = 50.0f;
    private int nbBonusAvailable = 4;


    void Start()
    {
        levelScript.DepthLevel();
        levelScript.CreateBorders();
        levelScript.SetPlayerInCenter();
        bordersObj = GameObject.Find("Borders");
        MovePlayer();

        Transform itemsParent = GameObject.Find("BonusPanel").transform;
        EventSystem.current.SetSelectedGameObject(null);

        for(int i = 0; i < nbBonusAvailable; ++i)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            int bonusItemsIndex = Random.Range(0, bonusItems.Length);
            GameObject obj = Instantiate(bonusItems[bonusItemsIndex], position, transform.rotation);
            // get callback function
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(delegate { PickABonus(); });

            obj.transform.SetParent(itemsParent, false);
            // set selected the first item
            if(i == 0)
            {

                EventSystem.current.SetSelectedGameObject(obj);
            }
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
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale += 0.1f;
        GameObject.Find("MyCamera").GetComponent<CamerFollow>().Unfollow();
    }

    void GoToLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PickABonus()
    {
        MovePlayer();
        canvas.SetActive(false);
        Invoke("GoToLevel", 1.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        GameObject.Find("MyCamera").GetComponent<CamerFollow>().Follow();
    }
}
