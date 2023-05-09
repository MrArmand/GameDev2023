using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveGame
{
    private static int score;
    private static int profileID;
    private static int levelID;

    public static int LevelID
    {
        get { return levelID; }
        set { levelID = value; }  
    }
    public static int Score
    {
        get { return score; }
        set { score = value; }
    }
    public static int ProfileID
    {
        get { return profileID; }
        set { profileID = value; }
    }
    [SerializeField]
    [Serializable]
    private class SaveData
    {
        public int score;
        public int levelID;
    }

    public static void SaveProgress()
    {
        SaveData saveData = new SaveData();
        saveData.score = Score;
        saveData.levelID = LevelID;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save" + ProfileID + ".dat");
        Debug.Log("Saving game");
        formatter.Serialize(file, saveData);
        file.Close();
    }

    public static void LoadProgress()
    {

        if (File.Exists(Application.persistentDataPath + "/save" + ProfileID + ".dat"))
        {
            Debug.Log("Loading Game");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save" + ProfileID + ".dat", FileMode.Open);
            SaveData saveData = (SaveData)formatter.Deserialize(file);
            file.Close();
            Score = saveData.score;
            LevelID = saveData.levelID;
        } else
        {
            Score = 0;
            LevelID = 1;
        }
    }
}