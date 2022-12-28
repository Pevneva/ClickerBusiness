using System;
using UnityEngine;

public class BusinessModel
{
    private readonly int _baseIncome;
    private readonly int _baseCost;
    private float _upgrade1Factor;
    private float _upgrade2Factor;

    public string Name { get; }
    public bool IsIncoming { get; private set; }

    public string NameUpgrade1 { get; }
    public string NameUpgrade2 { get; }
    public BusinessNumber Number { get; }
    public int Level { get; private set; }
    public float Income { get; private set; }
    public float IncomeDelay { get; }
    public float Cost { get; private set; }
    public Upgrade Upgrade1 { get; }
    public Upgrade Upgrade2 { get; }

    public event Action<BusinessModel> ModelChanged;
    public event Action<float> IncomeStarted;

    public BusinessModel(BusinessData businessData)
    {
        Name = businessData.Name;
        NameUpgrade1 = businessData.Upgrade_1Name;
        NameUpgrade2 = businessData.Upgrade_2Name;
        Number = businessData.BusinessNumber;
        Level = businessData.LevelNumber;
        _baseIncome = businessData.BaseIncome;
        _baseCost = businessData.BaseCost;
        IncomeDelay = businessData.IncomeDelay;
        Upgrade1 = businessData.Upgrade1;
        Upgrade2 = businessData.Upgrade2;

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
        Upgrade1.IsBought = true;
        Income = CalculateIncome(_baseIncome, Level);
        ModelChanged?.Invoke(this);
    }

    public void DoUpgrade2()
    {
        Upgrade2.IsBought = true;
        Income = CalculateIncome(_baseIncome, Level);
        ModelChanged?.Invoke(this);
    }

    private float CalculateIncome(int baseIncome, int level)
    {
        _upgrade1Factor = Upgrade1.IsBought ? Upgrade1.UpgradeFactor : 0;
        _upgrade2Factor = Upgrade2.IsBought ? Upgrade2.UpgradeFactor : 0;

        return baseIncome * level * (1 + _upgrade1Factor / 100 + _upgrade2Factor / 100);
    }

    private float CalculateCost(int baseCost, int level)
    {
        return baseCost * (level + 1);
    }
}