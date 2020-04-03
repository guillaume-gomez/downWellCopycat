using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject bloc;
    public GameObject[] rooms;
    public GameObject spawnObject;
    // size of the level
    public int xSize = 50;
    public int ySize = 100;
    // position of x or y
    public int xOrigin = 20;
    public int yOrigin = 0;

    public int sizeXHoleMin = 2;
    public int sizeXHoleMax = 10;
    public int sizeYPathMin = 5;
    public int sizeYPathMax = 10;

    private int roomHeight = 0;

    void Awake()
    {
       roomHeight = 20;
       CreateBorders();
       SpawnObjects();
    }

    private void CreateBorders() {
        Transform boardHolder = new GameObject("Borders").transform;
        boardHolder.transform.SetParent(transform);
         //borders
        Vector3 position = new Vector3(0f, 0f, 0f);
        for(int y = yOrigin; y < (yOrigin + ySize); ++y)
        {
            position.Set(xOrigin - 1 , -y, 0.0f);
            GameObject obj = Instantiate(bloc, position, transform.rotation); // left
            obj.transform.SetParent(boardHolder);

            position.Set(xOrigin + xSize, -y, 0.0f);
            obj = Instantiate(bloc, position, transform.rotation); // right
            obj.transform.SetParent(boardHolder);
        }

        for(int x = xOrigin - 1 ; x < (xOrigin + xSize) + 1; ++x)
        {
            position.Set(x, - (yOrigin + ySize), 0.0f);
            GameObject obj = Instantiate(bloc, position, transform.rotation);
            obj.transform.SetParent(boardHolder);
        }
    }

    private void SpawnObjects() {
        Transform spwawnHolder = new GameObject("SpawnObjects").transform;
        spwawnHolder.transform.SetParent(transform);
        Vector3 position = new Vector3(0f, 0f, 0f);
        for(int y = yOrigin + roomHeight; y <= yOrigin + ySize; y+= roomHeight){
            position.Set(xOrigin , -y, 0.0f);
            GameObject obj = Instantiate(rooms[Random.Range(0, rooms.Length)], position, transform.rotation);
            obj.transform.SetParent(spwawnHolder);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
