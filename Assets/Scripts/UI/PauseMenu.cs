using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
   public static bool GameIsPaused = false;

   public GameObject pauseMenuUI;
   public GameObject firstSelectedButton;

   void Update()
   {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
   }

   public void Resume()
   {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
   }

   private void Pause()
   {
      firstSelectedButton.GetComponent<Selectable>().OnSelect(null);
      EventSystem.current.SetSelectedGameObject(firstSelectedButton);
      pauseMenuUI.SetActive(true);
      Time.timeScale = 0.0f;
      GameIsPaused = true;
   }

   public void Quit()
   {
      Application.Quit();
   }

   public void Menu()
   {
      Time.timeScale = 1.0f;
      SceneManager.LoadScene(0);
   }

}
