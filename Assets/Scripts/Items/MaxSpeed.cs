using UnityEngine;

public class MaxSpeed : MonoBehaviour
{
    public void Equip()
    {
        GameManager.instance.CharacterStats.maxSpeed.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public void Unequip()
    {
        GameManager.instance.CharacterStats.maxSpeed.RemoveAllModifiersFromSource(this);
    }
}
