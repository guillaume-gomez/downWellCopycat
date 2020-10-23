using System;
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
    MarketManager.instance.OnMoneyChange += OnMoneyChange;
    score.text = "Score: " + MarketManager.instance.Score.ToString("F2");
    money.text = "Money: " + MarketManager.instance.Money.ToString("F2");
  }

   void Update()
   {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MarketManager.instance.Save();
            GoBackMainMenu();
        }
   }

   private void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
   {
      money.text = "Money: " + e.money.ToString("F2");
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
