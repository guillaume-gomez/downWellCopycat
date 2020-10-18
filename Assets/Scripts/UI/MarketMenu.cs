using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MarketMenu : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI money;

  void Start()
  {
    if(GameManager.instance != null && GameManager.instance.LevelSystem != null)
    {
      score.text = "Score: " + GameManager.instance.LevelSystem.score.ToString("#.00");
      money.text = "Money: " + GameManager.instance.LevelSystem.money.ToString("#.00");
    }
  }

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
