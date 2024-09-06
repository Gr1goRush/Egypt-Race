using UnityEngine;

public class HeartArtifactView : ArtifactSceneView
{
    [SerializeField] private int _healedHealth;
    public override void Realize()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _playerTag)
        {

            GameObject player = other.gameObject;
            if (player.TryGetComponent<IHealer>(out IHealer healer) && player.TryGetComponent<IRoadFollower>(out IRoadFollower follower))
            {
                if (follower.RoadIndex == RoadIndex)
                {
                    healer.Heal(_healedHealth);
                    Realize();
                }
            }

        }
    }
}

internal interface IHealer
{
    public void Heal(int health);
}