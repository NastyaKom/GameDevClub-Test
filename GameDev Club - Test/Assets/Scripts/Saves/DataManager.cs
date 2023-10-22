using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistentsObjects;

    private FileManager dataHandler;

    public static DataManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        this.dataHandler = new FileManager(Application.persistentDataPath, fileName);
        this.dataPersistentsObjects = FindAllDataPersistentObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistentObjects()
    {
        IEnumerable<IDataPersistence> dataPersistentsObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistentsObjects);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
     {
        Debug.Log("Data Manager Save");
        foreach (IDataPersistence dataPersistentObj in dataPersistentsObjects)
        {
            dataPersistentObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        Debug.Log("Data Manager Load");
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            NewGame();
        }
        foreach (IDataPersistence dataPersistentObj in dataPersistentsObjects)
        {
            dataPersistentObj.LoadData(gameData);
        }
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }
}
