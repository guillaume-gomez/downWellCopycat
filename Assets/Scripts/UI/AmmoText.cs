using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoText : MonoBehaviour
{
    private Slider slider;
    public Weapon weapon;
    public TextMeshProUGUI text;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int) GameManager.instance.CharacterStats.weaponAbilities.Value;
        weapon.OnShootHandler += OnUpdateSlider;
    }

    private void OnUpdateSlider(object sender, WeaponEventArgs e)
    {
        slider.value = e.bullet;
        text.text = e.bullet.ToString();
    }
}