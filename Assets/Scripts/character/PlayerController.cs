using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=44djqUTg2Sg
// https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs

// public class OnLifeChangedEventArgs : EventArgs
// {
//     public int life { get; set; }
//     public int diff { get; set;}
// }

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public float maxSpeed = 7;
    [SerializeField]
    public float jumpTakeOffSpeed = 7;
    [SerializeField]
    public float jumpPressedRememberTime = 0.2f;
    [SerializeField]
    public float groundedRememberTime = 0.2f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingWhenTurning = 0.5f;
    [SerializeField]
    [Range(0, 10)]
    public float fallMultiplier = 1f;
    [SerializeField]
    [Range(0, 10)]
    public float lowJumpMultiplier = 1f;
    public float vYmax = 9999f;

    public AudioClip jumpSound;
    public AudioClip hurtSound;

    public ParticleSystem dust;
    public bool godMode;

    public event EventHandler<OnLifeChangedEventArgs> OnLifeChanged;


    private HealthBar healthBar;
    private Vector2 velocity;
    protected Vector2 targetVelocity;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    protected string groundedName;
    protected bool grounded;
    public Transform groundCheck;
    // allows to jump few frames before to be grounded
    private float jumpPressedRemember = 0.0f;
    // allow jump few frame after leaving the floor
    private float groundedRemember = 0.0f;
    //private Animator animator;
    private Inventory inventory;
    private bool shoot;
    private bool unvisible = false;
    public float unvisibleTimer = 0.5f;


    protected new void Start()
    {
        if(GameManager.instance) {
            jumpTakeOffSpeed += GameManager.instance.CharacterStats.jumpTakeOffSpeed.Value;
            maxSpeed += GameManager.instance.CharacterStats.maxSpeed.Value;
        }
        shoot = false;
        spriteRenderer = GetComponent<SpriteRenderer> ();
        inventory = GetComponent<Inventory>();
        rb2d = GetComponent<Rigidbody2D> ();
        //animator = GetComponent<Animator> ();
    }

    private void FixedUpdate()
    {
        velocity = new Vector2(0,rb2d.velocity.y);
        targetVelocity = Vector2.zero;

        Debug.Log(rb2d.velocity.y);
        
        Vector2 move = Vector2.zero;

        if(Physics2D.Linecast(transform.position, groundCheck.position, 1<< LayerMask.NameToLayer("bloc")))
        {
            Debug.Log("grounded");
            grounded = true;
            groundedName = "Bloc";
        }
        else {
            Debug.Log("not grounded");
            grounded = false;
            groundedName = "";
        }

        move.x = Input.GetAxis ("Horizontal");
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
        {
            move.x *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(move.x))
        {
            move.x *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            move.x *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);
        }



        groundedRemember = groundedRemember - Time.deltaTime;
        if(grounded && groundedName != "Enemy") // do not taking account grounded when the enemy is the collider
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember = jumpPressedRemember - Time.deltaTime;


        if(Input.GetButtonDown("Jump")) {
            jumpPressedRemember = jumpPressedRememberTime;
            if(!IsGrounded()) {
                shoot = true;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            SoundManager.instance.PlaySingle(jumpSound);
            if (velocity.y > 0) {
                velocity.y = velocity.y * ( lowJumpMultiplier - 1.0f );
            }
            // in case of shooting
            shoot = false;
        }

        if (velocity.y < 0) {
            velocity.y = velocity.y * ( fallMultiplier - 1.0f);
        }

        if (CanJump() && IsGrounded())
        {
            CreateDust();
            jumpPressedRemember = 0.0f;
            groundedRemember = 0.0f;
            velocity.y = jumpTakeOffSpeed;
        }

        // limit the fall velocity
        if(velocity.y < -vYmax) {
            velocity.y = -vYmax;
        }

        if (inventory.CanShoot() && shoot) {
            velocity.y = inventory.Shoot();
        }

        if(IsGrounded())
        {
            inventory.Reload();
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            CreateDust();
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //animator.SetBool ("grounded", grounded);
        //animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;
        rb2d.velocity = targetVelocity + velocity;
    }

    bool CanJump()
    {
        return jumpPressedRemember > 0.0f;
    }

    bool IsGrounded()
    {
        return groundedRemember > 0.0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(unvisible) {
            return;
        }
        EnemyBase enemy = collision.collider.GetComponent<EnemyBase>();
        if(!enemy) {
            //Debug.Log(collision.collider.name);
            // otherwise it must be floor
            if(LevelManager.instance != null)
            {
                foreach(ContactPoint2D point in collision.contacts)
                {
                    if(point.normal.y >= 0.9f)
                    {
                        LevelManager.instance.ResetCombo();
                        return;
                    }
                }
            }
            return;
        }
        bool hasJumpedOnEnemy = false;
        bool hurtEnemyDuringJump = false;

        if(enemy.canBeJumped) // else you can jump on it so player will be hurt
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                Debug.DrawLine(point.point, point.point + point.normal, Color.red,100);
                // Debug.Log(point.normal);
                // if fall into enemy
                if( point.normal.y >= 0.9f)
                {
                  hasJumpedOnEnemy = true;
                }

                // contact with an enemy after the ascendant phase
                if(velocity.y > 0 && Math.Abs(point.normal.x) == 1.0f)
                {
                    hurtEnemyDuringJump = true;
                }
            }
        }
        if(hasJumpedOnEnemy)
        {
            velocity.y = jumpTakeOffSpeed * 0.75f;
        }
        else if(hurtEnemyDuringJump)
        {
            inventory.Reload();
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    public void SetToZero()
    {
        velocity = new Vector2(0.0f, 0.0f);
    }

    void OnDrawGizmo()
    {
    }

}
