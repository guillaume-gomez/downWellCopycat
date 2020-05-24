using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int enemyScore = 0;
    public float life = 1;
    protected Vector3 slotSize;
    protected Vector3 slotPosition;

    public void Hurt(float loss)
    {
        life = life - loss;
        if (life <= 0.0f)
        {
            Destroy(this.gameObject);
            LevelManager.instance.AddScore(enemyScore);
            LevelManager.instance.IncCombo();
        }
    }

    protected bool CannotMove()
    {
        return LevelManager.instance.PauseGame;
    }

    public void SetSlotSize(Vector3 _slotSize)
    {
        slotSize = _slotSize;
    }

    public void SetSlotPosition(Vector3 _position)
    {
        slotPosition = _position;
    }
}