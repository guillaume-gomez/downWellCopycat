using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    public bool isLeft = true;

    public void Init()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject choosedObject = objects[rand];
        Vector3 rotationVector = new Vector3(0f, 0f, 0f);
        if(!isLeft) {
          rotationVector = new Vector3(0f, 0f, 180f);
        }
        GameObject instance = (GameObject) Instantiate(choosedObject, transform.position + choosedObject.transform.position, Quaternion.Euler(rotationVector));
        instance.transform.parent = transform;
    }

}