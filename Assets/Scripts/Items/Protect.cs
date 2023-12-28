using UnityEngine;

public class Protect : ItemBase
{
    public override void Equip()
    {
      AddBonus(Bonus.Protect);
      GameManager.instance.CharacterStats.armor.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public override void Unequip()
    {
      GameManager.instance.CharacterStats.armor.RemoveAllModifiersFromSource(this);
    }
}
