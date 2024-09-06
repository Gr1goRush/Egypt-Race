using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArtifactsWallet
{
    [SerializeField] private ArtifactWalletPresenter[] _presenters;
    private Dictionary<string, Wallet> _wallets;
    public void Init()
    {
        _wallets = new Dictionary<string, Wallet>();
        for (int i = 0; i < _presenters.Length; i++)
        {
            Wallet wallet = new Wallet();
            int coins = Saver.GetInt(_presenters[i].Type);
            wallet.Add(coins);
            _wallets[_presenters[i].Type] = wallet;
            _presenters[i].Wallet.Init(wallet);
        }
    }
    public bool TryRemoveArtifacts(string type, int count)
    {
        Wallet wallet = _wallets[type];
        bool isRemoved = wallet.TryRemove(count);
        if (isRemoved)
        {
            Saver.SaveInt(wallet.Coins, type);
        }
        return isRemoved;
    }
}
[System.Serializable]
public struct ArtifactWalletPresenter
{
    public string Type;
    public WalletPresenter Wallet;
}