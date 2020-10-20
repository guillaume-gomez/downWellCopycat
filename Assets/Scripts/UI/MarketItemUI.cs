using UnityEngine;
using UnityEngine.UI;

public class MarketItemUI : MonoBehaviour
{
    private Button button;
    public float price;
    public float level;

    void Start()
    {
        button = GetComponent<Button>();
        // TODO check manager
        // TODO add level concept
        //if(price > Manager.instance.money || level > Manager.instance.CharacterStats.level)
        //{
        //  button.interactable = false;
        //}
    }
}