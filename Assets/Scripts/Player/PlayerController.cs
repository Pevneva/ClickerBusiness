public class PlayerController
{
    private readonly PlayerModel _playerModel;
    private readonly PlayerView _playerView;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        _playerModel = model;
        _playerView = view;
        _playerModel.BalanceUpdated += _playerView.SetBalance;
        _playerModel.BalanceUpdated += PlayerDataSaver.SaveBalance;
    }

    public bool IsEnoughMoney(float cost)
    {
        return _playerModel.Balance >= cost;
    }

    public void Buy(float cost)
    {
        _playerModel.SpendMoney(cost);
    }

    public void TakeIncome(float income)
    {
        _playerModel.EarnMoney(income);
    }
}