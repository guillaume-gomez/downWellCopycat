using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoText : MonoBehaviour
{
    private Slider slider;
    public Inventory Inventory;
    public TextMeshProUGUI text;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int) GameManager.instance.CharacterStats.weaponAbilities.Value;
        Inventory.OnShootHandlerActiveWeapon += OnUpdateSlider;
    }

    private void OnUpdateSlider(object sender, WeaponEventArgs e)
    {
        slider.value = e.bullet;
        text.text = e.bullet.ToString();
    }
}