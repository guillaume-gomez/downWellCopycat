using UnityEngine;

public class JumpTakeOffPower : MonoBehaviour
{
    public void Equip()
    {
        GameManager.instance.CharacterStats.jumpTakeOffSpeed.AddModifier(new StatModifier(0.1f, StatModType.Flat, this));
    }

    public void Unequip()
    {
        GameManager.instance.CharacterStats.jumpTakeOffSpeed.RemoveAllModifiersFromSource(this);
    }
}
