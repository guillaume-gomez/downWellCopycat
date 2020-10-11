using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveCharacterStat()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/characterStat.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, GameManager.instance.CharacterStats.SavePersistantCharacterStats());
        stream.Close();
    }

    public static CharacterStats LoadCharacterStat()
    {
        string path = Application.persistentDataPath + "/characterStat.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CharacterStats data = formatter.Deserialize(stream) as CharacterStats;
            stream.Close();

            return data;
        } else
        {
            Debug.LogWarning("Save file not found in " + path);
            return new CharacterStats();
        }
    }

    public static void SaveLevelSystem()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levelSystem.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, GameManager.instance.LevelSystem);
        stream.Close();
    }

    public static LevelSystem LoadLevelSystem()
    {
        string path = Application.persistentDataPath + "/levelSystem.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelSystem data = formatter.Deserialize(stream) as LevelSystem;
            stream.Close();

            return data;
        } else
        {
            Debug.LogWarning("Save file not found in " + path);
            return new LevelSystem();
        }
    }
}