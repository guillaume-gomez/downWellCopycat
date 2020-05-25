using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public void Equip()
    {
        GameManager.instance.CharacterStats.life.AddModifier(new StatModifier(1, StatModType.Flat, this));
    }
 
    public void Unequip()
    {
        GameManager.instance.CharacterStats.life.RemoveAllModifiersFromSource(this);
    }
}
