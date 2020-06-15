using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    private const string DataFileName = "data.dat";
    private static string _fullPath;

    public static void Save(GameData gameData)
    {
        _fullPath = Path.Combine(Application.persistentDataPath, DataFileName);

        var file = File.Exists(_fullPath) ? File.OpenWrite(_fullPath) : File.Create(_fullPath);

        var bf = new BinaryFormatter();
        bf.Serialize(file, gameData);

        file.Close();
    }

    public static GameData Load()
    {
        _fullPath = Path.Combine(Application.persistentDataPath, DataFileName);

        if (!File.Exists(_fullPath))
        {
            Debug.LogWarning("File not found");
            return new GameData();
        }

        var file = File.OpenRead(_fullPath);

        var bf = new BinaryFormatter();
        var data = (GameData)bf.Deserialize(file);

        file.Close();

        return data;
    }
}