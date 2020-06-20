using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class EnemyConstants
{
    public static readonly Color[] ColorsEnemy = {
        new Color( 0.0f, 0.0f, 1.0f , 1.0f ),
        new Color( 0.0f, 0.0f, 0.65f, 1.0f ),
        new Color( 0.0f, 0.0f, 0.49f, 1.0f ),
    };
}


public class EnemyBase : MonoBehaviour
{
    private SpriteRenderer sprite;
    public int enemyScore = 0;
    [Range(1, 4)]
    public float life = 1;
    protected Vector3 slotSize;
    protected Vector3 slotPosition;
    public Transform target;

    public float Life {
        get => life;
        set => life = value;
    }


    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        computeColor();
    }

    protected void Start()
    {
        // in case someone forgot to assign it
        if(!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void Hurt(float loss)
    {
        life = life - loss;
        if (life <= 0.0f)
        {
            Destroy(this.gameObject);
            LevelManager.instance.AddScore(enemyScore);
            LevelManager.instance.IncCombo();
            LevelManager.instance.IncEnemyKill();
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

    private void computeColor()
    {
        Color color = new Color();
        switch(life) {
            case 1:
                color = EnemyConstants.ColorsEnemy[0];
            break;
            case 2:
                color = EnemyConstants.ColorsEnemy[1];
            break;
            case 3:
                color = EnemyConstants.ColorsEnemy[2];
            break;
            default:
                color = EnemyConstants.ColorsEnemy[2];
            break;
        }
        sprite.color = color;
    }
}