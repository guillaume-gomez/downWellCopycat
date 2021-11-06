using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GeneratePlatformBase : MonoBehaviour
{
    protected int width;
    protected int height;
    [Space]
    [Header("X values")]
    [Range(2, 24)]
    public int xRangeMin;
    [Range(2, 24)]
    public int xRangeMax;
    [Space]
    [Header("Y values")]
    [Range(1, 18)]
    public int yRangeMin;
    [Range(1, 18)]
    public int yRangeMax;
    [Space]
    public PlatformPosition platformPosition;
    public bool overrideGameManager = true;

    protected void computeRanges()
    {
        if(!LevelManager.instance || !LevelManager.instance.LevelScript)
        {
            return;
        }
        // DISABLE WHEN LEVELMANAGER BECOME LESS GENERATIVE 

       /* platformPosition = transform.parent.gameObject.GetComponent<SpawnObject>().platformPosition;

        int xIndex = transform.parent.gameObject.GetComponent<SpawnObject>().xIndex;
        int yIndex = transform.parent.gameObject.GetComponent<SpawnObject>().yIndex;

        int nbSpawnersX = LevelManager.instance.LevelScript.nbSpawnersX;
        int nbSpawnersY = LevelManager.instance.LevelScript.nbSpawnersY;
        int offsetLeftAndRight = LevelManager.instance.LevelScript.offsetLeftAndRight;
        int roomWidth = LevelManager.instance.LevelScript.roomWidth;
        int roomHeight = LevelManager.instance.LevelScript.roomHeight;

        int widthSubRoom = (platformPosition == PlatformPosition.Center) ?
            (roomWidth - (2 * offsetLeftAndRight)) / nbSpawnersX :
            offsetLeftAndRight;

        int heightSubRoom = roomHeight / nbSpawnersY;
        
        xRangeMin = Mathf.Max(2, (xIndex - 1) * widthSubRoom);
        xRangeMax = xIndex * widthSubRoom;

        yRangeMin = Mathf.Max(2, (yIndex - 1) * heightSubRoom);
        yRangeMax = yIndex * heightSubRoom;*/
    }

    protected void SetSize() {
        if(overrideGameManager)
        {
            computeRanges();
        }
        width = Random.Range(xRangeMin, xRangeMax);
        height = Random.Range(yRangeMin, yRangeMax);
    }

}