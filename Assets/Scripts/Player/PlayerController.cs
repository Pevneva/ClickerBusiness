public class PlayerController
{
    private readonly PlayerModel _playerModel;
    private readonly PlayerView _playerView;
    private readonly PlayerSavedData _playerSavedData = new PlayerSavedData();

    public PlayerController(PlayerModel model, PlayerView view)
    {
        _playerModel = model;
        _playerView = view;
        _playerView.SetBalance(model.Balance);
        _playerModel.BalanceUpdated += OnBalanceUpdated;
    }

    private void OnBalanceUpdated(float value)
    {
        _playerView.SetBalance(value);
        _playerSavedData.Balance = value;
        SaveManager.Save(ParamsController.PlayerKey, _playerSavedData);
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