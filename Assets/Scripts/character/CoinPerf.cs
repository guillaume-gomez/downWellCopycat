using UnityEngine;

public class CoinPerf : ItemBase
{
    public override void Equip()
    {
        GameManager.instance.CharacterStats.coinPerf.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public override void Unequip()
    {
        GameManager.instance.CharacterStats.coinPerf.RemoveAllModifiersFromSource(this);
    }
}
