using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonManager
{
    public static void SaveJson(JsonData data, string fileName)
    {
        if (fileName == string.Empty)
        {
            Debug.LogWarning($"{fileName} is empty");
            return;
        }

        string fileData = JsonConvert.SerializeObject(data, Formatting.Indented);
        string filePath = Path.Combine(Application.dataPath, "Resources", fileName + ".json");
        File.WriteAllText(filePath, fileData);
        Debug.Log($"{fileName} has been saved to {filePath}");
    }

    public static JsonData LoadJson(string fileName)
    {
        string filePath = Path.Combine(Application.dataPath, "Resources", fileName + ".json");

        if (File.Exists(filePath) == false)
        {
            Debug.LogWarning($"{fileName} was not found in {filePath}");
            return null;
        }

        string fileData = File.ReadAllText(filePath);
        JsonData data = JsonConvert.DeserializeObject<JsonData>(fileData);
        Debug.Log($"{fileName} has been loaded from {filePath}");
        return data;
    }
}

public class JsonData
{
    public int Column { get; set; }
    public int Row { get; set; }
    //public Dictionary<Vector2Int, BoardTile> BoardTileDictionary { get; set; }
    public Dictionary<Vector2Int, Color> BoardTile {  get; set; }

    public JsonData(int column, int row, Dictionary<Vector2Int, Color> boardTileDictionary)
    {
        Column = column;
        Row = row;
        BoardTile = boardTileDictionary;
    }
}