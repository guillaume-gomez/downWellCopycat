using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : EnemyBase
{
    public float maxDistanceVision = 50;
    public float jumpVelocity = 30;
    public Transform target;

    private bool isJumping;
    Rigidbody2D rb2d;
    

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isJumping = false;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) >= maxDistanceVision) {
            return;
        }

        Vector2 norm = (target.position - transform.position).normalized;
        if(!isJumping)
        {
            rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpVelocity);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bloc" || collision.gameObject.tag == "BreakableBloc") {
            isJumping = false;
            Debug.Log("penus");
        }
    }
}