using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        LevelManager.instance.OnUpdateCombo += OnUpdateCombo;
        UpdateCombo(LevelManager.instance.CurrentCombo);
    }

    private void OnUpdateCombo(object sender, System.EventArgs e)
    {
        UpdateCombo(LevelManager.instance.CurrentCombo);
    }

    public void UpdateCombo(int currentCombo)
    {
        text.text = "Combo : " + currentCombo;
    }
}