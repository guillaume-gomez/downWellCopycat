using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathType {
Triangle,
Square,
}

public class SpawnMovingPlatform : MonoBehaviour
{
  public PathType pathType;
  public GameObject prefabMovingPlatform;
  public Vector2 size;

  void Start()
  {
    GameObject instance = (GameObject) Instantiate(prefabMovingPlatform, transform.position, transform.rotation);
    instance.transform.parent = transform;
    switch(pathType)
    {
      case PathType.Triangle:
        TrianglePath(instance);
      break;
      case PathType.Square:
        SquarePath(instance);
      break;
    }
  }

  void TrianglePath(GameObject instance)
  {
    GameObject platformPivot = instance.transform.Find("PlatformPivot").gameObject;
    MovingPlatform script = platformPivot.GetComponent<MovingPlatform>();

    float middleX = size.x / 2.0f;
    float middleY = size.y / 2.0f;

    Transform dots = instance.transform.Find("Dots");
    GameObject dot1 = new GameObject("dot1");
    dot1.transform.position = new Vector3(-middleX, -middleY, 0.0f);
    dot1.transform.parent = dots.transform;
    GameObject dot2 = new GameObject("dot2");
    dot2.transform.position = new Vector3(middleX, -middleY, 0.0f);
    dot2.transform.parent = dots.transform;
    GameObject dot3 = new GameObject("dot3");
    dot3.transform.position = new Vector3(0.0f, middleY, 0.0f);
    dot3.transform.parent = dots.transform;

    List<GameObject> spots = new List<GameObject>();
    spots.Add(dot1);
    spots.Add(dot2);
    spots.Add(dot3);
    script.spots = spots.ToArray();

    platformPivot.transform.position = dot1.transform.position;
  }

  void SquarePath(GameObject instance)
  {
    GameObject platformPivot = instance.transform.Find("PlatformPivot").gameObject;
    MovingPlatform script = platformPivot.GetComponent<MovingPlatform>();

    float middleX = size.x / 2.0f;
    float middleY = size.y / 2.0f;

    Transform dots = instance.transform.Find("Dots");
    GameObject dot1 = new GameObject("dot1");
    dot1.transform.position = new Vector3(-middleX, -middleY, 0.0f);
    dot1.transform.parent = dots.transform;
    GameObject dot2 = new GameObject("dot2");
    dot2.transform.position = new Vector3(middleX, -middleY, 0.0f);
    dot2.transform.parent = dots.transform;
    GameObject dot3 = new GameObject("dot3");
    dot3.transform.position = new Vector3(middleX, middleY, 0.0f);
    dot3.transform.parent = dots.transform;
    GameObject dot4 = new GameObject("dot3");
    dot4.transform.position = new Vector3(-middleX, middleY, 0.0f);
    dot4.transform.parent = dots.transform;

    List<GameObject> spots = new List<GameObject>();
    spots.Add(dot1);
    spots.Add(dot2);
    spots.Add(dot3);
    spots.Add(dot4);
    script.spots = spots.ToArray();

    platformPivot.transform.position = dot1.transform.position;
  }
  
}