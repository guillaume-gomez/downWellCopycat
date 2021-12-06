using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static void UpdateEnemiesStats (int level)
    {
        EnemyBase[] components = GameObject.FindObjectsOfType<EnemyBase>();
        foreach(EnemyBase enemy in components) {
            enemy.life = enemy.Life + (int)Mathf.Floor(level * 0.2f);
            enemy.speed = enemy.Speed + level * 0.2f;
        }
    }
}