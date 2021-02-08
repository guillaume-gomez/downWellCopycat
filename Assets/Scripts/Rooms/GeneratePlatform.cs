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
        Debug.Log(width);
        Debug.Log(height);
        Debug.Log(")))))");
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


}