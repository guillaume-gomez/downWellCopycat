using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Spawnable objects")]
    public GameObject[] objects;
    public float[] percentages;
    public PlatformPosition platformPosition = PlatformPosition.Left;

    public void Init()
    {
        if(objects.Length == 0)
        {
            return;
        }
        int rand = PickGameObject();
        GameObject choosedObject = objects[rand];
        Vector3 rotationVector = new Vector3(0f, 0f, 0f);
        if(platformPosition == PlatformPosition.Right)
        {
          rotationVector = new Vector3(0f, 180f, 0.0f);
        }
        GameObject instance = (GameObject) Instantiate(choosedObject, transform.position + choosedObject.transform.position, Quaternion.Euler(rotationVector));
        instance.transform.parent = transform;
    }

    private int PickGameObject() {
        if(objects.Length != percentages.Length)
        {
            return Random.Range(0, objects.Length);
        } else
        {
            float random = Random.Range(0.0f, 1.0f);
            for(int index = 0; index < percentages.Length; index++)
            {
                if( random <= percentages[index]) {
                    return index;
                }
            }
            return 0;
        }
    }

}