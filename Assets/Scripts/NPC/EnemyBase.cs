using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class EnemyConstants
{
    public static readonly Color[] ColorsEnemy = {
        new Color( 0.0f, 0.0f, 1.0f , 1.0f ),
        new Color( 0.0f, 0.0f, 0.5f, 1.0f ),
        new Color( 0.0f, 0.0f, 0.25f, 1.0f ),
    };

    public static readonly Vector3[] Size = {
        new Vector3( 1.0f, 1.0f, 1.0f ),
        new Vector3( 1.75f, 1.75f, 1.75f ),
        new Vector3( 2.5f, 2.5f, 2.5f ),
    };

    public static readonly float[] Damage = {
        0.5f,
        1.0f,
        2.0f,
    };
}


public class EnemyBase : MonoBehaviour
{
    public Transform target;
    public int enemyScore = 0;
    public float damage = 0.0f;
    [Range(1, 3)]
    public float life = 1;
    public GameObject coin;
    protected Vector3 slotSize;
    protected Vector3 slotPosition;
    private SpriteRenderer sprite;
    public bool canBeJumped = true;

    public float Life {
        get => life;
        set {
            life = value;
            computeColor();
        }
    }

    public float Damage {
        get => damage;
        set {
            damage = value;
            computeSize();
        }
    }


    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
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
        computeColor();

        if (life <= 0.0f)
        {
            Destroy(this.gameObject);
            LevelManager.instance.AddScore(enemyScore);
            LevelManager.instance.IncEnemyKill();
            LevelManager.instance.IncCombo();
            Instantiate(coin, transform.position, transform.rotation);
        }
    }

    protected bool CannotMove()
    {
        return LevelManager.PauseGame;
    }

    public void SetSlotSize(Vector3 _slotSize)
    {
        slotSize = _slotSize;
    }

    public void SetSlotPosition(Vector3 _position)
    {
        slotPosition = _position;
    }

    public virtual float Height()
    {
        return transform.localScale.y;
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

    private void computeSize()
    {
        Vector3 size = new Vector3();
        switch(damage) {
            case 1:
                transform.localScale = EnemyConstants.Size[0];
            break;
            case 2:
                transform.localScale = EnemyConstants.Size[1];
            break;
            case 3:
                transform.localScale = EnemyConstants.Size[2];
            break;
            default:
                transform.localScale = EnemyConstants.Size[2];
            break;
        }
    }

}