using UnityEngine;

public class Protect : MonoBehaviour
{
    public void Equip()
    {
        GameManager.instance.CharacterStats.armor.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public void Unequip()
    {
        GameManager.instance.CharacterStats.armor.RemoveAllModifiersFromSource(this);
    }
}
