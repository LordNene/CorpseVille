
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveCemeteryData(TileData[,] tileDataMatrix, AtributesManagerScript atributes, AchievementData achievementData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/cemetery.data";
        FileStream stream = new FileStream(path,FileMode.Create);

        CemeteryData data = new CemeteryData(tileDataMatrix, atributes, achievementData);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static CemeteryData LoadCemeteryData()
    {
        string path = Application.persistentDataPath + "/cemetery.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =new FileStream (path, FileMode.Open);
            CemeteryData data = formatter.Deserialize(stream) as CemeteryData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }

    }

}
