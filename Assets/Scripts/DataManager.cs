using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public string bestPlayerName;
    public int highScore;
    public bool hasSaved = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    [System.Serializable]
    class DataToSave
    {
        public string bestPlayerName;
        public int highScore;
        public bool hasSaved;
    }

    public void SaveData()
    {
        DataToSave data = new DataToSave();
        data.bestPlayerName = bestPlayerName;
        data.highScore = highScore;
        data.hasSaved = hasSaved;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataToSave data = JsonUtility.FromJson<DataToSave>(json);

            bestPlayerName = data.bestPlayerName;
            highScore = data.highScore;
            hasSaved = data.hasSaved;
        }
    }

    public void DeleteSaveData()
    {
        playerName = null;
        bestPlayerName = null;
        highScore = 0;
        hasSaved = false;
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
