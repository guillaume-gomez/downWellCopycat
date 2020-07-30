using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundPlatform : EnemyBase
{
  public float speed;
  public Transform[] groundDetections;
  private int layerMastk;
  protected Rigidbody2D rb2d;

  private float distance = 1.0f;
  private bool movingRight = true;

  protected void OnEnable()
  {
      rb2d = GetComponent<Rigidbody2D>();
  }

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
      if (groundInfo.collider && (groundInfo.collider.tag == "Bloc" || groundInfo.collider.tag == "BreakableBloc") )
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

    Vector3 direction = movingRight ? transform.right : - transform.right;
    rb2d.position = rb2d.position + (new Vector2(direction.x, direction.y) * speed * Time.deltaTime);
    if(!onPlatform())
    {
        //Debug.Break();
        transform.eulerAngles += new Vector3(0.0f, 0.0f, -90f);
        for(int i = 0; i < groundDetections.Length; ++i)
        {
          RaycastHit2D groundInfo = Physics2D.Raycast(groundDetections[i].position, -transform.up, distance, layerMastk);
          Debug.DrawLine(groundDetections[i].position, groundDetections[i].position - (transform.up * distance), Color.red,100);
          if (groundInfo.collider && (groundInfo.collider.tag == "Bloc" || groundInfo.collider.tag == "BreakableBloc") )
          {
            //return true;
            transform.position += -groundInfo.distance * transform.up;
            break;
          }
        }
        //Debug.DrawLine(transform.position, transform.position - (transform.up * distance), Color.red,100);
        //Debug.Break();
    } else {
      // adjust position due to rotation overlap
    }
  }

}