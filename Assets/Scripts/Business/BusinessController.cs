public class BusinessController
{
    private readonly BusinessModel _businessModel;
    private readonly BusinessView _businessView;
    private PlayerController _playerController;

    public BusinessController(BusinessModel businessModel, BusinessView businessView)
    {
        _businessModel = businessModel;
        _businessView = businessView;
        _businessView.SetStartProgress(BusinessDataSaver.LoadProgress(_businessModel));
        _businessView.Render(_businessModel);
        _businessView.IncreaseLevelButtonClicked += TryBuyNewLevel;
        _businessView.Upgrade1ButtonClicked += TryBuyUpgrade1;
        _businessView.Upgrade2ButtonClicked += TryBuyUpgrade2;
        _businessView.IncomeProgressFinished += AddEarnedMoney;
        _businessView.ApplicationQuit += SaveProgress;
        _businessModel.ModelChanged += OnModelChanged;
        _businessModel.IncomeStarted += _businessView.ExecuteIncomeProgress;
        _businessModel.TryExecuteIncome();
    }

    public void InitPlayer(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private void OnModelChanged(BusinessModel model)
    {
        _businessView.Render(model);
        SaveData(model);
    }

    private void SaveProgress(float value)
    {
        BusinessDataSaver.SaveProgress(value, _businessModel);
    }

    private void SaveData(BusinessModel model)
    {
        BusinessDataSaver.SaveBusiness(model);
    }

    private void TryBuyNewLevel()
    {
        if (_playerController.IsEnoughMoney(_businessModel.Cost))
        {
            _playerController.Buy(_businessModel.Cost);
            _businessModel.IncreaseLevel();
        }
    }

    private void TryBuyUpgrade1()
    {
        float cost = _businessModel.Upgrade1.Cost;
        if (_playerController.IsEnoughMoney(cost))
        {
            _playerController.Buy(cost);
            _businessModel.DoUpgrade1();
            _businessView.Upgrade1ButtonClicked -= TryBuyUpgrade1;
        }
    }

    private void TryBuyUpgrade2()
    {
        float cost = _businessModel.Upgrade2.Cost;
        if (_playerController.IsEnoughMoney(cost))
        {
            _playerController.Buy(cost);
            _businessModel.DoUpgrade2();
            _businessView.Upgrade2ButtonClicked -= TryBuyUpgrade2;
        }
    }

    private void AddEarnedMoney()
    {
        _playerController.TakeIncome(_businessModel.Income);
    }
}