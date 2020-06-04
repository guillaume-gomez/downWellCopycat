using UnityEngine;
using TMPro;

public class InventoryUIGamedata : MonoBehaviour
{
    private TextMeshProUGUI text;
    public string gamedatakey;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if(GameManager.instance)
        {
            string value = GameManager.instance.GeneralStatistics.valueFromString(gamedatakey);
            text.text += ": " + value;
        }
    }
}