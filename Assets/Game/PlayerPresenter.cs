using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private CameraFollowing _cameraFollowing;
    [SerializeField] private WalletPresenter _arrowWalletPresenter;
    [SerializeField] private DistanceTracker _tracker;
    [SerializeField] private WalletPresenter _trackerWalletPresenter;
    [SerializeField] private SkeensData _arrowData;
    [SerializeField] private SkeensData _characterData;
    [SerializeField] private SkeensData _chariotData;

    private void Awake()
    {
        SkeenSetup setup = new SkeenSetup(_arrowData, _characterData, _chariotData);
        Player player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        player.OnDied += ShowScore;
        player.Init(setup);
        Wallet trackerWallet = new Wallet();
        _tracker.Init(player.transform, trackerWallet);
        _trackerWalletPresenter.Init(trackerWallet);
        _losePanel.Init(trackerWallet);
        _arrowWalletPresenter.Init(player.ArrowWallet);
        _healthView.Init(player.Health);
        _cameraFollowing.Init(player.transform);
        _losePanel.Close();
    }
    private void ShowScore()
    {
        _tracker.SaveDistance();
        _losePanel.Open();
    }
}
