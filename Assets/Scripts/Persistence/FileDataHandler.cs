using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Utility class to write and read to the save file
/// </summary>
public class FileDataHandler
{
    // Save file path
    private string _path;
    // Save file filename
    private string _filename;

    /// <summary>
    /// Constructor to initialize with path and filename
    /// </summary>
    /// <param name="path">save file path</param>
    /// <param name="filename">save file filename</param>
    public FileDataHandler(string path, string filename)
    {
        _path = path;
        _filename = filename;
    }

    /// <summary>
    /// Loads GameData from save file
    /// </summary>
    /// <returns></returns>
    public GameData Load()
    {
        string fullPath = Path.Combine(_path, _filename);
        GameData gameData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string data = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
                gameData = JsonUtility.FromJson<GameData>(data);
            } 
            catch (Exception e)
            {
                Debug.LogError($"Error when loading game data from {fullPath}. \n {e}");
            }
        }
        return gameData;
    }

    /// <summary>
    /// Saves game data to save file
    /// </summary>
    /// <param name="gameData"></param>
    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(_path, _filename);

        Debug.Log($"Save... Enemies={gameData.enemies.Count}, Loot={gameData.loot.Count}");

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string data = JsonUtility.ToJson(gameData, true);
            Debug.Log($"JSON={data}");
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
            Debug.Log($"Game saved successfully in {fullPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error when saving game data to {fullPath}. \n {e}");
        }
    }

    public bool Exists()
    {
        string fullPath = Path.Combine(_path, _filename);
        return File.Exists(fullPath);
    }
}