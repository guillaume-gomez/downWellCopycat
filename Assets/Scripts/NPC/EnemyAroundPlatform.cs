using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundPlatform : EnemyBase
{
  public float speed;
  public Transform[] groundDetections;
  public Transform horizontalDetection;
  private int layerMastk;
  protected Rigidbody2D rb2d;

  private float distance = 1.0f;
  private bool movingRight = true;
  // if the enemy is locked and turn around himself
  // no detection is more than  4 rotations (each corner)
  private int noDectection = 0;
  private int layerBloc;
  private bool collisionWithPlayer;

  protected void OnEnable()
  {
      rb2d = GetComponent<Rigidbody2D>();
      layerBloc = LayerMask.NameToLayer("bloc");
  }

  protected void Start()
  {
    collisionWithPlayer = false;
    // check raycast to everything except" enemy layer (espacially itself)
    layerMastk = 1 << LayerMask.NameToLayer("enemy");
    layerMastk = ~layerMastk;
  }

  protected bool onPlatform()
  {
    for(int i = 0; i < groundDetections.Length; ++i)
    {
      RaycastHit2D groundInfo = Physics2D.Raycast(groundDetections[i].position, -transform.up, distance, layerMastk);
      //Debug.DrawLine(groundDetections[i].position, groundDetections[i].position - (transform.up * distance), Color.red,100);
      if (groundInfo.collider && groundInfo.collider.gameObject.layer == layerBloc )
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

    if(!isVisibleOnCamera)
    {
      return;
    }

    RaycastHit2D groundInfof = Physics2D.Raycast(horizontalDetection.position, Vector2.right, 0.05f);
    if(groundInfof.collider && groundInfof.collider.gameObject.layer == layerBloc)
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
        return;
    }

    // move
    if(!collisionWithPlayer)
    {
      rb2d.position = rb2d.position + (Vector2) transform.right * speed * Time.deltaTime;
    }

    if(!onPlatform())
    {
        noDectection++;
        // if too many rotation, leave the platform
        if(noDectection < 4) {
          transform.eulerAngles += new Vector3(0.0f, 0.0f, -90f);
          // ajust position to collide with the platform
          for(int i = 0; i < groundDetections.Length; ++i)
          {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetections[i].position, -transform.up, distance, layerMastk);
            Debug.DrawLine(groundDetections[i].position, groundDetections[i].position - (transform.up * distance), Color.red, 5);
            if (groundInfo.collider && groundInfo.collider.gameObject.layer == layerBloc )
            {
              transform.position += -groundInfo.distance * transform.up;
              break;
            }
          }
        }
    } else {
      noDectection = 0;
    }
  }

  // theses 2 methods avoid colission glitch between a moving player and a moving enemy :)
  void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.collider.tag == "Player")
    {
      collisionWithPlayer = true;
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if(collision.collider.tag == "Player")
    {
      collisionWithPlayer = false;
    }
  }

}