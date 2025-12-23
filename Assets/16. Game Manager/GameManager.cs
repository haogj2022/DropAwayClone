using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData Data;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        Data = PlayerDataManager.LoadPlayerData();
        Shop.Instance.UpdateCurrentCoin(Data.Coins);
    }

    private void OnDestroy()
    {
        PlayerDataManager.SavePlayerData(Data);
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
            case ItemType.Magnets:
                Data.Magnets += amount;
                break;
            case ItemType.TimeFreezes:
                Data.TimeFreezes += amount;
                break;
        }
    }

    public void UpdatePlayerData(float infiniteLivesHours)
    {
        Data.InfiniteLivesHours += infiniteLivesHours;
    }

    public void UpdatePlayerData(bool removeAds = true)
    {
        Data.RemoveAds = removeAds;
    }
}
