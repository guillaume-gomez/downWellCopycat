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

   // theses 2 methods avoid colission glitch between a moving player and a moving enemy :)
  void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.collider.tag == "Player")
    {
      collision.collider.transform.SetParent(transform);
      collision.collider.GetComponent<Movement>().OnJump += OnCharacterJump;
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if(collision.collider.tag == "Player")
    {
      collision.collider.transform.SetParent(null);
      collision.collider.GetComponent<Movement>().OnJump -= OnCharacterJump;
    }
  }

  void OnCharacterJump(object sender, System.EventArgs e)
  {
      Debug.Log("fjd");
      Movement gg  = (Movement) sender;
      gg.transform.SetParent(null);
      gg.OnJump -= OnCharacterJump;
  }


}