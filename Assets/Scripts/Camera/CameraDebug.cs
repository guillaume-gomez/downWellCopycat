using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraDebug : MonoBehaviour
{
  public float speed = 10;
  private float originalOrthographicSize;
  private float zoomFactor;

  void Start()
  {

    originalOrthographicSize = GetComponent<Camera>().orthographicSize;
    zoomFactor = 1.0f;
  }

  void Update ()
  {
    float inputX = Input.GetAxis ("Horizontal");
    float inputY = Input.GetAxis ("Vertical");

    if(Input.GetKey(KeyCode.E))
    {
      GetComponent<Camera>().orthographicSize += 0.1f;
      zoomFactor = Math.Abs(1.0f + GetComponent<Camera>().orthographicSize / originalOrthographicSize);
    }

    if(Input.GetKey(KeyCode.Z))
    {
      GetComponent<Camera>().orthographicSize -= 0.1f;
      zoomFactor = Math.Abs(1.0f - GetComponent<Camera>().orthographicSize / originalOrthographicSize);
    }

    if(Input.GetKey(KeyCode.R))
    {
      GetComponent<Camera>().orthographicSize = originalOrthographicSize;
      zoomFactor = 1.0f;
    }

    move (inputX, inputY);
  }


  void move(float inputX, float inputY)
  {
    transform.position += new Vector3(inputX, inputY, 0.0f) * speed * zoomFactor * Time.deltaTime;
  }

}
