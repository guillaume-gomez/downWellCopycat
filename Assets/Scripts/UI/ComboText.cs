using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateCombo(0);
    }

    public void UpdateCombo(int currentCombo)
    {
        text.text = "Combo : " + currentCombo;
    }
}