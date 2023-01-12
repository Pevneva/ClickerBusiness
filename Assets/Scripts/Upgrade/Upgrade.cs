using UnityEngine;

[CreateAssetMenu(menuName = "Create New Upgrade", fileName = "New Upgrade")]
public class Upgrade : ScriptableObject, IUpgradeReadOnly
{
    [SerializeField] private float _cost;
    [SerializeField] private float _upgradeFactor;
    [SerializeField] private bool _isBought;

    public float Cost => _cost;
    public float UpgradeFactor => _upgradeFactor;

    public bool IsBought
    {
        get { return _isBought; }
        set { _isBought = value; }
    }
}