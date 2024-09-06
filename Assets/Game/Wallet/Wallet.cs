using System;

public class Wallet
{
    private int _coins;
    public int Coins { 
        get
        {
            return _coins;
        } 
        private set
        {
            _coins = value;
            OnCoinsChanget?.Invoke(_coins);
        }
    }
    public event Action<int> OnCoinsChanget;
    public void Add(int coins)
    {
        Coins += coins;
    }
    public bool TryRemove(int coins)
    {
        if(coins > Coins)
        {
            return false;
        }
        Coins -= coins;
        return true;
    }
    public void Remove(int coins)
    {
        Coins -= coins;
    }
}