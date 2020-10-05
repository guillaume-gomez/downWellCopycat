using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    public SpawnObject spawner;
    public GameObject[] items;

    private int xOffsetMax = 30;
    private int yOffsetMax =  20;
    private int nbItems = 10;

    void Start()
    {
      int x = 0;
      int y = 0;
      foreach(GameObject item in items)
      {
        SpawnObject instance = (SpawnObject) Instantiate(spawner, new Vector3(x * xOffsetMax, -y, 0), transform.rotation);
        instance.objects =  new GameObject[1];
        instance.name = item.name;
        instance.objects[0] = item;
        instance.Init();

        x = x + 1;

        if(x > nbItems) {
          y += xOffsetMax;
          x = 0;
        }
      }
    }
}
