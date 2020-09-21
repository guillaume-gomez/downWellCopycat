using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public SpawnObject[] lefts;
    public SpawnEnemy[] leftsEnemy;
    public SpawnObject[] rights;
    public SpawnEnemy[] rightsEnemy;
    public GameObject[] spawnerCenters;
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
        widthSubRoom = 10;
        heightSubRoom = 5;
        offsetLeftAndRight = 3;
        width = 36;
        height = 20;

        for(int i = 0; i < lefts.Length; i++)
        {
            if(lefts[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                lefts[i].Init();
            }
            else if(
                i < leftsEnemy.Length &&
                leftsEnemy[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                leftsEnemy[i].Init();
            }
        }

        for(int i = 0; i < rights.Length; i++)
        {
            if(rights[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                rights[i].Init();
            }
            else if(
                i < rightsEnemy.Length &&
                rightsEnemy[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                rightsEnemy[i].Init();
            }
        }
        for(float y = heightSubRoom/2.0f; y < height; y += heightSubRoom)
        {
            SplitInChunk(3, y);
        }
    }

    protected void SplitInChunk(int length, float y)
    {
        int i = 0;
        while(i < length)
        {
            int newChunk = PopSpwaner();
            if(i + newChunk > length) {
                // spawner has the max length possible
                newChunk = ((i + newChunk) % length);
            }

            if(Random.Range(0.0f, 1.0f) <= percentageCenter)
            {
                float x = offsetLeftAndRight + (i * widthSubRoom) + (newChunk * widthSubRoom) / 2.0f;
                Vector3 position = new Vector3(x, -y, transform.position.z);
                GameObject obj = Instantiate(spawnerCenters[newChunk - 1], new Vector3(0,0,0), transform.rotation);
                obj.transform.parent = transform;
                obj.transform.localPosition = position;
                obj.GetComponent<SpawnObject>().Init();
            }


            i = i + newChunk;
        }
    }

    protected int PopSpwaner()
    {
        float spwanerSizePercentage = Random.Range(0.0f, 1.0f);
        if(spwanerSizePercentage <= 0.80f)
        {
            return 1;
        } else if(spwanerSizePercentage <= 0.95f)
        {
            return 2;
        } else
        {
            return 3;
        }
    }
}
