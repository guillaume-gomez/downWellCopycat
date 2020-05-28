using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPanel : MonoBehaviour
{

   private float timerOpen = 2.0f;
   private bool isOpen = false;
   public GameObject panelUI;

   void Update()
   {
        if(LevelManager.instance.EndGamePanel && !isOpen) {
          Open();
          Invoke("Done", timerOpen);
        }
   }

   private void Open()
   {
      panelUI.SetActive(true);
   }

  private void Done()
  {
    isOpen = false;
    panelUI.SetActive(false);
  }


}
