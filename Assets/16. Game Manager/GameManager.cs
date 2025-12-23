using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static PlayerData Data;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGame();
    }

    private void LoadGame()
    {
        Data = PlayerDataManager.LoadPlayerData();
    }

    public void UpdatePlayerData(ItemType itemType, int amount)
    {
        switch (itemType)
        {
            case ItemType.Coins:
                Data.Coins += amount;
                break;
            case ItemType.Propellers:
                Data.Propellers += amount;
                break;
            case ItemType.Infinite_Lives:
                Data.InfiniteLivesDuration += amount;
                break;
            case ItemType.Magnets:
                Data.Magnets += amount;
                break;
            case ItemType.Time_Freezes:
                Data.TimeFreezes += amount;
                break;
            case ItemType.No_Ads:
                break;
        }

        PlayerDataManager.SavePlayerData(Data);
    }
}
