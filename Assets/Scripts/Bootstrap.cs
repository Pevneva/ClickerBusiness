using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerDashboard _playerDashboard;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = new PlayerController(PlayerModel.TryLoadPlayerData(), _playerView);
        _playerDashboard.Init(_playerController);
    }
}