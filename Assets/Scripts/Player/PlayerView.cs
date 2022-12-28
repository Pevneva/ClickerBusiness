using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _balanceText;

    public void SetBalance(float balance)
    {
        _balanceText.text = balance + "$";
    }
}
