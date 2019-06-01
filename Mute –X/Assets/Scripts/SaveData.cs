using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData  {


    public static void Save(int score , bool zombieMode)
    {
        string path = Application.persistentDataPath + "/MuteSave.X";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path,FileMode.OpenOrCreate);
        DataToSave data;
        data = new DataToSave(zombieMode, score);

        formatter.Serialize(stream, data);
        stream.Close();
    }



    public static DataToSave Load()
    {
        string path = Application.persistentDataPath + "/MuteSave.X";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            DataToSave dataLoaded = formatter.Deserialize(stream) as DataToSave ;

            stream.Close();

            return dataLoaded;
        }
        else
         return null;
    }
}

[System.Serializable]
public class DataToSave{
    public bool ZombieMode;
    public int HighScore;

   
    public DataToSave(bool isUnlocked , int scoreAsHighScore)
    {
        ZombieMode = isUnlocked;
        HighScore = scoreAsHighScore;
    }

}