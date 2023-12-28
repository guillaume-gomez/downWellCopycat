using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectRandomly : MonoBehaviour
{
    [Header("Spawnable objects")]
    public GameObject[] objects;
    public float[] percentages;
    
    public void Start()
    {
        if(objects.Length == 0)
        {
            return;
        }
        GameObject choosedObject = PickGameObject();
        GameObject instance = (GameObject) Instantiate(choosedObject, transform.position, transform.rotation);
        instance.transform.parent = transform;
    }

    private GameObject PickGameObject() {
        float randomNumber = Random.Range(0.0f, 1.0f);
        int indexChoosed = 0;
        float minDifference = 1.0f;
        for(int i = 0; i < percentages.Length; ++i) {
            float difference = (percentages[i] - randomNumber);
            if(minDifference > difference) {
                minDifference = difference;
                indexChoosed = i;
            }
        }
        return objects[indexChoosed];
    }

}