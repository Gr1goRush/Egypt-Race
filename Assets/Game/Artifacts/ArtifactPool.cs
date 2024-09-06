using UnityEngine;

[System.Serializable]
public class ArtifactPool
{
    [SerializeField] private ArtifactSceneView[] _artifacts;
    [SerializeField] private int _artifactsPoolSize;

    private Pool<ArtifactSceneView>[] _artifactPools;
    public void Init(Transform parent)
    {
        _artifactPools = new Pool<ArtifactSceneView>[_artifacts.Length];
        for (int i = 0; i < _artifacts.Length; i++)
        {
            ArtifactSceneView[] pool = new ArtifactSceneView[_artifactsPoolSize];
            for (int j = 0; j < _artifactsPoolSize; j++)
            {
                ArtifactSceneView artifact = GameObject.Instantiate(_artifacts[i], parent);
                artifact.gameObject.SetActive(false);
                pool[j] = artifact;
            }
            _artifactPools[i] = new Pool<ArtifactSceneView>(pool);
        }
    }
    public ArtifactSceneView Get()
    {
        ArtifactSceneView artifact = _artifactPools[Random.Range(0, _artifactPools.Length)].Get();
        return artifact;

    }
}
