using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTutorial : MonoBehaviour
{

    void Start()
    {
        if(GameManager.instance.LevelSystemRun.level == 1)
        {
          gameObject.SetActive(true);
        }
        else
        {
          gameObject.SetActive(false);
        }
    }

}
