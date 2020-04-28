using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnPlatform : EnemyBase
{
  public float speed;
  public Transform groundDetection;

  private float distance = 1.0f;
  private bool movingRight = true;

  protected new void Update()
  {
    base.Update();

    transform.Translate(Vector2.right * speed * Time.deltaTime);

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

}