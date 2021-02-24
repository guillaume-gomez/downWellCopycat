using UnityEngine;

public enum Bonus
{
    ExtraAmmo,
    ExtraLife,
    JumpTakeOff,
    MaxSpeed,
    Protect
}

public class ItemBase : MonoBehaviour
{
    public void AddBonus(Bonus bonus)
    {
      GameManager.instance.LevelSystemRun.AddBonus(bonus);
    }

    public virtual void Equip()
    {
      GameManager.instance.CharacterStats.armor.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public virtual void Unequip()
    {
      GameManager.instance.CharacterStats.armor.RemoveAllModifiersFromSource(this);
    }
}
