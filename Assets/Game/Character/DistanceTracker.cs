
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private Transform _target;
    private float _lastYPos;
    private Wallet _distanceWallet;
    private const string _bestScoreKey = "BestScore";

    public int Score => _distanceWallet.Coins;
    public void Init(Transform target, Wallet wallet)
    {
        _distanceWallet = wallet;
        _target = target;
        _lastYPos = _target.position.y;
    }
    private void Update()
    {
        float delta = _target.position.y - _lastYPos;
        if(delta >= 1)
        {
            _lastYPos = _target.position.y;
            _distanceWallet.Add(Mathf.RoundToInt(delta));
        }

    }
    public void SaveDistance()
    {
        int bestScore = Saver.GetInt(_bestScoreKey);
        if(bestScore < Score)
        {
            Saver.SaveInt(Score, _bestScoreKey);
        }
    }
}
