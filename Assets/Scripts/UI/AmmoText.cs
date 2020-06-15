using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    private Slider slider;
    public Weapon weapon;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int) GameManager.instance.CharacterStats.weaponAbilities.Value;
        weapon.OnShootHandler += OnUpdateSlider;
    }

    private void OnUpdateSlider(object sender, WeaponEventArgs e)
    {
        slider.value = e.bullet;
    }
}