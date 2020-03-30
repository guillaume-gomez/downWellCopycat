using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject bloc;
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

    void Awake()
    {
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
        int xMiddle = xSize / 2;
        int middleY = sizeYPathMax - sizeYPathMin;
        for(int y = yOrigin; y < (yOrigin + ySize); y += Random.Range(sizeYPathMin, sizeYPathMax + 1))
        //for(int y = yOrigin; y < (yOrigin + ySize); y++)
        {
            int sizeXHole = Random.Range(sizeXHoleMin, sizeXHoleMax + 1);
            int middleSizeXHole = sizeXHole / 2;

            int rangeSizeXHole = Random.Range(2, sizeXHoleMin - 2);
            int yRange = 0;//Random.Range(- middleY + 2 , middleY - 2);

            Vector3 position = new Vector3(0f, 0f, 0f);

            for(int x = xOrigin ; x < (xOrigin + xSize); ++x)
            {
                if(x <= (xOrigin + xMiddle - middleSizeXHole) || x >= (xOrigin + xMiddle + middleSizeXHole) )
                {
                    position.Set(x, -y, 0.0f);
                    GameObject obj = Instantiate(spawnObject, position, transform.rotation);
                    obj.transform.SetParent(spwawnHolder);
                }
                else if(x > (xOrigin + xMiddle - middleSizeXHole + rangeSizeXHole ) && x < (xOrigin + xMiddle + middleSizeXHole - rangeSizeXHole ) )
                {
                    Debug.Log(-(y + yRange));
                    position.Set(x, -(y + yRange), 0.0f);
                    GameObject obj = Instantiate(spawnObject, position, transform.rotation);
                    obj.transform.SetParent(spwawnHolder);
                }
            }
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
