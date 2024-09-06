
using System;
using UnityEngine;

public class TimerUpdater : MonoBehaviour
{
    private event Action OnUpdate;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void AddListener(Action update)
    {
        OnUpdate += update;
    }
    public void RemoveListener(Action update)
    {
        OnUpdate -= update;
    }
    private void Update()
    {
        OnUpdate?.Invoke();
    }
}
