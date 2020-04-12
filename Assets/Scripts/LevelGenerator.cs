﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Pathfinding;

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
        AstarData data = AstarPath.active.data;
        data.cacheStartup = true;
        GridGraph gridGraph = data.gridGraph;
        int nodeSize = 1;

        // Setting up the default parameters.
        gridGraph.width = xSize;
        gridGraph.depth = ySize;
        gridGraph.nodeSize = 1;

        // Calculating the centre based on node size and number of nodes
        gridGraph.center.x = xOrigin + (xSize * nodeSize) / 2;
        gridGraph.center.y = yOrigin + (ySize * nodeSize) / 2;
        gridGraph.center = new Vector3(gridGraph.center.x, -gridGraph.center.y, gridGraph.center.z);

        // Updates internal size from the above values
        gridGraph.UpdateSizeFromWidthDepth();

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
