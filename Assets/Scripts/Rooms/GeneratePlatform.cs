using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GeneratePlatform : GeneratePlatformBase
{
    public Tilemap tilemap;
    public TileBase tile;
    private SpawnEnemy spawnEnemy;
    
    void Start()
    {
        SetSize();
        platformPosition = transform.parent.gameObject.GetComponent<SpawnObject>().platformPosition;
        spawnEnemy = GetComponent<SpawnEnemy>();
        tilemap =  GameObject.Find("Platforms").GetComponent<Tilemap>();

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
                Vector3Int cellPosition = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
                Vector3Int tilePosition = new Vector3Int(x,y,0);
                switch(platformPosition)
                {
                    case PlatformPosition.Left:
                         tilePosition = new Vector3Int(x, y - (height/2), 0);
                    break;
                    case PlatformPosition.Right:
                        tilePosition = new Vector3Int(- width + x, y - (height/2), 0);
                    break;
                    default:
                    case PlatformPosition.Center:
                        tilePosition = new Vector3Int(-(width/2) + x, y - (height/2), 0);
                    break;
                }
                
                tilemap.SetTile(cellPosition + tilePosition, tile);
            }
        }

        spawnEnemy.Init(width, height);
    }

    void OnDrawGizmos()
    {
        Vector3 position = new Vector3();
        switch(platformPosition)
        {
            case PlatformPosition.Left:
                position = new Vector3(transform.position.x + (xRangeMax / 2.0f), transform.position.y, 0);
                Gizmos.color = Color.magenta;
            break;
            case PlatformPosition.Right:
                position = new Vector3(transform.position.x - (xRangeMax / 2.0f), transform.position.y, 0);
                Gizmos.color = Color.green;
            break;
            default:
            case PlatformPosition.Center:
                position = transform.position;
                Gizmos.color = Color.yellow;
            break;
        }

        //sides
        Gizmos.DrawWireCube(
            position,
            new Vector3(
                xRangeMax,
                yRangeMax,
                1)
        );
    }
}