using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class RoomsTester : MonoBehaviour
{
    public Tile tileBloc;
    public Tilemap tileMap;
    public GameObject[] rooms;
    private int roomHeight = 24;
    private int roomWidth = 36;

    void Start()
    {
      CreateBorders();
      CreateRooms();
    }

    void CreateRooms() {
      int x = 0;
      int y = 0;
      foreach(GameObject room in rooms)
      {
        Instantiate(room, new Vector3(x, -y, 0), transform.rotation);
        y = y + roomHeight;
      }
    }

    void CreateBorders() {
       //borders
      Vector3Int position = new Vector3Int(0, 0, 0);
      for(int y = 0; y < roomHeight * rooms.Length; ++y)
      {
          position.Set(-1, -y, 0);
          tileMap.SetTile(position, tileBloc);
          
          position.Set(roomWidth, -y, 0);
          tileMap.SetTile(position, tileBloc);
      }
    }

}
