using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundPlatform : EnemyBase
{
  public float speed;
  public float waitTime;
  public Transform[] spots;

  private float startWaitTime;
  private int indexSpot = 0;

  void Start()
  {
    waitTime = startWaitTime;
  }
  
  void Update()
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

}