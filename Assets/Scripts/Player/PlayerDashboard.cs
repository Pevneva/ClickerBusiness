using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashboard : MonoBehaviour
{
    [SerializeField] private List<BusinessData> _businessesData;
    [SerializeField] private BusinessView _template;
    [SerializeField] private GameObject _container;

    private PlayerController _playerController;
    private List<BusinessController> _businessControllers;

    public event Action ApplicationQuit;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
        ApplicationQuit += _playerController.OnApplicationQuit;
        ShowBusinesses();
    }

    private void ShowBusinesses()
    {
        _businessControllers = new List<BusinessController>();
        foreach (var data in _businessesData)
            InitItem(data);
    }

    private void InitItem(BusinessData businessData)
    {
        BusinessView view = Instantiate(_template, _container.transform);
        BusinessController businessController = new BusinessController(new BusinessModel(businessData), view);
        businessController.InitPlayer(_playerController);
        _businessControllers.Add(businessController);
        ApplicationQuit += businessController.OnApplicationQuit;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
            ApplicationQuit?.Invoke();
    }
}
