using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnDistanceRange;
    [SerializeField] private Obstacle[] _obstacles;
    [SerializeField] private float _spawnCheckDistance;
    [SerializeField] private int _obstaclesPoolSize;
    [SerializeField] private ArtifactPool _artifactPool;

    private Transform _target;
    private int[] _roads;
    private Pool<Obstacle>[] _obstaclesPools;
    private Transform _lastObstacle;
    private void Start()
    {
        _artifactPool.Init(transform);
        _target = FindObjectOfType<Player>().transform;
        _lastObstacle = transform;
        _roads = ServiceLocator.Locator.Roads;
        _obstaclesPools = new Pool<Obstacle>[_obstacles.Length];
        for (int i = 0; i < _obstacles.Length; i++)
        {
            Obstacle[] pool = new Obstacle[_obstaclesPoolSize];
            for (int j = 0; j < _obstaclesPoolSize; j++)
            {
                Obstacle obstacle = Instantiate(_obstacles[i], transform);
                obstacle.Init(_artifactPool);
                obstacle.gameObject.SetActive(false);
                pool[j] = obstacle;
            }
            _obstaclesPools[i] = new Pool<Obstacle>(pool);
        }
        SpawnLine();
    }
    private void Update()
    {
        if(_lastObstacle.position.y - _target.position.y <= _spawnCheckDistance)
        {
            SpawnLine();
        }
    }
    private void SpawnLine()
    {
        int obstacleCount = Random.Range(0,_roads.Length);
        float Ypos = _lastObstacle.position.y + Random.Range(_spawnDistanceRange.x, _spawnDistanceRange.y);
        List<int> avaliableIndices = new List<int> ();
        for (int i = 0; i < _roads.Length; i++)
        { 
            avaliableIndices.Add(i);
        }
            for (int i = 0; i < obstacleCount; i++)
        {
            int index = avaliableIndices[Random.Range(0, avaliableIndices.Count)];
            SpawnObstacle(index, Ypos);
            avaliableIndices.Remove(index);
        }
    }
    private void SpawnObstacle(int roadIndex, float Ypos)
    {
        Vector3 position = new Vector3(_roads[roadIndex], Ypos, transform.position.z);
        Obstacle obstacle = _obstaclesPools[Random.Range(0, _obstaclesPools.Length)].Get();
        _lastObstacle = obstacle.transform;
        obstacle.Spawn(position, roadIndex);
    }
}
