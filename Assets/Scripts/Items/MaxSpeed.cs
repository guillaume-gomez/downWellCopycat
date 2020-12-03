using UnityEngine;

public class MaxSpeed : ItemBase
{
    public override void Equip()
    {
        GameManager.instance.CharacterStats.maxSpeed.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public override void Unequip()
    {
        GameManager.instance.CharacterStats.maxSpeed.RemoveAllModifiersFromSource(this);
    }
}
