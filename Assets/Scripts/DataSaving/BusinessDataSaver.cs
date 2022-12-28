using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BusinessDataSaver
{
    private const string FILE_NAME = "/BusinessData";
    private const string FILE_NAME_PROGRESS = "/BusinessProgress";

    public static void SaveBusiness(BusinessModel model)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_NAME + model.Number + ".dat");
        BusinessSaveData data = new BusinessSaveData();
        data.Level = model.Level;
        data.IsBoughtUpgrade1 = model.Upgrade1.IsBought;
        data.IsBoughtUpgrade2 = model.Upgrade2.IsBought;
        bf.Serialize(file, data);
        file.Close();
    }

    public static bool LoadBusinessData(BusinessNumber number, out int level, out bool isBoughtUpgrade1, out bool isBoughtUpgrade2)
    {
        if (File.Exists(Application.persistentDataPath + FILE_NAME + number + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_NAME + number + ".dat", FileMode.Open);
            BusinessSaveData data = (BusinessSaveData)bf.Deserialize(file);
            file.Close();
            level = data.Level;
            isBoughtUpgrade1 = data.IsBoughtUpgrade1;
            isBoughtUpgrade2 = data.IsBoughtUpgrade2;
            return true;
        }

        level = 0;
        isBoughtUpgrade1 = false;
        isBoughtUpgrade2 = false;
        return false;
    }

    public static void SaveProgress(float value, BusinessModel model)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_NAME_PROGRESS + model.Number + ".dat");
        BusinessSaveProgress data = new BusinessSaveProgress();
        data.Progress = value;
        bf.Serialize(file, data);
        file.Close();
    }
    
    public static float LoadProgress(BusinessModel model)
    {
        if (File.Exists(Application.persistentDataPath + FILE_NAME_PROGRESS + model.Number + ".dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_NAME_PROGRESS + model.Number + ".dat", FileMode.Open);
            BusinessSaveProgress data = (BusinessSaveProgress)bf.Deserialize(file);
            file.Close(); 
            return data.Progress;
        }

        return 0;    
    }
}

[Serializable]
public class BusinessSaveData
{
    public int Level;
    public bool IsBoughtUpgrade1;
    public bool IsBoughtUpgrade2;
}

[Serializable]
public class BusinessSaveProgress
{
    public float Progress;
}