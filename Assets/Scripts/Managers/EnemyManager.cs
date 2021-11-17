using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static void UpdateEnemiesStats (int level)
    {
        EnemyBase[] components = GameObject.FindObjectsOfType<EnemyBase>();
        foreach(EnemyBase enemy in components) {
            enemy.life = enemy.Life + level;
            enemy.speed = enemy.Speed + level * 0.2f;
        }
    }
}