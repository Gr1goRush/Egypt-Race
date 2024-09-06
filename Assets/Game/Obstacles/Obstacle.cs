using UnityEngine;

public class Obstacle : MonoBehaviour, IDamageble, IRoadFollower
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _playerTag;
    [SerializeField] private Collider _collider;

    private ArtifactPool _artifactPool;
    private bool isAlive = true;

    public int RoadIndex {get; private set;}

    public void Init(ArtifactPool artifactPool)
    {
        _artifactPool = artifactPool;
    }
    public void GetDamage(int damage)
    {
        if (!isAlive) return;
        ArtifactSceneView artifact = _artifactPool.Get();
        artifact.Spawn(transform.position, RoadIndex);
        Die();
    }

    public void Spawn(Vector3 position, int roadIndex)
    {
        isAlive = true;
        _collider.enabled = true;
        _animator.SetTrigger("Reset");
        RoadIndex = roadIndex;
        gameObject.SetActive(true);
        transform.position = position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isAlive) return;
        if(other.tag == _playerTag)
        {
           
            GameObject player = other.gameObject;
            if(player.TryGetComponent<IDamageble>(out IDamageble damageble) && player.TryGetComponent<IRoadFollower>(out IRoadFollower follower))
            {
                if(follower.RoadIndex == RoadIndex) damageble.GetDamage(1);
                Die();
            }

        }
    }
    private void Die()
    {
        isAlive = false;
        _collider.enabled = false;
        _animator.SetTrigger("Destroy");
    }
}