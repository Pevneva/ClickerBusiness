using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private TMP_Text _income;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _increaseLevelButton;
    [SerializeField] private Slider _incomeProgress;
    [SerializeField] private UpgradeView _upgradeView1;
    [SerializeField] private UpgradeView _upgradeView2;

    public event Action IncreaseLevelButtonClicked;
    public event Action Upgrade1ButtonClicked;
    public event Action Upgrade2ButtonClicked;
    public event Action IncomeProgressFinished;
    public event Action<float> ApplicationQuit;

    private Tween _incomeProgressTween;

    private void Start()
    {
        _increaseLevelButton.onClick.AddListener(() => IncreaseLevelButtonClicked?.Invoke());
        _upgradeView1.gameObject.GetComponent<Button>().onClick.AddListener(() => Upgrade1ButtonClicked?.Invoke());
        _upgradeView2.gameObject.GetComponent<Button>().onClick.AddListener(() => Upgrade2ButtonClicked?.Invoke());
    }

    public void Render(BusinessModel model)
    {
        _name.text = model.Name;
        _levelNumber.text = model.Level.ToString();
        _income.text = model.Income + "$";
        _cost.text = "cost:" + model.Cost + "$";
        _upgradeView1.Render(model.Upgrade1, model.NameUpgrade1);
        _upgradeView2.Render(model.Upgrade2, model.NameUpgrade2);
        
        if (model.IsIncoming == false)
            _incomeProgress.gameObject.SetActive(false);
        else if (_incomeProgressTween == null)
            ExecuteIncomeProgress(model.IncomeDelay);
    }

    public void ExecuteIncomeProgress(float incomeDelay)
    {
        Debug.Log("=== ExecuteIncomeProgress === AAA");
        _incomeProgressTween.Kill();
        _incomeProgress.gameObject.SetActive(true);
        _incomeProgressTween = _incomeProgress.DOValue(1, incomeDelay * (1 - _incomeProgress.value)).SetEase(Ease.Linear).OnComplete(() =>
        {
            IncomeProgressFinished?.Invoke();
            _incomeProgress.value = 0;
            ExecuteIncomeProgress(incomeDelay);
        });
    }

    private void OnApplicationQuit()
    {
        ApplicationQuit?.Invoke(_incomeProgress.value);
    }

    public void SetStartProgress(float value)
    {
        _incomeProgress.value = value;
    }
}