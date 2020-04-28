using UnityEngine;
using UnityEngine.UI;

public class InventoryUIGamedata : MonoBehaviour
{
    private Text text;
    public string gamedatakey;

    void Start()
    {
        text = GetComponent<Text>();

        if(GameManager.instance)
        {
            string value = GameManager.instance.Gamedata.valueFromString(gamedatakey);
            text.text += ": " + value;
        }
    }
}