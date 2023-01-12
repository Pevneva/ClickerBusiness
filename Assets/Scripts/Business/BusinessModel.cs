using System;

public class BusinessModel
{
    private readonly int _baseIncome;
    private readonly int _baseCost;
    private readonly Upgrade _upgrade1;
    private readonly Upgrade _upgrade2;

    public string Name { get; }
    public bool IsIncoming { get; private set; }
    public string NameUpgrade1 { get; }
    public string NameUpgrade2 { get; }
    public BusinessNumber Number { get; }
    public int Level { get; private set; }
    public float Income { get; private set; }
    public float IncomeDelay { get; }
    public float Cost { get; private set; }
    public IUpgradeReadOnly Upgrade1 => _upgrade1;
    public IUpgradeReadOnly Upgrade2 => _upgrade2;

    public event Action<BusinessModel> ModelChanged;
    public event Action<float> IncomeStarted;

    public BusinessModel(BusinessData businessData)
    {
        Name = businessData.Name;
        NameUpgrade1 = businessData.Upgrade1Name;
        NameUpgrade2 = businessData.Upgrade2Name;
        Number = businessData.BusinessNumber;
        _baseIncome = businessData.BaseIncome;
        _baseCost = businessData.BaseCost;
        IncomeDelay = businessData.IncomeDelay;
        _upgrade1 = businessData.Upgrade1;
        _upgrade2 = businessData.Upgrade2;

        BusinessData loadedBusinessData = TryLoadBusinessData(businessData);
        if (loadedBusinessData != null)
        {
            Level = loadedBusinessData.LevelNumber;
            _upgrade1.IsBought = loadedBusinessData.Upgrade1.IsBought;
            _upgrade2.IsBought = loadedBusinessData.Upgrade2.IsBought;
        }
        else
        {
            Level = businessData.LevelNumber;
            _upgrade1.IsBought = businessData.Upgrade1.IsBought;
            _upgrade2.IsBought = businessData.Upgrade2.IsBought;
        }

        Income = CalculateIncome(_baseIncome, Level);
        Cost = CalculateCost(_baseCost, Level);
        TryExecuteIncome();
    }

    public void IncreaseLevel()
    {
        Level++;
        TryExecuteIncome();
        Cost = CalculateCost(_baseCost, Level);
        Income = CalculateIncome(_baseIncome, Level);
        ModelChanged?.Invoke(this);
    }

    public void TryExecuteIncome()
    {
        if (Level > 0 && IsIncoming == false)
        {
            IncomeStarted?.Invoke(IncomeDelay);
            IsIncoming = true;
        }
    }

    public void DoUpgrade1()
    {
        _upgrade1.IsBought = true;
        Income = CalculateIncome(_baseIncome, Level);
        ModelChanged?.Invoke(this);
    }

    public void DoUpgrade2()
    {
        _upgrade2.IsBought = true;
        Income = CalculateIncome(_baseIncome, Level);
        ModelChanged?.Invoke(this);
    }

    public static BusinessData TryLoadBusinessData(BusinessData businessData)
    {
        BusinessSavedData loadedBusinessSavedData =
            SaveManager.Load<BusinessSavedData>(ParamsController.BusinessPrefKey + businessData.BusinessNumber);
        if (loadedBusinessSavedData.Level > 0)
        {
            businessData.LevelNumber = loadedBusinessSavedData.Level;
            businessData.Upgrade1.IsBought = loadedBusinessSavedData.IsBoughtUpgrade1;
            businessData.Upgrade2.IsBought = loadedBusinessSavedData.IsBoughtUpgrade2;
            return businessData;
        }
        else
        {
            return null;
        }
    }

    private float CalculateIncome(int baseIncome, int level)
    {
        float _upgrade1Factor = _upgrade1.IsBought ? _upgrade1.UpgradeFactor : 0;
        float _upgrade2Factor = Upgrade2.IsBought ? Upgrade2.UpgradeFactor : 0;

        return baseIncome * level * (1 + _upgrade1Factor / 100 + _upgrade2Factor / 100);
    }

    private float CalculateCost(int baseCost, int level)
    {
        return baseCost * (level + 1);
    }
}