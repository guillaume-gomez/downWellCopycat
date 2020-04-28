using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, enemies.Length);
        GameObject choosedEnemy = enemies[rand];
        Vector3 offsetPosition = new Vector3(0.0f, 1.0f, 0.0f);
        GameObject instance = (GameObject) Instantiate(choosedEnemy, transform.position + offsetPosition, transform.rotation);
        instance.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
