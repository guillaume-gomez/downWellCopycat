using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  public float speed;
  public float waitTime;
  public Transform[] spots;
  public float startWaitTime = 0.0f;
  private int indexSpot;

  void Start()
  {
    indexSpot = 0;
  }
  
  protected void Update()
  {
    transform.position = Vector2.MoveTowards(transform.position, spots[indexSpot].position, speed * Time.deltaTime);
    if(Vector2.Distance(transform.position, spots[indexSpot].position) < 0.1f)
    {
      if(waitTime <= 0)
      {
        waitTime = startWaitTime;
      } else
      {
        waitTime = waitTime - Time.deltaTime;
      }

      indexSpot = indexSpot + 1;
      if(indexSpot >= spots.Length)
      {
        indexSpot = 0;
      }
    }
  }

  void OnDrawGizmos()
  {
      Gizmos.color = Color.yellow;
      for(int i = 0; i < spots.Length; i++)
      {
        Gizmos.DrawWireSphere((Vector2) spots[i].position, 0.5f);
        Transform origin = spots[i];
        Transform target = i + 1 < spots.Length ? spots[i + 1] : spots[0];
        Gizmos.DrawLine(origin.position, target.position);
      }
  }
}