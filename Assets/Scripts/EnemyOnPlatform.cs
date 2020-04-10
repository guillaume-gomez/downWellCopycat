using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnPlatform : MonoBehaviour
{
  public float speed;
  public Transform groundDetection;

  private float distance = 1.0f;
  private bool movingRight = true;

  void Update()
  {
    transform.Translate(Vector2.right * speed * Time.deltaTime);

    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
    Debug.Log(groundInfo.collider);
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