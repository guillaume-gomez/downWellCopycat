using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine;
using Pathfinding;

public class LevelGeneratorSimple : MonoBehaviour
{
    [Header("Tiles")]
    public Tilemap tileMap;
    public Tile tileBloc;
    [Space]
    [Header("References")]
    public GameObject beginRoom;
    public GameObject endRoom;
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

    public void Start() {
        SetupScene(1);
    }

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

        StaticRoom currentRoom = beginRoom.GetComponent<StaticRoom>();
        float totalOfThelevel = yOrigin + depthLevel;
        for(int y = yOrigin; y < totalOfThelevel; y+= roomHeight)
        {
            float percent = y / totalOfThelevel;

            GameObject obj = null;
            position.Set(xOrigin, -(y + yOrigin), 0.0f);

           
            obj = Instantiate(currentRoom.gameObject, position, transform.rotation);
            obj.transform.SetParent(spwawnHolder);

            currentRoom = currentRoom.getNextRoom();
        }
        //end room
        position.Set(xOrigin, -(totalOfThelevel + yOrigin), 0.0f);
        GameObject endRoomObj = Instantiate(endRoom, position, transform.rotation);
        endRoomObj.transform.SetParent(spwawnHolder);
    }

}
