using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnPlatform : EnemyBase
{
  public float speed;
  public Transform groundDetection;

  private float distance = 1.0f;
  private bool movingRight = true;
  private bool collisionWithPlayer;

  protected new void Start()
  {
    collisionWithPlayer = false;
    isVisibleOnCamera = true;
    base.Start();
    distance = transform.localScale.y;
  }



  protected void Update()
  {
    if(CannotMove())
    {
      return;
    }

    if(!isVisibleOnCamera) {
      return;
    }

    if(!collisionWithPlayer)
    {
      transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
    if(!groundInfo.collider)
    {
        if(movingRight)
        {
            transform.eulerAngles = new Vector3(0.0f, -180f, 0.0f);
            movingRight = false;
        } else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            movingRight = true;
        }
    }
  }

  // theses 2 methods avoid colission glitch between a moving player and a moving enemy :)
  void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.collider.CompareTag("Player"))
    {
      collisionWithPlayer = true;
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if(collision.collider.CompareTag("Player"))
    {
      collisionWithPlayer = false;
    }
  }

}