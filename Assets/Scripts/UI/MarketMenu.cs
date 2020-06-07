using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketMenu : MonoBehaviour
{
   void Update()
   {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackMainMenu();
        }
   }

   public void Save()
   {
    //TODO
   }

   public void GoBack()
   {
      GoBackMainMenu();
   }

   private void GoBackMainMenu()
   {
      SceneManager.LoadScene(0);
   }

}
