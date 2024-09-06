using UnityEngine;

public class DirectionalMoving : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.down;
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private Rigidbody _rb;


    public float Speed => _speed;
    private void FixedUpdate()
    {
        _speed += _acceleration * Time.deltaTime;

        _rb.position +=(direction * _speed * Time.deltaTime);
    }
}
