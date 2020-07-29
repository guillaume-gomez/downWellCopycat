using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public bool autoEnable = true;
    private Transform parentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        if(autoEnable)
        {
            Init();
        }
    }

    void SetEnemyParent()
    {
        if(autoEnable)
        {
            parentEnemy = transform.parent;
        }
        else
        {
            parentEnemy = transform;
        }
    }

    public void Init()
    {
        SetEnemyParent();
        float shouldDisplayRandom = Random.Range(0.0f, 1.0f);
        if(shouldDisplayRandom >= 0.75f) {
            return;
        }
        int rand = Random.Range(0, enemies.Length);
        GameObject choosedEnemy = enemies[rand];
        GameObject instance = (GameObject) Instantiate(choosedEnemy, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);

        float lifeAnDamage =  Random.Range(1, 3);
        instance.GetComponent<EnemyBase>().Life = lifeAnDamage;
        instance.GetComponent<EnemyBase>().Damage = lifeAnDamage;

        float heightEnemy = instance.GetComponent<EnemyBase>().Height();

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        float heightPlatform = 0.0f;
        if(boxCollider2D != null)
        {
            Vector3 sizePlatform = boxCollider2D.size;
            heightPlatform = sizePlatform.y;
        }

        Vector3 position;
        // above the platorm
        if(choosedEnemy.transform.position.y == 1)
        {
            position = new Vector3(transform.position.x, parentEnemy.position.y + (heightPlatform/2.0f) , transform.position.z);
        }
        else // below the platform
        {
            position = new Vector3(transform.position.x, parentEnemy.position.y - (heightEnemy/2.0f) - (heightPlatform/2.0f) , transform.position.z);
        }

        instance.transform.position = position;
        instance.transform.parent = parentEnemy; // spawner
    }
}
