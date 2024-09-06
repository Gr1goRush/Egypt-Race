using UnityEngine;

public class Arrow : MonoBehaviour, IRoadFollower
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private string _obstaclesTag;
    [SerializeField] private Animator _animator;

    private Timer _timer;

    public int RoadIndex {get; private set;}

    private void Awake()
    {
        Stop();
    }
    public void Shoot(Vector3 position, int roadIndex, float speed)
    {
        gameObject.SetActive(true);
        RoadIndex = roadIndex;
        transform.position = position;
        _rb.velocity = transform.up * (speed + _speed);
        _timer = Timer.Start(_lifeTime).OnComplete(Stop);
    }
    public void Stop()
    {

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == _obstaclesTag)
        {
            if(collision.gameObject.TryGetComponent<IDamageble>(out IDamageble damageble) && collision.gameObject.TryGetComponent<IRoadFollower>(out IRoadFollower follower))
            {
                if(follower.RoadIndex == RoadIndex)
                {
                    damageble.GetDamage(1);
                    _timer.Stop();
                    Stop();
                }

            }
        }

    }
    internal void SetupSkeen(AnimatorOverrideController setup)
    {
        _animator.runtimeAnimatorController = setup;
    }
    private void OnDisable()
    {
        if(_timer != null) _timer.Stop();
    }
}

internal interface IDamageble
{
    public void GetDamage(int damage);
}