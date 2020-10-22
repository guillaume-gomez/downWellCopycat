using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        LevelManager.instance.OnMoneyChange += OnMoneyChange;
        UpdateMoney(GameManager.instance.LevelSystemRun.money);
    }

    private void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
    {
        UpdateMoney(e.money);
    }

    public void UpdateMoney(float money)
    {
        text.text = "Money : " + money;
    }
}