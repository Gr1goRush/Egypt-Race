using System.Collections.Generic;
using UnityEngine.UIElements;

public class Pool<T> 
{
    private T _prefab;
    private Queue<T> _pool = new Queue<T>();

    public Pool(T[] instances)
    {
        for (int i = 0; i < instances.Length; i++)
        {
            _pool.Enqueue(instances[i]);
        }
    }
    public void Add(T instance)
    {
        _pool.Enqueue(instance);
    }
    public T Get()
    {
        T obj = _pool.Dequeue();
        _pool.Enqueue(obj);
        return obj;
    }
    public T GetNotDelete()
    {
        T obj = _pool.Peek();
        return obj;
    }
}
