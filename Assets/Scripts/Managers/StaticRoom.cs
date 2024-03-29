using System.Collections;
using System.Collections.Generic;
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
        tilemap.CompressBounds();

        foreach (var tilemapPosition in tilemap.cellBounds.allPositionsWithin) {
            TileBase tile = tilemap.GetTile(tilemapPosition);
            destinationTilemap.SetTile(tilemapPosition + roomPosition, tile);
            destinationTilemap.SetTransformMatrix(tilemapPosition + roomPosition, tilemap.GetTransformMatrix(tilemapPosition));
        }

        tilemap.ClearAllTiles();
        Destroy(grid);
    }

    public StaticRoom GetNextRoom() {
        //float randomNumber = Random.Range(0.0f, 1.0f);
        int randomIndex = Random.Range(0, possibleRooms.Length);
        StaticRoom chosenRoom = possibleRooms[randomIndex];
        return chosenRoom;
    }

    private int GetRoomIndexFromPercentage(float randomNumber) {
        int indexChoosed = 0;
        float minDifference = 1.0f;
        for(int i = 0; i < percentagePossibleRooms.Length; ++i) {
            float difference = (percentagePossibleRooms[i] - randomNumber);
            if(minDifference > difference) {
                minDifference = difference;
                indexChoosed = i;
            }
        }
        return indexChoosed;
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

        Gizmos.color = Color.green;
        for(var x = offsetLeftAndRight;
                x <= width - 2 * offsetLeftAndRight;
                x += offsetLeftAndRight) {
            for(var y = 0; y < height; y += offsetLeftAndRight) {

                Gizmos.DrawWireCube(
                    new Vector3(
                        transform.position.x + x + offsetLeftAndRight/2.0f,
                        transform.position.y - y - offsetLeftAndRight/2.0f,
                        0),
                    new Vector3(
                        offsetLeftAndRight,
                        offsetLeftAndRight,
                        1)
                );
            }
        }

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
