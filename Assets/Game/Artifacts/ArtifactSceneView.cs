using UnityEngine;

public class ArtifactSceneView : MonoBehaviour, IRoadFollower
{
    [SerializeField] private string _name;
    [SerializeField] private float _lifeTime;
    [SerializeField] protected string _playerTag;

    private Timer _timer;

    public int RoadIndex { get; private set; }

    public void Spawn(Vector3 position, int roadIndex)
    {
        gameObject.SetActive(true);
        _timer = Timer.Start(_lifeTime).OnComplete(Disable);
        transform.position = position;
        RoadIndex = roadIndex;
    }
    public virtual void Realize()
    {
        GlobalWallet.AddCoins(1, _name);
        if(_timer != null) _timer.Stop();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _playerTag)
        {

            GameObject player = other.gameObject;
            if (player.TryGetComponent<IDamageble>(out IDamageble damageble) && player.TryGetComponent<IRoadFollower>(out IRoadFollower follower))
            {
                if (follower.RoadIndex == RoadIndex)
                {
                    Realize();
                    Disable();
                }
            }

        }
    }
    private void OnDisable()
    {
        if(_timer != null) _timer.Stop();
    }
}
