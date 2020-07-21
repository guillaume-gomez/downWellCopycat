using UnityEngine;

public class CoinPerf : MonoBehaviour
{
    public void Equip()
    {
        GameManager.instance.CharacterStats.coinPerf.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public void Unequip()
    {
        GameManager.instance.CharacterStats.coinPerf.RemoveAllModifiersFromSource(this);
    }
}
