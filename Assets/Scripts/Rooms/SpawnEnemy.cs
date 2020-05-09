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

        Vector3 sizeEnemy = choosedEnemy.GetComponent<BoxCollider2D>().size;
        float heightEnemy = sizeEnemy.y;

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Vector3 sizePlatform = boxCollider2D.size;
        float heightPlatform = sizePlatform.y;

        Vector3 position;
        // above the platorm
        if(choosedEnemy.transform.position.y == 1)
        {
            position = new Vector3(transform.position.x, transform.parent.position.y + (heightPlatform/2.0f) , transform.position.z);
        }
        else // below the platform
        {
            position = new Vector3(transform.position.x, transform.parent.position.y - (heightEnemy/2.0f) - (heightPlatform/2.0f) , transform.position.z);
        }

        GameObject instance = (GameObject) Instantiate(choosedEnemy, position, transform.rotation);
        instance.transform.parent = transform.parent.transform; // spawner
        EnemyBase enemyBase = instance.GetComponent<EnemyBase>();
        if(enemyBase) {
            // only for Enmey with slots...
            enemyBase.SetSlotSize(sizePlatform);
            enemyBase.SetSlotPosition(transform.position);
        }

        // disable BoxCollider2D
        boxCollider2D.enabled = false;
    }
}
