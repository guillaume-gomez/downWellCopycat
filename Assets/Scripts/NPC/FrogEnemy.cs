using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : EnemyBase
{
    public float maxDistanceVision = 50;
    public float jumpVelocity = 30;
    public float jumpRememberTime = 2.0f;

    private bool isJumping;
    private Rigidbody2D rb2d;
    private float jumpRemember;
    private float shakeAmount = 0.1f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected new void Start()
    {
        base.Start();
        isJumping = false;
        jumpRemember = 0.0f;
    }

    void Update()
    {
        if(CannotMove())
        {
            return;
        }

        if(!isVisibleOnCamera)
        {
            return;
        }

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
        // shaking
        else if(!isJumping) {
            Debug.Log("shakke");
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x - Random.Range(-shakeAmount , shakeAmount),
                gameObject.transform.position.y,
                0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bloc" || collision.gameObject.tag == "BreakableBloc") {
            isJumping = false;
        }
    }
}