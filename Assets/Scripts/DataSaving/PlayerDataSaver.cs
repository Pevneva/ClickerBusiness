using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PlayerDataSaver
{
    private const string FILE_NAME = "/PlayerData.dat";
    
    public static void SaveBalance(float balance)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_NAME);
        PlayerSaveData data = new PlayerSaveData();
        data.Balance = balance;
        bf.Serialize(file, data);
        file.Close();
    }

    public static float LoadBalance()
    {
        if (File.Exists(Application.persistentDataPath + FILE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Open);
            PlayerSaveData data = (PlayerSaveData)bf.Deserialize(file);
            file.Close();
            return data.Balance;
        }
        return 0;
    }
}

[Serializable]
public class PlayerSaveData
{
    public float Balance;
}