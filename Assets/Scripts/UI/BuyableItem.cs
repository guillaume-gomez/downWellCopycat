using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItem : MonoBehaviour
{
    private Button button;
    public float price;

    void Start()
    {
        price = 10.0f;
        button = GetComponent<Button>();

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
        }
        else
        {
            button.interactable = false;
        }
    }

}