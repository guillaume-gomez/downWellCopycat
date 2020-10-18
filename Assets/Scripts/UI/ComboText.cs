using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        LevelManager.instance.OnUpdateCombo += OnUpdateCombo;
        UpdateCombo(GameManager.instance.LevelSystemRun.currentCombo);
    }

    private void OnUpdateCombo(object sender, OnComboChangedEventArgs e)
    {
        UpdateCombo(e.combo);
    }

    public void UpdateCombo(int currentCombo)
    {
        text.text = "Combo : " + currentCombo;
    }
}