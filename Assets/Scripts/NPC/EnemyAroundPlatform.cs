using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundPlatform : EnemyBase
{
  public float speed;
  public float waitTime;
  public Transform[] spots;
  public GameObject enemyObject;

  public float startWaitTime = 0.0f;
  private int indexSpot = 0;

  void Start()
  {
    waitTime = startWaitTime;
    SetSlotsFromPlatform();
  }
  
  protected void Update()
  {
    if(CannotMove())
    {
      return;
    }
    
    enemyObject.transform.position = Vector2.MoveTowards(enemyObject.transform.position, spots[indexSpot].position, speed * Time.deltaTime);
    if(Vector2.Distance(enemyObject.transform.position, spots[indexSpot].position) < 0.1f)
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

  public void SetSlotsFromPlatform()
  {
    float offset = 1f;
    spots[0].position = new Vector3(slotPosition.x - (slotSize.x/2) - offset, slotPosition.y + (slotSize.y/2) + offset, 0.0f);

    spots[1].position = new Vector3(slotPosition.x + (slotSize.x/2) + offset, slotPosition.y + (slotSize.y/2) + offset, 0.0f);

    spots[2].position = new Vector3(slotPosition.x + (slotSize.x/2) + offset, slotPosition.y - (slotSize.y/2) - offset, 0.0f);

    spots[3].position = new Vector3(slotPosition.x - (slotSize.x/2) - offset, slotPosition.y - (slotSize.y/2) - offset, 0.0f);
  }

}