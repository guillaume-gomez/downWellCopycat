using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public CharacterStat life;
    public CharacterStat weaponAbilities;
    public CharacterStat jumpTakeOffSpeed;
    public CharacterStat maxSpeed;

    public CharacterStats()
    {
        // default value of life
        life = new CharacterStat(4);
        weaponAbilities = new CharacterStat(12);
        jumpTakeOffSpeed = new CharacterStat(0.0f);
        maxSpeed = new CharacterStat(0.0f);
    }

    public void Reset()
    {

        life.RemoveAllNotifier();
        weaponAbilities.RemoveAllNotifier();
        jumpTakeOffSpeed.RemoveAllNotifier();
        maxSpeed.RemoveAllNotifier();
    }
}