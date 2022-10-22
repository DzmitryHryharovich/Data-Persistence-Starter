using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class SavaAndLoad : MonoBehaviour
{
    public static SavaAndLoad Instance;
    public List<Player> topPlayers = new List<Player>();
    public class Player
    {
        public string name;
        public int score;
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Load();
    }
    [Serializable]
    class SaveData
    {
        public List<Player> topPlayers;// = new List<Player>();
    }

    

    public  void Save()
    {
        SaveData saveData = new SaveData();
        saveData.topPlayers = topPlayers;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Save");
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            topPlayers = saveData.topPlayers;
            Debug.Log("Load");
        }
    }
}
