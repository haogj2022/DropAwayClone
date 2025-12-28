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
        Data = PlayerDataManager.LoadPlayerData();
    }

    private void Start()
    {
        Shop.Instance.UpdateCurrentCoin(Data.Coins);
    }

    private void OnDestroy()
    {
        PlayerDataManager.SavePlayerData(Data);
    }

    public void UpdatePlayerData(ItemType itemType, int amount = 0)
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
            case ItemType.InfiniteLivesHours:
                Data.InfiniteLivesHours += amount;
                break;
            case ItemType.RemoveAds:
                Data.RemoveAds = true;
                break;
        }
    }
}
