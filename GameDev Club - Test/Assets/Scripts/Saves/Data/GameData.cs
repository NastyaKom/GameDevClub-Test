using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, string> inventoryCollection;
    public int score;

    public GameData()
    {
        inventoryCollection = new SerializableDictionary<string, string>();
        score = 0;
    }
}
