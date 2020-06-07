using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Pathfinding;

public class LevelGenerator : MonoBehaviour
{
    public GameObject bloc;
    public GameObject[] rooms;
    public GameObject endRoom;
    public GameObject caveRoom;
    public GameObject spawnObject;

    // size of the level
    public int xSize = 50;
    public int ySize = 100;
    // position of x or y
    public int xOrigin = 20;
    public int yOrigin = 0;

    private int roomHeight = 0;

    public void SetupScene(int level)
    {
       roomHeight = 20;
       CreateBorders();
       SpawnObjects();
    }


    public void CreateBorders() {
        Transform boardHolder = new GameObject("Borders").transform;
        boardHolder.transform.SetParent(transform);
         //borders
        Vector3 position = new Vector3(0f, 0f, 0f);
        for(int y = yOrigin; y < (yOrigin + ySize); ++y)
        {
            position.Set(xOrigin - 1 , -y, 0.0f);
            GameObject obj = Instantiate(bloc, position, transform.rotation); // left
            obj.transform.SetParent(boardHolder);

            position.Set(xOrigin + xSize + 1, -y, 0.0f);
            obj = Instantiate(bloc, position, transform.rotation); // right
            obj.transform.SetParent(boardHolder);
        }

        // bottom
        // for(int x = xOrigin - 1 ; x <= (xOrigin + xSize) + 1; ++x)
        // {
        //     position.Set(x, - (yOrigin + ySize), 0.0f);
        //     GameObject obj = Instantiate(bloc, position, transform.rotation);
        //     obj.transform.SetParent(boardHolder);
        // }
    }

    private void SpawnObjects() {
        // cave gen
        float nbCaves = Random.Range(0.0f, 2.0f);
        int nbElapsedcave = (int) nbCaves;

        Transform spwawnHolder = new GameObject("SpawnObjects").transform;
        spwawnHolder.transform.SetParent(transform);
        Vector3 position = new Vector3(0f, 0f, 0f);
        
        float totalOfThelevel = yOrigin + ySize;
        for(int y = yOrigin + roomHeight; y < totalOfThelevel; y+= roomHeight)
        {
            float percent = y / totalOfThelevel;
            
            GameObject obj = null;
            position.Set(xOrigin + xSize/2 , -y + 10, 0.0f);

            if(nbElapsedcave > 0 && percent >= 0.4 && percent <= 0.8 && Random.Range(0.0f,1.0f) >= 0.5f ) // cave room
            {
                obj = Instantiate(caveRoom, position, transform.rotation);
                nbElapsedcave = nbElapsedcave - 1;
            } else // general room
            {
                obj = Instantiate(rooms[Random.Range(0, rooms.Length)], position, transform.rotation);
            }
            obj.transform.SetParent(spwawnHolder);
        }
        //end room
        position.Set(xOrigin + xSize/2 , -(yOrigin + ySize), 0.0f);
        GameObject endRoomObj = Instantiate(endRoom, position, transform.rotation);
        endRoomObj.transform.SetParent(spwawnHolder);
    }

    // Start is called before the first frame update
    void Start()
    {
        // for instance scene intro
        if(!GameObject.Find("A*"))
        {
            return;
        }
        AstarData data = AstarPath.active.data;
        data.cacheStartup = true;
        GridGraph gridGraph = data.gridGraph;
        int nodeSize = 1;

        // Calculating the centre based on node size and number of nodes
        gridGraph.center.x = xOrigin + (xSize * nodeSize) / 2;
        gridGraph.center.y = yOrigin + (ySize * nodeSize) / 2;
        gridGraph.center = new Vector3(gridGraph.center.x, -gridGraph.center.y, gridGraph.center.z);

        // Updates internal size from the above values
        gridGraph.SetDimensions(xSize, ySize, nodeSize);

        AstarPath.active.Scan(gridGraph);

        Invoke("asyncScan", 2);
    }

    // some instantiation (like bloc) are not completed after Start LevelGenerator method.
    // So we call this function to scan again optimistically (render scene might take more than 2 seconds ?)
    void asyncScan() {
        AstarData data = AstarPath.active.data;
        data.cacheStartup = true;
        GridGraph gridGraph = data.gridGraph;
        AstarPath.active.Scan(gridGraph);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
