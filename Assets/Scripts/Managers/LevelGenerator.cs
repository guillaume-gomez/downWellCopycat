using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine;
using Pathfinding;

public class LevelGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public Tilemap tileMap;
    public Tile tileBloc;
    [Space]
    [Header("References")]
    public GameObject beginRoom;
    public GameObject endRoom;
    public GameObject caveRooms;
    public GameObject player;
    [Space]
    [Header("Level Characteristics")]
    // size of the level
    public int roomWidth = 36;
    public int roomHeight = 24;
    [Range(0,50)]
    public int nbRooms = 20;
    private int depthLevel;
    [Space]
    [Header("Positions")]
    // position of x or y
    public int xOrigin = 20;
    public int yOrigin = 0;

    public void DepthLevel()
    {
        depthLevel = roomHeight * nbRooms;
    }

    public void SetupScene(int level)
    {
        DepthLevel();
        SetPlayerInCenter();
        CreateBorders();
        SpawnRooms();
    }

    public void SetPlayerInCenter()
    {
        player.transform.position = new Vector3(xOrigin + roomWidth/2, yOrigin + player.transform.position.y, 0);
    }


    public void CreateBorders() {
        // avoid to see "void" a the beginning of the level
        int offsetBordersY = 10;
         //borders
        Vector3Int position = new Vector3Int(0, 0, 0);
        for(int y = yOrigin - (int)player.transform.position.y - offsetBordersY; y < (yOrigin + depthLevel); ++y)
        {
            position.Set(xOrigin -1, -y, 0);
            tileMap.SetTile(position, tileBloc);
            
            position.Set(xOrigin + roomWidth, -y, 0);
            tileMap.SetTile(position, tileBloc);
        }
    }

    private void SpawnRooms() {
        Transform spwawnHolder = new GameObject("Rooms").transform;
        spwawnHolder.transform.SetParent(transform);
        Vector3 position = new Vector3(0f, 0f, 0f);

        var roomsList = GenerateLevel();
        var y = yOrigin;
        foreach(GameObject room in roomsList)
        {
            position.Set(xOrigin, -(y + yOrigin), 0.0f);

            GameObject obj = Instantiate(room, position, transform.rotation);
            obj.transform.SetParent(spwawnHolder);

            y = y + roomHeight;
        }
    }

    private List<GameObject> GenerateLevel() {
        List<GameObject> roomsList = new List<GameObject>();
        
        roomsList.Add(beginRoom);

        StaticRoom currentRoom = beginRoom.GetComponent<StaticRoom>();
        for(int i = 0; i < nbRooms - 2; ++i) {
            currentRoom = currentRoom.GetNextRoom();
            roomsList.Add(currentRoom.gameObject);
        }
        roomsList.Add(endRoom);
        return roomsList;
    }

    // Start is called before the first frame update
    void Start()
    {
        //only for the test
        //DepthLevel();
        //end of test

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
        gridGraph.center.x = xOrigin + (roomWidth * nodeSize) / 2;
        gridGraph.center.y = yOrigin + (depthLevel * nodeSize) / 2;
        gridGraph.center = new Vector3(gridGraph.center.x, -gridGraph.center.y, gridGraph.center.z);

        // Updates internal size from the above values
        gridGraph.SetDimensions(roomWidth, depthLevel, nodeSize);

        AstarPath.active.Scan(gridGraph);

        Invoke("asyncScan", 2);

        //only for the test
        //SetupScene(1);
        // end of test
    }

    // some instantiation (like bloc) are not completed after Start LevelGenerator method.
    // So we call this function to scan again optimistically (render scene might take more than 2 seconds ?)
    void asyncScan() {
        AstarData data = AstarPath.active.data;
        data.cacheStartup = true;
        GridGraph gridGraph = data.gridGraph;
        AstarPath.active.Scan(gridGraph);
    }

}
