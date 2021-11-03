using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine;


public class StaticRoom : MonoBehaviour
{
    [Header("Setup")]
    public Tilemap tilemap;
    public Grid grid;
    public StaticRoom[] possibleRooms;
    public int[] percentagePossibleRooms;

    protected int offsetLeftAndRight = 6;
    protected int width = 36;
    protected int height = 24;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        copyTile();
    }

    private void copyTile()
    {

        Tilemap destinationTilemap =  GameObject.Find("Platforms").GetComponent<Tilemap>();

        Vector3Int roomPosition = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);

        foreach (var tilemapPosition in tilemap.cellBounds.allPositionsWithin) {
            TileBase tile = tilemap.GetTile(tilemapPosition);
            destinationTilemap.SetTile(tilemapPosition + roomPosition, tile);
            destinationTilemap.SetTransformMatrix(tilemapPosition + roomPosition, tilemap.GetTransformMatrix(tilemapPosition));
        }

        tilemap.ClearAllTiles();
        Destroy(grid);
    }

    public StaticRoom getNextRoom() {
        //float randomNumber = Random.Range(0.0f, 1.0f);
        int randomIndex = Random.Range(0, possibleRooms.Length);
        StaticRoom chosenRoom = possibleRooms[randomIndex];
        return chosenRoom;
    }


     void OnDrawGizmos()
    {
        //sides
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(
            new Vector3(
                transform.position.x + offsetLeftAndRight/2.0f,
                transform.position.y - height/2.0f,
                0),
            new Vector3(
                offsetLeftAndRight,
                height,
                1)
        );

        Gizmos.DrawWireCube(
            new Vector3(
                transform.position.x - offsetLeftAndRight/2.0f + width,
                transform.position.y - height/2.0f,
                0),
            new Vector3(
                offsetLeftAndRight,
                height,
                1)
        );

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector3(
                transform.position.x + width/2.0f,
                transform.position.y - height/2.0f,
                0),
            new Vector3(
                width - (2.0f * offsetLeftAndRight),
                height,
                1)
        );
    }

}
