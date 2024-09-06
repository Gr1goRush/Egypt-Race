using System;
using UnityEngine;

[Serializable]
public class ArrowSpawn
{
    public int Count => _arrowWallet.Coins;

    [SerializeField] private int _startArrowCount;
    [SerializeField] private int _arrowPoolSize;
    [SerializeField] private Arrow _arrowPrefab;

    private Wallet _arrowWallet;
    private Pool<Arrow> _arrowPool;

    public void Init(Wallet arrowWallet, AnimatorOverrideController animatorOverride)
    {
        _arrowWallet = arrowWallet;
        Arrow[] arrows = new Arrow[_arrowPoolSize];
        for (int i = 0; i < _arrowPoolSize; i++)
        {

            arrows[i] = GameObject.Instantiate(_arrowPrefab);
            if (animatorOverride)
            {
                arrows[i].SetupSkeen(animatorOverride);
            }
            arrows[i].gameObject.SetActive(false);
        }
        _arrowPool = new Pool<Arrow>(arrows);
        _arrowWallet.Add(_startArrowCount);
    }
    public Arrow Get()
    {
        if(Count > 0)
        {
            Arrow arrow = _arrowPool.Get();
            _arrowWallet.Remove(1);
            return arrow;
        }
        else
        {
            return null;
        }
    }
}
