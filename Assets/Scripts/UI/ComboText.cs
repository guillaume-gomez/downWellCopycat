using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        LevelManager.instance.OnUpdateCombo += OnUpdateCombo;
        UpdateCombo(GameManager.instance.LevelSystem.currentCombo);
    }

    private void OnUpdateCombo(object sender, System.EventArgs e)
    {
        UpdateCombo(GameManager.instance.LevelSystem.currentCombo);
    }

    public void UpdateCombo(int currentCombo)
    {
        text.text = "Combo : " + currentCombo;
    }
}