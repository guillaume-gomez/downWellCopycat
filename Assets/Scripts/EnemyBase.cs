using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int enemyScore = 0;

    public void Hurt() {
      Destroy(this.gameObject);
      GameManager.instance.AddScore(enemyScore);
    }
}