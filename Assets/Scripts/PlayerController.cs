using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

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
    [Range(0, 1)]
    public float cutJumpHeight = 0.5f;


    private SpriteRenderer spriteRenderer;
    // allows to jump few frames before to be grounded
    private float jumpPressedRemember = 0.0f;
    // allow jump few frame after leaving the floor
    private float groundedRemember = 0.0f;
    //private Animator animator;
    private Weapon weapon;
    private bool shoot;


    // Use this for initialization
    void Awake ()
    {
        shoot = false;
        spriteRenderer = GetComponent<SpriteRenderer> ();
        //animator = GetComponent<Animator> ();
        weapon = transform.Find("WeaponPosition").Find("Weapon").GetComponent<Weapon>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

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
        if(grounded)
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

        if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * cutJumpHeight;
            }
            // in case oof shooting
            shoot = false;
        }

        if (CanJump() && IsGrounded())
        {
            jumpPressedRemember = 0.0f;
            groundedRemember = 0.0f;
            velocity.y = jumpTakeOffSpeed;
 
            weapon.Reload();
        }

        if(weapon.CanShoot() && shoot) {
            velocity.y = weapon.Shoot();
        }

        if(IsGrounded())
        {
            weapon.Reload();
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite) 
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //animator.SetBool ("grounded", grounded);
        //animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;
    }

    bool CanJump()
    {
        return jumpPressedRemember > 0.0f;
    }

    bool IsGrounded()
    {
        return groundedRemember > 0.0f;
    }

    void Hurt()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(!enemy) {
            return;
        }
        foreach(ContactPoint2D point in collision.contacts)
        {
            Debug.DrawLine(point.point, point.point + point.normal, Color.blue,10);
            if( point.normal.y >= 0.9f )
            {
                velocity.y = jumpTakeOffSpeed;
                enemy.Hurt();
            } else
            {
                Hurt();
            }
        }
    }
}
