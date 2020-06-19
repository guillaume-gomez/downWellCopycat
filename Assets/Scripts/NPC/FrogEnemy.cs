using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : EnemyBase
{
    public float maxDistanceVision = 50;
    public float jumpVelocity = 30;
    public Transform target;
    public float jumpRememberTime = 2.0f;

    private bool isJumping;
    private Rigidbody2D rb2d;
    private float jumpRemember;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isJumping = false;
        jumpRemember = 0.0f;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) >= maxDistanceVision) {
            return;
        }

        jumpRemember = jumpRemember + Time.deltaTime;

        Vector2 direction = (target.position - transform.position);
        if(!isJumping && jumpRemember >= jumpRememberTime)
        {
            jumpRemember = 0.0f;
            rb2d.velocity = new Vector2 (direction.x, jumpVelocity);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bloc" || collision.gameObject.tag == "BreakableBloc") {
            isJumping = false;
        }
    }
}