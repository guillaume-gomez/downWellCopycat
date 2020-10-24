using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyableItem : MonoBehaviour
{
    private Button button;
    private Image image;
    public float price;
    public TextMeshProUGUI score;

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        score.text = price.ToString();


        MarketManager.instance.OnMoneyChange += OnMoneyChange;
        CanBeBought(MarketManager.instance.Money);
    }

    private void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
    {
        CanBeBought(e.money);
    }

    private void CanBeBought(float totalMoney)
    {
        if(totalMoney >= price)
        {
            button.interactable = true;
            image.color = Color.white;
        }
        else
        {
            button.interactable = false;
            image.color = Color.red;
        }
    }

    public void Buy()
    {
        MarketManager.instance.Buy(price);
    }

}