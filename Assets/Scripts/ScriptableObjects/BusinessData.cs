using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create New Business Data",fileName = "Business Data")]
public class BusinessData : ScriptableObject
{
    [SerializeField] private NameBusiness _name;
    [SerializeField] private float _incomeDelay;
    [SerializeField] private int _baseCost;
    [SerializeField] private int _baseIncome;
    [SerializeField] private int _levelNumber;
    [SerializeField] private Upgrade _upgrade1;
    [SerializeField] private Upgrade _upgrade2;
    [SerializeField] private BusinessNumber _businessNumber;

    public string Name => _name.BusinessName;
    public string Upgrade_1Name => _name.Upgrade1Name;
    public string Upgrade_2Name => _name.Upgrade2Name;
    public float IncomeDelay => _incomeDelay;
    public int BaseCost => _baseCost;
    public int BaseIncome => _baseIncome;
    public BusinessNumber BusinessNumber => _businessNumber;

    public int LevelNumber
    {
        get { return _levelNumber; }
        set { _levelNumber = value; }
    }

    public Upgrade Upgrade1     {
        get { return _upgrade1; }
        set { _upgrade1 = value; }
    }
    public Upgrade Upgrade2     {
        get { return _upgrade2; }
        set { _upgrade2 = value; }
    }
}
