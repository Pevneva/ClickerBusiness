using System;

public class PlayerModel
{
    private int _startBalance;

    public float Balance { get; private set; }

    public event Action<float> BalanceUpdated;

    public PlayerModel(float startBalance)
    {
        Balance = startBalance;
    }

    public static PlayerModel TryLoadPlayerData()
    {
        PlayerSavedData loadedPlayerSavedData = SaveManager.Load<PlayerSavedData>(ParamsController.PlayerKey);
        return new PlayerModel(loadedPlayerSavedData.Balance);
    }

    public void SpendMoney(float spentMoney)
    {
        Balance -= spentMoney;

        if (Balance < 0)
            Balance = 0;

        BalanceUpdated?.Invoke(Balance);
    }

    public void EarnMoney(float earnedMoney)
    {
        Balance += earnedMoney;
        BalanceUpdated?.Invoke(Balance);
    }
}