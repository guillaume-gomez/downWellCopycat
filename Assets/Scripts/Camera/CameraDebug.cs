using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraDebug : MonoBehaviour
{
  public float speed = 10;
  private Camera camera;
  private float originalOrthographicSize;
  private float zoomFactor;

  void Start()
  {
    camera = GetComponent<Camera>();
    originalOrthographicSize = camera.orthographicSize;
    zoomFactor = 1.0f;
  }

  void Update ()
  {
    float inputX = Input.GetAxis ("Horizontal");
    float inputY = Input.GetAxis ("Vertical");

    if(Input.GetKey(KeyCode.E))
    {
      camera.orthographicSize += 0.1f;
      zoomFactor = Math.Abs(1.0f + camera.orthographicSize / originalOrthographicSize);
    }

    if(Input.GetKey(KeyCode.Z))
    {
      camera.orthographicSize -= 0.1f;
      zoomFactor = Math.Abs(1.0f - camera.orthographicSize / originalOrthographicSize);
    }

    if(Input.GetKey(KeyCode.R))
    {
      camera.orthographicSize = originalOrthographicSize;
      zoomFactor = 1.0f;
    }

    move (inputX, inputY);
  }


  void move(float inputX, float inputY)
  {
    transform.position += new Vector3(inputX, inputY, 0.0f) * speed * zoomFactor * Time.deltaTime;
  }

}
