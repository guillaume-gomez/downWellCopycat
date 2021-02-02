using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public bool autoEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        if(autoEnable)
        {
            //Init();
        }

    }

    float computedYPosition(GameObject enemyInstance, int platformHeight)
    {
        float heightEnemy = enemyInstance.GetComponent<EnemyBase>().Height();
        float middleHeight = 0;
        if(heightEnemy % 2 == 0)
        {
            middleHeight = platformHeight/2;
        } else
        // Matfh.Ceil and Math.Floor is used to avoid colission with tilemap objects (no float number), and spawner (float number)
        {
            if(enemyInstance.transform.position.y < 0) // the enemy is above the platform. A convention :)
            {
                middleHeight = Mathf.Floor(platformHeight/2.0f);
            } else
            {
                middleHeight = Mathf.Ceil(platformHeight/2.0f);
            }
        }
        return transform.position.y + enemyInstance.transform.position.y * (middleHeight + (heightEnemy/2.0f));
    }

    float computedXPosition(GameObject enemyInstance)
    {
        float widthEnemy = enemyInstance.GetComponent<EnemyBase>().Width();
        PlatformPosition platformPosition = transform.parent.gameObject.GetComponent<SpawnObject>().platformPosition;
        if(PlatformPosition.Left == platformPosition)
        {
            return transform.position.x + (widthEnemy / 2.0f);
        } else if(PlatformPosition.Right == platformPosition)
        {
            return transform.position.x - (widthEnemy / 2.0f);
        }

        return transform.position.x;
    }

 
    public void Init(int platformWidth, int platformHeight)
    {
        float shouldDisplayRandom = Random.Range(0.0f, 1.0f);
        if(shouldDisplayRandom >= 0.75f) {
            return;
        }
        if(enemies.Length == 0)
        {
            return;
        }
        int rand = Random.Range(0, enemies.Length);
        GameObject choosedEnemy = enemies[rand];
        GameObject instance = (GameObject) Instantiate(choosedEnemy, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        EnemyBase enemyBaseScript = instance.GetComponent<EnemyBase>();

        int maxSize = Mathf.Min(
            GameManager.instance != null && GameManager.instance.LevelSystemRun != null ? GameManager.instance.LevelSystemRun.maxEnemyLife : 3,
            platformWidth - 1
        ); //-1 to make sure the enemy can move around the platform
        int lifeAnDamage = Random.Range(
            GameManager.instance != null && GameManager.instance.LevelSystemRun != null ? GameManager.instance.LevelSystemRun.minEnemyLife : 1,
            maxSize
        );
        enemyBaseScript.Life = lifeAnDamage;
        enemyBaseScript.Damage = lifeAnDamage;

        int minSpeed = GameManager.instance != null && GameManager.instance.LevelSystemRun != null ? GameManager.instance.LevelSystemRun.minEnemySpeed : 2;
        int maxSpeed = GameManager.instance != null && GameManager.instance.LevelSystemRun != null ? GameManager.instance.LevelSystemRun.maxEnemySpeed : 5;
        enemyBaseScript.speed = Random.Range(minSpeed, maxSpeed);

        
        Vector3 position = new Vector3(
            computedXPosition(choosedEnemy),
            computedYPosition(choosedEnemy, platformHeight),
            transform.position.z
        );
        instance.transform.position = position;
        instance.transform.parent = transform; // spawner
    }
}
