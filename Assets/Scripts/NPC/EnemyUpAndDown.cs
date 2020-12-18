using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpAndDown : EnemyBase
{
  public float speed = 1.0f;
  public Transform blocDetection;
  private bool movingUp = true;


  protected void Update()
  {
    if(CannotMove())
    {
      return;
    }

    if(!isVisibleOnCamera) {
      return;
    }

    transform.Translate(Vector2.up * speed * Time.deltaTime);
    
    RaycastHit2D groundInfo = Physics2D.Raycast(blocDetection.position, Vector2.up, 0.1f);
    if(groundInfo.collider && groundInfo.collider.tag != "Player")
    {
      Rotate();
    }
  }

  private void Rotate()
  {
    if(movingUp)
    {
        transform.eulerAngles = new Vector3(-180f, 0.0f, 0.0f);
        movingUp = false;
    } else
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        movingUp = true;
    }
  }

}