using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        float shouldDisplayRandom = Random.Range(0.0f, 1.0f);
        if(shouldDisplayRandom >= 1.0f) {
            return;
        }
        int rand = Random.Range(0, enemies.Length);
        GameObject choosedEnemy = enemies[rand];
        Vector3 size = GetComponent<BoxCollider2D>().size;
        float height = size.y;
        GameObject instance = (GameObject) Instantiate(choosedEnemy, transform.position + Vector3.Scale(size/2.0f, choosedEnemy.transform.position), transform.rotation);
        instance.transform.parent = transform;
        EnemyBase enemyBase = instance.GetComponent<EnemyBase>();
        if(enemyBase) {
            // only for Enmey with slots...
            enemyBase.SetSlotSize(size);
            enemyBase.SetSlotPosition(transform.position);
        }
    }
}
