using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public SpawnEnemy[] leftsEnemy;
    public SpawnEnemy[] rightsEnemy;
    public GameObject[] spawnersCenter;
    public GameObject[] spawnersLeft;
    public GameObject[] spawnersRight;
    [Range(0,1.0f)]
    public float percentageCenter = 1.0f;
    [Range(0,1.0f)]
    public float percentageSide = 1.0f;
    protected int widthSubRoom;
    protected int heightSubRoom;
    protected int offsetLeftAndRight;
    protected int width;
    protected int height;


    // Start is called before the first frame update
    void Start()
    {
        widthSubRoom = 6;
        heightSubRoom = 5;
        offsetLeftAndRight = 6;
        width = 36;
        height = 20;

        SplitInChunkY(4, 0, spawnersLeft);
        SplitInChunkY(4, 36, spawnersRight);

        for(float y = heightSubRoom/2.0f; y < height; y += heightSubRoom)
        {
            SplitInChunkX(4, y);
        }
    }

    protected void SplitInChunkX(int length, float y)
    {
        int i = 0;
        while(i < length)
        {
            int newChunk = PopSpwaner();
            if(i + newChunk > length) {
                // spawner has the max length possible
                newChunk = length - i;
            }
            if(Random.Range(0.0f, 1.0f) <= percentageCenter)
            {
                float x = offsetLeftAndRight + (i * widthSubRoom) + (newChunk * widthSubRoom) / 2.0f;
                CreateSpwaner(x, y, spawnersCenter, newChunk - 1);
            }
            i = i + newChunk;
        }
    }

    protected void SplitInChunkY(int length, float x, GameObject[] typeOfSpawn)
    {
        int i = 0;
        while(i < length)
        {
            int newChunk = PopSpwaner();
            if(i + newChunk > length) {
                // spawner has the max length possible
                newChunk = length - i;
            }
            if(Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                float y = (i * heightSubRoom) + (newChunk * heightSubRoom) / 2.0f;
                CreateSpwaner(x, y, typeOfSpawn, newChunk - 1);
            }
            i = i + newChunk;
        }
    }

    protected void CreateSpwaner(float x, float y, GameObject[] typeOfSpawn, int index)
    {
        Vector3 position = new Vector3(x, -y, transform.position.z);
        GameObject obj = Instantiate(typeOfSpawn[index], new Vector3(0,0,0), transform.rotation);
        obj.transform.parent = transform;
        obj.transform.localPosition = position;
        obj.GetComponent<SpawnObject>().Init();
    }

    protected int PopSpwaner()
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
}
