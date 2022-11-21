
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{

    //EXP
    public static void SaveEXPData(SistemaXP EXP)
    {
        SaveData saveData = new SaveData(EXP);
        string dataPath = Application.persistentDataPath + "/EXPData.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formateadorBynario = new BinaryFormatter();
        formateadorBynario.Serialize(fileStream, saveData);
        fileStream.Close();


    }

    public static SaveData LoadEXPData()
    {
        string dataPath = Application.persistentDataPath + "/EXPData.save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formateadorBynario = new BinaryFormatter();

            SaveData expData = (SaveData)formateadorBynario.Deserialize(fileStream);
            fileStream.Close();
            return expData;

        }
        else
        {
            return null;
        }

    }



    //Torretas
    public static void SaveDataTorreta(SistemadeSpawnTorretas torretasSave)
    {
        SaveDataTorretas saveDataTorretas = new SaveDataTorretas(torretasSave);
        string dataPath = Application.persistentDataPath + "/TorretasData.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formateadorBynario = new BinaryFormatter();
        formateadorBynario.Serialize(fileStream, saveDataTorretas);
        fileStream.Close();


    }

    public static SaveDataTorretas LoadTorretasData()
    {
        string dataPath = Application.persistentDataPath + "/TorretasData.save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formateadorBynario = new BinaryFormatter();

            SaveDataTorretas torretasData = (SaveDataTorretas)formateadorBynario.Deserialize(fileStream);
            fileStream.Close();
            return torretasData;

        }
        else
        {
            return null;
        }


        

    }
    //TorretasDamage

    public static void SaveDamageTorreta(Torreta2 torretasDamage)
    {
        SaveDamage saveDamage = new SaveDamage(torretasDamage);
        string dataPath = Application.persistentDataPath + "/TorretasDamage.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formateadorBynario = new BinaryFormatter();
        formateadorBynario.Serialize(fileStream, saveDamage);
        fileStream.Close();


    }

    public static SaveDamage LoadTorretasDamageData()
    {
        string dataPath = Application.persistentDataPath + "/TorretasDamage.save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formateadorBynario = new BinaryFormatter();

            SaveDamage torretasDamage = (SaveDamage)formateadorBynario.Deserialize(fileStream);
            fileStream.Close();
            return torretasDamage;

        }
        else
        {
            return null;
        }




    }
    //Vida
    public static void SaveVida(PlayerVida vidaPlayer)
    {
        SaveVida saveData = new SaveVida(vidaPlayer);
        string dataPath = Application.persistentDataPath + "/SaveVida.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formateadorBynario = new BinaryFormatter();
        formateadorBynario.Serialize(fileStream, saveData);
        fileStream.Close();


    }

    public static SaveVida LoadVida()
    {
        string dataPath = Application.persistentDataPath + "/SaveVida.save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formateadorBynario = new BinaryFormatter();

            SaveVida vidaPlayer = (SaveVida)formateadorBynario.Deserialize(fileStream);
            fileStream.Close();
            return vidaPlayer;

        }
        else
        {
            return null;
        }

    }
    //DañoArmasPlayer
    public static void SaveWeaponsStats(Player player)
    {
        SaveWeaponsPlayer saveData = new SaveWeaponsPlayer(player);
        string dataPath = Application.persistentDataPath + "/SaveStatsWeapon.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formateadorBynario = new BinaryFormatter();
        formateadorBynario.Serialize(fileStream, saveData);
        fileStream.Close();


    }

    public static SaveWeaponsPlayer LoadWeaponsStats()
    {
        string dataPath = Application.persistentDataPath + "/SaveStatsWeapon.save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formateadorBynario = new BinaryFormatter();

            SaveWeaponsPlayer player = (SaveWeaponsPlayer)formateadorBynario.Deserialize(fileStream);
            fileStream.Close();
            return player;

        }
        else
        {
            return null;
        }

    }

}
