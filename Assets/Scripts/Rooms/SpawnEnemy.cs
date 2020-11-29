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


    public void Init(int platformWidth, int platformHeight)
    {
        float shouldDisplayRandom = Random.Range(0.0f, 1.0f);
        if(shouldDisplayRandom >= 0.75f) {
            return;
        }
        int rand = Random.Range(0, enemies.Length);
        GameObject choosedEnemy = enemies[rand];
        GameObject instance = (GameObject) Instantiate(choosedEnemy, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);

        int lifeAnDamage =  Random.Range(1, 3);
        instance.GetComponent<EnemyBase>().Life = lifeAnDamage;
        instance.GetComponent<EnemyBase>().Damage = lifeAnDamage;

        float heightEnemy = instance.GetComponent<EnemyBase>().Height();
        // Matfh.Ceil is used to avoid colission with tilemap objects (no float number), and spawner (float number)
        Vector3 position = new Vector3(
            transform.position.x,
            transform.position.y + choosedEnemy.transform.position.y * ( Mathf.Ceil(platformHeight/2.0f) + (heightEnemy/2.0f)),
            transform.position.z
        );
        instance.transform.position = position;
        instance.transform.parent = transform; // spawner
    }
}
