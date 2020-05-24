using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, GameManager.instance.GameData);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/game.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return new GameData(-1,-1, -1);
        }
    }

    public static void SaveCharacterStat()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/characterStat.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, GameManager.instance.CharacterStats);
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
            Debug.LogError("Save file not found in " + path);
            return new CharacterStats();
        }
    }
}