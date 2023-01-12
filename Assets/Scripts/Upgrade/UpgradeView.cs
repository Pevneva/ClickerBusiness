using TMPro;
using UnityEngine;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _factor;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private GameObject _costLabel;
    [SerializeField] private GameObject _boughtLabel;

    public void Render(IUpgradeReadOnly upgrade, string upgradeName)
    {
        _name.text = upgradeName;
        _factor.text = "+" + upgrade.UpgradeFactor + "%";
        _cost.text = "cost:" + upgrade.Cost + "$";
        
        _costLabel.SetActive(upgrade.IsBought == false);
        _boughtLabel.SetActive(upgrade.IsBought);
    }
}