using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
  public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject choosedObject = objects[rand];
        GameObject instance = (GameObject) Instantiate(choosedObject, transform.position + choosedObject.transform.position, choosedObject.transform.rotation);
        instance.transform.parent = transform;

        //Destroy(gameObject);
    }

}