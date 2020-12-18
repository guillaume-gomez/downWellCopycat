using UnityEngine;

public class ExtraAmmo : ItemBase
{
    public override void Equip()
    {
        GameManager.instance.CharacterStats.weaponAbilities.AddModifier(new StatModifier(1, StatModType.Flat, this));
    }

    public override void Unequip()
    {
        GameManager.instance.CharacterStats.weaponAbilities.RemoveAllModifiersFromSource(this);
    }
}
