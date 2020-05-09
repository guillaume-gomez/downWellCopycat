using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundPlatform : EnemyBase
{
  public float speed;
  public Transform[] groundDetections;
  int layerMastk;

  private float distance = 1.0f;
  private bool movingRight = true;

  protected void Start()
  {
    // check raycast to everything except enemy layer (espacially itself)
    layerMastk = 1 << LayerMask.NameToLayer("enemy");
    layerMastk = ~layerMastk;
  }

  protected bool onPlatform()
  {
    
    for(int i = 0; i < groundDetections.Length; ++i)
    {
      RaycastHit2D groundInfo = Physics2D.Raycast(groundDetections[i].position, -transform.up, distance, layerMastk);
      //Debug.DrawLine(groundDetections[i].position, groundDetections[i].position - (transform.up * distance), Color.red,100);
      if(groundInfo.collider)
      {
        return true;
      }
    }
    return false;
  }

  protected void Update()
  {
    if(CannotMove())
    {
      return;
    }

    Vector2 direction = movingRight ? Vector2.right : - Vector2.right;
    transform.Translate(direction * speed * Time.deltaTime);
    if(!onPlatform())
    {
        //Debug.Break();
        transform.eulerAngles += new Vector3(0.0f, 0.0f, -90f);
    } else {
      // adjust position due to rotation overlap
      RaycastHit2D groundInfo = Physics2D.Raycast(groundDetections[0].position, -transform.up, distance, layerMastk);
      transform.position += - groundInfo.distance * transform.up;
    }
  }

}