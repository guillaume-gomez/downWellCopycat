using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundPlatform : EnemyBase
{
  public float speed;
  public float waitTime;
  public Transform[] spots;

  public float startWaitTime = 0.0f;
  private int indexSpot = 0;

  void Start()
  {
    waitTime = startWaitTime;
  }
  
  protected new void Update()
  {
    base.Update();
    
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

  public void SetSlotsFromPlatform(Vector3 size, Vector3 position)
  {
    spots[0].position = new Vector3(position.x - 1, position.y + 1, 0.0f);

    spots[1].position = new Vector3(position.x + size.x + 1, position.y + 1, 0.0f);

    spots[2].position = new Vector3(position.x + size.x + 1, position.y - 1, 0.0f);

    spots[3].position = new Vector3(position.x - 1, position.y - 1, 0.0f);
  }

}