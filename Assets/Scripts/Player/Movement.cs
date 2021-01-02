using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;


// TODOOOOOO
//fixer le slide particle 
//fixer les jumps trop fort

public class Movement : MonoBehaviour
{
    private Collision coll;
    private LifeScript lifeScript;
    private int amountOfJumpsLeft;
    [HideInInspector]
    public Rigidbody2D rb2d;
    public Inventory inventory;
    //private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float wallSlideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    [SerializeField]
    [Range(0,10)]
    public float gravityScale = 3;
    public int amountOfJumps = 1;

    [Space]
    [Header("Vertical Optimization")]
    [SerializeField]
    public float jumpPressedRememberTime = 0.2f;
    [SerializeField]
    public float groundedRememberTime = 0.25f;

    [Space]
    [Header("Horizontal Optimisation")]
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingBasic = 0.4f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingWhenStopping = 0.35f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingWhenTurning = 0.40f;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallJumped;
    public bool wallSlide;
    [Space]
    public int side = 1;
    
    private Vector2 movement;
    private float xRaw;
    // allows to jump few frames before to be grounded
    private float jumpPressedRemember = 0.0f;
    // allow jump few frame after leaving the floor
    private float groundedRemember = 0.0f;
    private bool groundTouch;
    private bool shooting;

    [Space]
    [Header("Polish")]
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb2d = GetComponent<Rigidbody2D>();
        lifeScript = GetComponent<LifeScript>();
        GetComponent<BetterJumping>().enabled = true;
        rb2d.gravityScale = gravityScale;
        amountOfJumpsLeft = amountOfJumps;
        movement = new Vector2(0,0);
        shooting = false;
        //anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        int oldSide = side;
        CheckInput();
        CheckMovementDirection();
        PlayParticles(oldSide);
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    bool CanJump()
    {
        return jumpPressedRemember > 0.0f;
    }

    bool IsGrounded()
    {
        return groundedRemember > 0.0f;
    }

    private void WallJump(float x)
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            //anim.Flip(side);
        }

        if(coll.onRightWall)
        {
            if(x >= 0.0f)
            {
                Jump(Vector2.up, jumpForce * 0.75f, true);
            }
            else
            {
                Jump(Vector2.left, jumpForce * 5.0f, true);
            }
        }
        else
        {
            if(x <= 0.0f)
            {
                Jump(Vector2.up, jumpForce * 0.75f, true);
            }
            else
            {
                Jump(Vector2.right, jumpForce * 5.0f, true);
            }
        }

        //Debug.Log(wallDir);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(coll.wallSide != side)
        {
            //anim.Flip(side * -1);
        }

        if (!canMove)
        {
            return;
        }

        bool pushingWall = false;
        if((rb2d.velocity.x > 0 && coll.onRightWall) || (rb2d.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb2d.velocity.x;
        if(rb2d.velocity.y < -wallSlideSpeed)
        {
            rb2d.velocity = new Vector2(push, -wallSlideSpeed);
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
        {
            return;
        }

        if (!wallJumped)
        {
            float x = dir.x;
            if (Mathf.Abs(xRaw) < 0.01f)
            {
                x *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
            }
            else if (Mathf.Sign(xRaw) != Mathf.Sign(x))
            {
                x *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
            }
            else
            {
                x *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);
            }
            rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);

        }
        else
        {
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, (new Vector2(dir.x * speed, rb2d.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, float jumpTakeOffSpeed, bool wall)
    {
        jumpPressedRemember = 0.0f;
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.velocity += dir * jumpTakeOffSpeed;
        particle.Play();
    }

    private void Shoot()
    {
        if (inventory.CanShoot())
        {
            Jump(Vector2.up, inventory.Shoot(), false);
        }
    }

    private void CheckInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(x, y);
        //anim.SetHorizontalMovement(x, y, rb2d.velocity.y);
        
        groundedRemember = groundedRemember - Time.deltaTime;
        if(coll.onGround)
        {
            groundedRemember = groundedRememberTime;
            wallJumped = false;
        }

        jumpPressedRemember = jumpPressedRemember - Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRemember = jumpPressedRememberTime;
            //anim.SetTrigger("jump");
            if(coll.onWall && !IsGrounded())
            {
                WallJump(xRaw);
            }
            else if(!IsGrounded())
            {
                shooting = true;
            }
            else if( IsGrounded() && CanJump()) // avoid double jump :p
            {
                Jump(Vector2.up, jumpForce, false);
            }

        }

        if(Input.GetButtonUp("Jump"))
        {
            shooting = false;
        }
    }

    void CheckMovementDirection()
    {
        if (!canMove)
        {
            return;
        }

        if(movement.x > 0)
        {
            side = 1;
            //anim.Flip(side);
        }
        if (movement.x < 0)
        {
            side = -1;
            //anim.Flip(side);
        }

        if(coll.onWall && !coll.onGround)
        {
            wallSlide = true;
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }
    }


    void ApplyMovement()
    {
        Walk(movement);
        if(IsGrounded())
        {
            inventory.Reload();
        }

        if(wallSlide)
        {
            WallSlide();
        }

        if(shooting)
        {
            Shoot();
        }
    }

    void PlayParticles(int oldSide)
    {
        if(side == -1 && oldSide == 1 || side == 1 && oldSide == -1)
        {
            dust.Play();
        }

        WallParticle(movement.y);

        if (coll.onGround && !groundTouch)
        {
            jumpParticle.Play();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(lifeScript.Unvisible) {
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
                if(rb2d.velocity.y > 0 && Math.Abs(point.normal.x) == 1.0f)
                {
                    hurtEnemyDuringJump = true;
                }
            }
        }
        if(hasJumpedOnEnemy)
        {
            Jump(Vector2.up, jumpForce * 0.75f, false);
            enemy.Hurt(inventory.GetDamage());
            if(enemy.Life == 0)
            {
                inventory.Reload();
            }
        }
        else if(hurtEnemyDuringJump)
        {
            enemy.Hurt(inventory.GetDamage());
            inventory.Reload();
        }
        else
        {
            lifeScript.Hurt(enemy);
        }
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide)
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
}
