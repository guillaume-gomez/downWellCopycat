using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int enemyScore = 0;
    public float life = 1;

    public void Hurt(float loss) {
        life = life - loss;
        if (life <= 0.0f)
        {
            Destroy(this.gameObject);
            GameManager.instance.AddScore(enemyScore);
        }
    }
}