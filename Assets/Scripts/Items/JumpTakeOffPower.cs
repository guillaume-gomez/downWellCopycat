using UnityEngine;

public class JumpTakeOffPower : ItemBase
{
    public override void Equip()
    {
      AddBonus(Bonus.JumpTakeOff);
      GameManager.instance.CharacterStats.jumpTakeOffSpeed.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public override void Unequip()
    {
        GameManager.instance.CharacterStats.jumpTakeOffSpeed.RemoveAllModifiersFromSource(this);
    }
}
