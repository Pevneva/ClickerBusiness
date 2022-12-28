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
