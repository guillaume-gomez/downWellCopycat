using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public CharacterStat life;
    public CharacterStat weaponAbilities;
    public CharacterStat jumpTakeOffSpeed;
    public CharacterStat maxSpeed;
    public CharacterStat armor;
    public CharacterStat coinPerf;

    public CharacterStats()
    {
        // default value of life
        life = new CharacterStat(4);
        weaponAbilities = new CharacterStat(12);
        jumpTakeOffSpeed = new CharacterStat(0.0f);
        maxSpeed = new CharacterStat(0.0f);
        armor = new CharacterStat(0.0f);
        coinPerf = new CharacterStat(1.0f);
    }

    public void Reset()
    {
        life.RemoveAllNotifier();
        weaponAbilities.RemoveAllNotifier();
        jumpTakeOffSpeed.RemoveAllNotifier();
        maxSpeed.RemoveAllNotifier();
        armor.RemoveAllNotifier();
        coinPerf.RemoveAllNotifier();
    }

    public CharacterStats SavePersistantCharacterStats()
    {
        CharacterStats persistantCharacterStats = new CharacterStats();
        persistantCharacterStats.weaponAbilities = weaponAbilities.GetPersistantModifiers();
        persistantCharacterStats.jumpTakeOffSpeed = jumpTakeOffSpeed.GetPersistantModifiers();
        persistantCharacterStats.maxSpeed = maxSpeed.GetPersistantModifiers();
        persistantCharacterStats.armor = armor.GetPersistantModifiers();
        persistantCharacterStats.coinPerf = coinPerf.GetPersistantModifiers();

        return persistantCharacterStats;
    }
}