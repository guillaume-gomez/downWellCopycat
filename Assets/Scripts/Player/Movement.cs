using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class Movement : MonoBehaviour
{
    private Collision coll;
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
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]
    public int side = 1;
    
    // allows to jump few frames before to be grounded
    private float jumpPressedRemember = 0.0f;
    // allow jump few frame after leaving the floor
    private float groundedRemember = 0.0f;
    private bool groundTouch;
    private bool hasDashed;
    private bool shoot;

    //[Space]
    //[Header("Polish")]
    // public ParticleSystem dashParticle;
    // public ParticleSystem jumpParticle;
    // public ParticleSystem wallJumpParticle;
    // public ParticleSystem slideParticle;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb2d = GetComponent<Rigidbody2D>();
        shoot = false;
        //anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir, xRaw);
        //anim.SetHorizontalMovement(x, y, rb2d.velocity.y);
        
        groundedRemember = groundedRemember - Time.deltaTime;
        if(coll.onGround)
        {
            groundedRemember = groundedRememberTime;
        }
        if(IsGrounded())
        {
            inventory.Reload();
        }

        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != coll.wallSide)
            {
                //anim.Flip(side*-1);
            }
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }
        
        if (wallGrab && !isDashing)
        {
            rb2d.gravityScale = 0;
            if(x > .2f || x < -.2f)
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb2d.velocity = new Vector2(rb2d.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb2d.gravityScale = 3;
        }

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }


        jumpPressedRemember = jumpPressedRemember - Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRemember = jumpPressedRememberTime;
            //anim.SetTrigger("jump");
            if(!IsGrounded())
            {
                shoot = true;
            }
            else if( IsGrounded() && CanJump()) // avoid double jump :p
            {
                Jump(Vector2.up, jumpForce, false);
            }

            if (coll.onWall && !coll.onGround)
            {
                WallJump();
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            shoot = false;
        }


        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            //anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            //anim.Flip(side);
        }

        if(shoot)
        {
            Shoot();
        }


    }

    bool CanJump()
    {
        return jumpPressedRemember > 0.0f;
    }

    bool IsGrounded()
    {
        return groundedRemember > 0.0f;
    }


    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        //TODO
        //side = anim.sr.flipX ? -1 : 1;

        //jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {
        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        //FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        //anim.SetTrigger("dash");

        rb2d.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb2d.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        //FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        //DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        //dashParticle.Play();
        rb2d.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        //dashParticle.Stop();
        rb2d.gravityScale = gravityScale;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            //anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), jumpForce, true);

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

        rb2d.velocity = new Vector2(push, -wallSlideSpeed);
    }

    private void Walk(Vector2 dir, float xRay)
    {
        if (!canMove)
        {
            return;
        }

        if (wallGrab)
        {
            return;
        }

        if (!wallJumped)
        {
            float x = dir.x;
            if (Mathf.Abs(xRay) < 0.01f)
            {
                x *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
            }
            else if (Mathf.Sign(xRay) != Mathf.Sign(x))
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
        //slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        //ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.velocity += dir * jumpTakeOffSpeed;

        //particle.Play();
    }

    private void Shoot()
    {
        if (inventory.CanShoot())
        {
            Jump(Vector2.up, inventory.Shoot(), false);
        }
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb2d.drag = x;
    }

    void WallParticle(float vertical)
    {
        // var main = slideParticle.main;

        // if (wallSlide || (wallGrab && vertical < 0))
        // {
        //     //slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        //     main.startColor = Color.white;
        // }
        // else
        // {
        //     main.startColor = Color.clear;
        // }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
}
