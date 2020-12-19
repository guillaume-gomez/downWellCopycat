using UnityEngine;

public class OneLife : ItemBase
{
    public override void Equip()
    {
        GameManager.instance.LevelSystemRun.currentLife++;
    }
}
