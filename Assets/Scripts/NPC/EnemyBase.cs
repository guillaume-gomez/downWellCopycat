using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class EnemyConstants
{
    public static readonly Vector3[] Size = {
        new Vector3( 1.0f, 1.0f, 1.0f ),
        new Vector3( 2.0f, 2.0f, 2.0f ),
        new Vector3( 3.0f, 3.0f, 3.0f ),
        new Vector3( 4.0f, 4.0f, 4.0f ),
    };

    public static readonly int[] Damage = {
        1,
        1,
        2,
        3
    };
}


public class EnemyBase : MonoBehaviour
{
    public Transform target;
    [Header("Characteristics")]
    public int enemyScore = 0;
    public int damage = 0;
    public float speed;
    [Range(1, 10)]
    public int life = 1;
    public Coin coin;
    [Range(1, 30)]
    public int coinValue;
    public bool canBeJumped = true;
    protected Vector3 slotSize;
    protected Vector3 slotPosition;
    protected bool isVisibleOnCamera;
    private SpriteRenderer sprite;
    [Space]
    [Header("Particles")]
    public GameObject damageParticle;
    [Space]
    [Header("Sounds")]
    public AudioClip[] dieSounds;

    public int Life {
        get => life;
        set {
            life = value;
            computeColor();
        }
    }

    public int Damage {
        get => damage;
        set {
            damage = value;
            computeSize();
        }
    }

     public float Speed {
        get => speed;
        set {
            speed = value;
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
        isVisibleOnCamera = true;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        // make sure enemies are heavier than player (to avoid collision issues)
        rb2d.mass = 100;
    }

    protected void OnBecameVisible()
    {
        isVisibleOnCamera = true;
    }

    protected void OnBecameInvisible()
    {
        isVisibleOnCamera = false;
    }

    public void Hurt(int loss)
    {
        life = life - loss;
        computeColor();

        if (life <= 0.0f)
        {
            Destroy(this.gameObject);
            SoundManager.instance.RandomizeSfx(dieSounds);
            LevelManager.instance.AddScore(enemyScore);
            LevelManager.instance.IncEnemyKill();
            LevelManager.instance.IncCombo();
            if(coinValue > 0)
            {
                Coin coinInstance = Instantiate(coin, transform.position, transform.rotation) as Coin;
                coinInstance.CoinValue = coinValue;
            }
            Instantiate(damageParticle, transform.position, transform.rotation);
        } else {
            invertColor();
            Invoke("BackToNormalColor", 0.1f);
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

    public virtual float Width()
    {
        return transform.localScale.x;
    }

    void invertColor()
    {
        Color revertColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        sprite.color = revertColor;
    }

    private void computeColor()
    {
        Color color = new Color();
        switch(life) {
            case 1:
                color = Lighten(.33f);
            break;
            case 2:
                color = Lighten(.66f);
            break;
            case 3:
                color = Lighten(.99f);
            break;
            default:
                color = Lighten(.99f);
            break;
        }
        sprite.color = color;
    }

    private Color Lighten(float percentage)
    {
        return Color.Lerp(sprite.color, Color.white, percentage);
    }

    private void computeSize()
    {
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
            case 4:
                transform.localScale = EnemyConstants.Size[3];
            break;
            default:
                transform.localScale = EnemyConstants.Size[1];
            break;
        }
    }

    void BackToNormalColor()
    {
        computeColor();
    }

}