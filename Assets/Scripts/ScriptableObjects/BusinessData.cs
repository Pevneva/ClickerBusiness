using UnityEngine;

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
    public string Upgrade1Name => _name.Upgrade1Name;
    public string Upgrade2Name => _name.Upgrade2Name;
    public float IncomeDelay => _incomeDelay;
    public int BaseCost => _baseCost;
    public int BaseIncome => _baseIncome;
    public BusinessNumber BusinessNumber => _businessNumber;
    public Upgrade Upgrade1 => _upgrade1;
    public Upgrade Upgrade2 => _upgrade2;

    public int LevelNumber
    {
        get { return _levelNumber; }
        set { _levelNumber = value; }
    }
}
