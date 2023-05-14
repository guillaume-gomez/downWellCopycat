using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
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
      Movement playerMovement  = (Movement) sender;
      playerMovement.transform.SetParent(null);
      playerMovement.OnJump -= OnCharacterJump;
  }

}