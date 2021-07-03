using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerData(int _gold, int _level, float[] _currentSkin, float[,] _mySkins){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.crown";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(_gold, _level, _currentSkin, _mySkins);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData(){
        string path = Application.persistentDataPath + "/player.crown";

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        } else{
            Debug.LogError("Save file not found in " + path);
            
            return null;
        }
    }
}
