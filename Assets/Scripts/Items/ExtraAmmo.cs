using UnityEngine;

public class ExtraAmmo : MonoBehaviour
{
    public void Equip()
    {
        GameManager.instance.CharacterStats.weaponAbilities.AddModifier(new StatModifier(1, StatModType.Flat, this));
    }
 
    public void Unequip()
    {
        GameManager.instance.CharacterStats.weaponAbilities.RemoveAllModifiersFromSource(this);
    }
}
