using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private int _maxTileCount;
    [SerializeField] private GameObject _tile;
    [SerializeField] private float _destroyDistance;
    [SerializeField] private float _scale;
    [SerializeField] private Vector3 _direction;
    private Pool<GameObject> _tilePool;
    private Transform _camera;
    private GameObject _last;

    private void Awake()
    {
        _camera = Camera.main.transform;
        GameObject[] tiles = new GameObject[_maxTileCount];
        _last = gameObject;
        GameObject last = gameObject;
        for (int i = 0; i < _maxTileCount; i++)
        {
            Vector3 position = _last.transform.position + _direction.normalized * (_scale) * i;
            tiles[i] = Instantiate(_tile, position, transform.rotation, transform);
            last = tiles[i];
        }
        _last = last;
        _tilePool = new Pool<GameObject>(tiles);
    }
    private void LateUpdate()
    {
        GameObject first = _tilePool.GetNotDelete();
        if(_camera.position.y - first.transform.position.y >= _destroyDistance)
        {
            Vector3 position = first.transform.position + _direction.normalized * (_scale) * (_maxTileCount - 1);
            Spawn(position);
        }

    }
    private GameObject Spawn(Vector3 position)
    {
        GameObject obj = _tilePool.Get();
        obj.transform.position = position;
        obj.SetActive(true);
        _last = obj;
        return obj;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + _direction.normalized * _scale/2, 1f);
    }
}
