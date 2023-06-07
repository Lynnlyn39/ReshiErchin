
using UnityEngine;


/// <summary>
/// Class and auxiliar structs to store the persistent game data
/// </summary>
[System.Serializable]
public class GameData
{
    public float timeOfDay;
    public Vector3 cameraPosition;
    public PlayerData player;
    public SerializableDictionary<string, EnemyData> enemies;
    public SerializableDictionary<string, bool> loot;
    
    public GameData()
    {
        enemies = new SerializableDictionary<string, EnemyData>();
        loot = new SerializableDictionary<string, bool>();
    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 position;
    public int hp;
    public bool hasLamp;

    public PlayerData(Vector3 p)
    {
        position = p;
        hp = 0;
        hasLamp = false;
    }
}

[System.Serializable]
public struct EnemyData
{   
    public Vector3 position;
    public int hp;

    public EnemyData(Vector3 p, int h)
    {
        position = p;
        hp = h;
    }
}
