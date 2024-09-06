using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private bool _canMove = true;
    [SerializeField] private Transform _target;
    
    private Vector3 _offset;
    private float _positionX;

    public void Init(Transform target)
    {
        _target = target;
        _offset = transform.position - _target.position;
        _positionX = transform.position.x;
    }
    private void Update()
    {
        Vector3 position = _target.position + _offset;
        position.x = _positionX;
        transform.position = position;
    }
}
