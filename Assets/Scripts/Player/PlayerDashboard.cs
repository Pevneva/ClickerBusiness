using System.Collections.Generic;
using UnityEngine;

public class PlayerDashboard : MonoBehaviour
{
    [SerializeField] private List<BusinessData> _businessesData;
    [SerializeField] private BusinessView _template;
    [SerializeField] private GameObject _container;

    private PlayerController _playerController;
    private List<BusinessController> _businessControllers;
    
    private void Start()
    {
        InitBusinesses();
    }

    private void InitBusinesses()
    {
        _businessControllers = new List<BusinessController>();
        foreach (var data in _businessesData)
        {
            AddItem(data);
        }
    }

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
    }
    
    private void AddItem(BusinessData businessData)
    {
        BusinessView view = Instantiate(_template, _container.transform);

        if (BusinessDataSaver.LoadBusinessData(businessData.BusinessNumber, out int level, out bool upgrade1, out bool upgrade2))
        {
            businessData.LevelNumber = level;
            businessData.Upgrade1.IsBought = upgrade1;
            businessData.Upgrade2.IsBought = upgrade2;
        };
        BusinessController businessController = new BusinessController(new BusinessModel(businessData), view);
        businessController.InitPlayer(_playerController);
        _businessControllers.Add(businessController);
    }
}
