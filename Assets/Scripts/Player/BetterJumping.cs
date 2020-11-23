using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb2d;
    [SerializeField]
    [Range(0, 20)]
    public float fallMultiplier = 2.5f;
    [SerializeField]
    [Range(0, 20)]
    public float lowJumpMultiplier = 2f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
    }

    void Update()
    {

        if(rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

}
