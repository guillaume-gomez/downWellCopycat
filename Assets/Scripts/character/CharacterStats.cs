using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public CharacterStat life;
    public CharacterStat weaponAbilities;

    public CharacterStats()
    {
        // default value of life
        life = new CharacterStat(4);
        weaponAbilities = new CharacterStat(12);
    }

    public void Reset()
    {

        life.RemoveAllNotifier();
        weaponAbilities.RemoveAllNotifier();
    }
}