using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine;
using Pathfinding;

public class LevelGenerator : MonoBehaviour
{
    public Tilemap tileMap;
    public Tile tileBloc;
    public GameObject[] rooms;
    public GameObject endRoom;
    public GameObject caveRoom;
    public GameObject player;

    // size of the level
    public int roomWidth;
    private int roomHeight;

    public int depthLevel = 100;
    // position of x or y
    public int xOrigin = 20;
    public int yOrigin = 0;


    public void SetupScene(int level)
    {
       roomHeight = 24;
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
        // bottom
        // for(int x = xOrigin - 1 ; x <= (xOrigin + roomWidth) + 1; ++x)
        // {
        //     position.Set(x, - (yOrigin + depthLevel), 0.0f);
        //     GameObject obj = Instantiate(bloc, position, transform.rotation);
        //     obj.transform.SetParent(boardHolder);
        // }
    }

    private void SpawnRooms() {
        // cave gen
        float nbCaves = Random.Range(0.0f, 2.0f);
        int nbElapsedcave = (int) nbCaves;

        Transform spwawnHolder = new GameObject("Rooms").transform;
        spwawnHolder.transform.SetParent(transform);
        Vector3 position = new Vector3(0f, 0f, 0f);

        float totalOfThelevel = yOrigin + depthLevel;
        for(int y = yOrigin; y < totalOfThelevel; y+= roomHeight)
        {
            float percent = y / totalOfThelevel;

            GameObject obj = null;
            position.Set(xOrigin, -(y + yOrigin), 0.0f);

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
        position.Set(xOrigin, -(totalOfThelevel + yOrigin), 0.0f);
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
        gridGraph.center.x = xOrigin + (roomWidth * nodeSize) / 2;
        gridGraph.center.y = yOrigin + (depthLevel * nodeSize) / 2;
        gridGraph.center = new Vector3(gridGraph.center.x, -gridGraph.center.y, gridGraph.center.z);

        // Updates internal size from the above values
        gridGraph.SetDimensions(roomWidth, depthLevel, nodeSize);

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

}
