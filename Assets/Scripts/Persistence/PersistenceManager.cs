using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    private string _filename;    
    public PersistenceManager instance { get; private set; }

    public string Filename
    {
        get => _filename;
        set { 
            _filename = value;            
        }
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one PersistenceManager instance.");
        }
        instance = this;        
    }

    public void LoadGame()
    {        
        FileDataHandler fileHandler = new FileDataHandler(Application.persistentDataPath, _filename);
        GameData gameData = fileHandler.Load();
        if (gameData != null)
        {
            foreach (IDataPersistence obj in FindAllPersistentObjects())
            {
                obj.LoadData(gameData);
            }
        }
    }

    public void SaveGame()
    {
        List<IDataPersistence> list = FindAllPersistentObjects();
        Debug.Log($"FindAllPersistenceObjects={list.Count}");
        FileDataHandler fileHandler = new FileDataHandler(Application.persistentDataPath, _filename);
        GameData gameData = new GameData();
        foreach (IDataPersistence obj in list)
        {
            obj.SaveData(gameData);
        }
        fileHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllPersistentObjects()
    {
        IEnumerable<IDataPersistence> objects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(objects);
    }

    public bool SaveGameExists()
    {
        FileDataHandler fileHandler = new FileDataHandler(Application.persistentDataPath, _filename);
        return fileHandler.Exists();
    }
}
