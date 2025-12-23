using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class PlayerDataManager
{
    public static void SavePlayerData(PlayerData data)
    {
        string fileData = JsonConvert.SerializeObject(data, Formatting.Indented);
        string filePath = Path.Combine(Application.dataPath, "playerData.json");
        File.WriteAllText(filePath, fileData);
        Debug.Log($"Player data has been saved to {filePath}");
    }

    public static PlayerData LoadPlayerData()
    {
        string filePath = Path.Combine(Application.dataPath, "playerData.json");

        if (File.Exists(filePath) == false)
        {
            Debug.LogWarning($"Player data was not found. Create new player data");
            SavePlayerData(new PlayerData());
        }

        string fileData = File.ReadAllText(filePath);
        PlayerData data = JsonConvert.DeserializeObject<PlayerData>(fileData);
        Debug.Log($"Player data has been loaded from {filePath}");
        return data;
    }
}

public class PlayerData
{
    public int Coins;
    public int Propellers;
    public int Magnets;
    public int TimeFreezes;
    public float InfiniteLivesDuration;
}
