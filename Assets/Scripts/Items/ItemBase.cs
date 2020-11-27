using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public virtual void Equip()
    {
        GameManager.instance.CharacterStats.armor.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public virtual void Unequip()
    {
        GameManager.instance.CharacterStats.armor.RemoveAllModifiersFromSource(this);
    }
}
