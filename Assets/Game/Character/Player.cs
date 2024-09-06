
using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble, IRoadFollower, IHealer
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private DirectionalMoving _directionalMoving;
    [SerializeField] private ArrowSpawn _arrowSpawn;
    [SerializeField] private Animator _charAnimator;
    [SerializeField] private Animator _chariotAnimator;
    private PlayerInput _input;

    public event Action OnDied;
    public Wallet ArrowWallet;
    public Health Health { get; private set; }
    public int RoadIndex
    {
        get
        {
            return _movement.RoadIndex;
        }
    }


    public void Init(SkeenSetup setup)
    {
        SetupSkeen(setup);
        ArrowWallet = new Wallet();
        _arrowSpawn.Init(ArrowWallet, setup.ArrowController);
        Health = new Health(_maxHealth);
        Health.OnHealthChanged += CheckHealth;
        _input = ServiceLocator.Locator.Input;
        _input.AddListener(EventKey.Shoot, Shoot);
    }
    public void Shoot()
    {
        Arrow arrow = _arrowSpawn.Get();
        Vector3 position = transform.position;
        if(arrow) arrow.Shoot(position, RoadIndex, _directionalMoving.Speed);
    }
    public void CheckHealth(int health)
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public void GetDamage(int damage)
    {
        Health.GetDamage(damage);
    }
    private void Die()
    {
        gameObject.SetActive(false);
        OnDied?.Invoke();
    }
    private void OnDisable()
    {
        _input.RemoveListener(EventKey.Shoot, Shoot);
    }

    public void Heal(int health)
    {
        Health.Heal(health);
    }

    internal void SetupSkeen(SkeenSetup setup)
    {
        if (setup.CharacterController)
        {
            _charAnimator.runtimeAnimatorController = setup.CharacterController;
        }
        if (setup.ChariotController)
        {
            _chariotAnimator.runtimeAnimatorController = setup.ChariotController;
        }
    }
}
