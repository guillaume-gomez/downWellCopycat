﻿using System;
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
    [Range(0, 10)]
    public float fallMultiplier = 1f;
    [SerializeField]
    [Range(0, 10)]
    public float lowJumpMultiplier = 1f;
    public float vYmax = 9999f;


    public event EventHandler OnHurt;


    private int life = 4;
    private HealthBar healthBar;

    private SpriteRenderer spriteRenderer;
    // allows to jump few frames before to be grounded
    private float jumpPressedRemember = 0.0f;
    // allow jump few frame after leaving the floor
    private float groundedRemember = 0.0f;
    //private Animator animator;
    private Inventory inventory;
    private bool shoot;
    private bool unvisible = false;
    public float unvisibleTimer = 0.5f;

    public int Life {
        get => life;
        set => life = value;
    }

    // Use this for initialization
    void Awake ()
    {
        life = (int) GameManager.instance.CharacterStats.life.Value;
        Debug.Log("Life ==> " + life);
        shoot = false;
        spriteRenderer = GetComponent<SpriteRenderer> ();
        inventory = GetComponent<Inventory>();
        //animator = GetComponent<Animator> ();
    }

    protected new void Start()
    {
        base.Start();
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
                velocity.y = velocity.y * ( lowJumpMultiplier - 1 );
            }
            // in case of shooting
            shoot = false;
        }

        if (velocity.y < 0) {
            velocity.y = velocity.y * ( fallMultiplier -1);
        }

        if (CanJump() && IsGrounded())
        {
            jumpPressedRemember = 0.0f;
            groundedRemember = 0.0f;
            velocity.y = jumpTakeOffSpeed;
        }

        // limit the fall velocity
        if(velocity.y < -vYmax) {
            velocity.y = -vYmax;
        }

        else if (inventory.CanShoot() && shoot) {
            velocity.y = inventory.Shoot();
        }

        if(IsGrounded())
        {
            inventory.Reload();
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

    public void Hurt(EnemyBase enemy)
    {
        if(!unvisible) {
            life = life -1;
            OnHurt(this, EventArgs.Empty);
            if(life == 0)
            {
                LevelManager.instance.GameOver();
            }
            StartCoroutine(FlashSprite(GetComponent<SpriteRenderer>(), 0.0f, 1f, 0.1f, unvisibleTimer));
            StartCoroutine(GetUnvisible(unvisibleTimer, enemy));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(unvisible) {
            return;
        }
        EnemyBase enemy = collision.collider.GetComponent<EnemyBase>();
        if(!enemy) {
            // otherwise it must be floor
            LevelManager.instance.ResetCombo();
            return;
        }
        
        bool hasJumpedOnEnemy = false;
        bool hurtEnemyDuringJump = false;

        foreach(ContactPoint2D point in collision.contacts)
        {
            Debug.DrawLine(point.point, point.point + point.normal, Color.blue,10);
            // if fall into enemy
            if( point.normal.y >= 0.9f)
            {
              hasJumpedOnEnemy = true;
              
            }

            // contact with an enemy after the ascendant phase
            if(velocity.y > 0)
            {
                hurtEnemyDuringJump = true;
            }
        }


        if(hasJumpedOnEnemy)
        {
            velocity.y = jumpTakeOffSpeed * 0.5f;
            enemy.Hurt(inventory.GetDamage());
        }
        else if(hurtEnemyDuringJump)
        {
            enemy.Hurt(inventory.GetDamage());
        }
        else
        {
            Hurt(enemy);
        }
    }

    IEnumerator FlashSprite(SpriteRenderer renderer, float minAlpha, float maxAlpha, float interval, float duration)
    {
        Color colorNow = renderer.color;
        Color minColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, minAlpha);
        Color maxColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, maxAlpha);

        float currentInterval = 0;
        while(duration > 0)
        {
            float tColor = currentInterval / interval;
            renderer.color = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if(currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            duration -= Time.deltaTime;
            yield return null;
        }
        renderer.color = maxColor;
    }

    IEnumerator GetUnvisible(float unvisibleTimer, EnemyBase enemy)
    {
        unvisible = true;
        if(enemy)
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("enemy_hurt");
        }
        yield return new WaitForSeconds(unvisibleTimer);
        unvisible = false;
        if(enemy)
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("enemy");
        }
    }

}
