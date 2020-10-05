using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    public GameObject spawner;
    public string[] foldersToInspect;

    void Start()
    {
      int i = 0;
      int y = 0;
      foreach(string folder in foldersToInspect)
      {
        Object[] blocs = Resources.LoadAll("" , typeof(GameObject));
        Debug.Log(blocs.Length);
        foreach(Object bloc in blocs)
        {
          Debug.Log(bloc.name);

        }
      }
    }
}
