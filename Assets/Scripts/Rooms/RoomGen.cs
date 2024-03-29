﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformPosition{ Left, Right, Center }

public class RoomGen : MonoBehaviour
{
    [Header("Spawners")]
    public GameObject[] spawnEnemies;
    public GameObject[] spawnersCenter;
    public GameObject[] spawnersSide;
    [Space]
    [Header("Stats")]
    public bool overrideByGameManager = true;
    [Range(0,1.0f)]
    public float percentageCenter = 1.0f;
    [Range(0,1.0f)]
    public float percentageSide = 1.0f;
    
    protected int widthSubRoom;
    protected int heightSubRoom;
    protected int offsetLeftAndRight = 6;
    protected int width = 36;
    protected int height = 24;
    protected int nbSpawnersX = 4;
    protected int nbSpawnersY = 4;

    protected void Init()
    {
       /* if(overrideByGameManager && LevelManager.instance != null && LevelManager.instance.LevelScript != null) {
            percentageCenter = LevelManager.instance.LevelScript.spawnerPercentageCenter;
            percentageSide = LevelManager.instance.LevelScript.spawnerPercentageSide;
            nbSpawnersX = LevelManager.instance.LevelScript.nbSpawnersX;
            nbSpawnersY = LevelManager.instance.LevelScript.nbSpawnersY;
            offsetLeftAndRight = LevelManager.instance.LevelScript.offsetLeftAndRight;
            width = LevelManager.instance.LevelScript.roomWidth;
            height = LevelManager.instance.LevelScript.roomHeight;
        }*/
        widthSubRoom = (width - (2 * offsetLeftAndRight)) / nbSpawnersX; // 6
        heightSubRoom = height / nbSpawnersY; // 6
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
        SplitInChunkY(4, 0, spawnersSide, true);
        SplitInChunkY(4, width, spawnersSide, false);

        /*for(float y = heightSubRoom/2.0f; y < height; y += heightSubRoom)
        {
            SplitInChunkX(4, y);
        }*/
       SplitInChunkXY(4, 4);
    }

    protected void SplitInChunkY(int length, float x, GameObject[] typeOfSpawn, bool isLeft)
    {
        int i = 0;
        while(i < length)
        {
            int newChunk = PopSpwanerY();
            if(i + newChunk > length) {
                // spawner has the max length possible
                newChunk = length - i;
            }
            
            float y = (i * heightSubRoom) + (newChunk * heightSubRoom) / 2.0f;
            if(Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                CreateGenericBloc(x, y, typeOfSpawn, newChunk - 1, isLeft ? PlatformPosition.Left :  PlatformPosition.Right, 1, newChunk);
            } else {
                int randomIndex = Random.Range(0, spawnEnemies.Length);
                GameObject obj = CreateSpwaner(x, y, spawnEnemies, randomIndex);
                //obj.GetComponent<SpawnEnemy>().Init();
            }
            
            i = i + newChunk;
        }
    }

    protected void SplitInChunkXY(int xLength, int yLength)
    {
        int y = 0;
        while(y < yLength)
        {
            int newChunkY = PopSpwanerY();
            if(y + newChunkY > yLength) {
                // spawner has the max length possible
                newChunkY = yLength - y;
            }

            int x = 0;
            while(x < xLength)
            {
                int newChunkX = PopSpwanerX();
                if(x + newChunkX > xLength) {
                    // spawner has the max length possible
                    newChunkX = xLength - x;
                }
                float xPosition = offsetLeftAndRight + (x * widthSubRoom) + (newChunkX * widthSubRoom) / 2.0f;
                float yPosition = (y * heightSubRoom) + (newChunkY * heightSubRoom) / 2.0f;
                if(Random.Range(0.0f, 1.0f) <= percentageCenter)
                {
                    CreateGenericBloc(xPosition, yPosition, spawnersCenter, convertSpawnerToIndex(newChunkX, newChunkY), PlatformPosition.Center, newChunkX, newChunkY);
                }
                x += newChunkX;
            }
            y += newChunkY;
        }
    }

    protected virtual void CreateGenericBloc(float xPosition, float yPosition, GameObject[] spawners, int index, PlatformPosition platformPosition, int newChunkX, int newChunkY)
    {
        GameObject obj = CreateSpwaner(xPosition, yPosition, spawners, index);
        obj.GetComponent<SpawnObject>().platformPosition = platformPosition;
        obj.GetComponent<SpawnObject>().xIndex = newChunkX;
        obj.GetComponent<SpawnObject>().yIndex = newChunkY;
        obj.GetComponent<SpawnObject>().Init();
    }

    protected virtual GameObject CreateSpwaner(float x, float y, GameObject[] typeOfSpawn, int index)
    {
        Vector3 position = new Vector3(x, -y, transform.position.z);
        GameObject obj = Instantiate(typeOfSpawn[index], new Vector3(0,0,0), transform.rotation);
        obj.transform.parent = transform;
        obj.transform.localPosition = position;
        return obj;
    }

    protected int PopSpwanerX()
    {
        float spwanerSizePercentage = Random.Range(0.0f, 1.0f);
        if(spwanerSizePercentage <= 0.70f)
        {
            return 1;
        } else if(spwanerSizePercentage <= 0.80f)
        {
            return 2;
        } else if(spwanerSizePercentage <= 0.95f)
        {
            return 3;
        } else
        {
            return 4;
        }
    }

    protected int PopSpwanerY()
    {
        float spwanerSizePercentage = Random.Range(0.0f, 1.0f);
        if(spwanerSizePercentage <= 0.40f)
        {
            return 1;
        } else if(spwanerSizePercentage <= 0.90f)
        {
            return 2;
        } else if(spwanerSizePercentage <= 0.95f)
        {
            return 3;
        } else {
            return 1;
        }
    }

    protected int convertSpawnerToIndex(int x, int y)
    {
        if(x == 1 && y == 1)
        {
            return 0;
        } else if(x == 2 && y == 1)
        {
            return 1;
        } else if(x == 3 && y == 1)
        {
            return 2;
        }else if(x == 4 && y == 1)
        {
            return 3;
        }else if(x == 1 && y == 2)
        {
            return 4;
        }else if(x == 2 && y == 2)
        {
            return 5;
        } else if(x == 3 && y == 2)
        {
            return 6;
        }else if(x == 4 && y == 2)
        {
            return 7;
        }else if(x == 1 && y == 3)
        {
            return 8;
        }else if(x == 2 && y == 3)
        {
            return 9;
        }else if(x == 3 && y == 3)
        {
            return 10;
        } else
        {
            return 0;
        }
    }

    // unused
    protected void SplitInChunkX(int length, float y)
    {
        int i = 0;
        while(i < length)
        {
            int newChunk = PopSpwanerX();
            if(i + newChunk > length) {
                // spawner has the max length possible
                newChunk = length - i;
            }
            if(Random.Range(0.0f, 1.0f) <= percentageCenter)
            {
                float x = offsetLeftAndRight + (i * widthSubRoom) + (newChunk * widthSubRoom) / 2.0f;
                GameObject obj = CreateSpwaner(x, y, spawnersCenter, newChunk - 1);
            }
            i = i + newChunk;
        }
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
