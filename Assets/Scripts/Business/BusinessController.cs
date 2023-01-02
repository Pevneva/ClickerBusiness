public class BusinessController
{
    private readonly BusinessModel _businessModel;
    private readonly BusinessView _businessView;
    private readonly BusinessSavedData _businessSavedData = new BusinessSavedData();
    private PlayerController _playerController;

    public BusinessController(BusinessModel businessModel, BusinessView businessView)
    {
        _businessModel = businessModel;
        _businessView = businessView;
        _businessView.SetStartProgress(GetBusinessProgressValue());
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

    private float GetBusinessProgressValue()
    {
        BusinessSavedData savedData =
            SaveManager.Load<BusinessSavedData>(ParamsController.BusinessPrefKey + _businessModel.Number);
        return savedData.ProgressValue;
    }

    private void OnModelChanged(BusinessModel model)
    {
        _businessView.Render(model);
    }

    private void SaveProgress(float value)
    {
        _businessSavedData.Level = _businessModel.Level;
        _businessSavedData.IsBoughtUpgrade1 = _businessModel.Upgrade1.IsBought;
        _businessSavedData.IsBoughtUpgrade2 = _businessModel.Upgrade2.IsBought;
        _businessSavedData.ProgressValue = value;
        SaveManager.Save(ParamsController.BusinessPrefKey + _businessModel.Number, _businessSavedData);
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