
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IRoadFollower
{
    [SerializeField] private float _minDragDistance;
    [SerializeField] private Rigidbody _rb;
    private int[] _roads;
    private PlayerInput _input;
    private bool _canChangeDirection;
    public int RoadIndex { get; private set; } = 1;

    private void Start()
    {
        _roads = ServiceLocator.Locator.Roads;
        _input = ServiceLocator.Locator.Input;
        ChangeRoad(0);
    }
    private void Update()
    {
        if (!_input.GetInputAction(InputKey.PointerDown))
        {
            _canChangeDirection = true;
        }
        Vector2 input = _input.GetInputAxis(AxisKey.Swipe);
        if (_canChangeDirection && Mathf.Abs(input.x) > _minDragDistance)
        {
            int direction = input.x > 0 ? 1 : -1;
            ChangeRoad(direction);
            _canChangeDirection = false;
        }
    }
    private void ChangeRoad(int direction)
    {
        RoadIndex += direction;
        RoadIndex = Mathf.Clamp(RoadIndex, 0, _roads.Length - 1);
        transform.position = new Vector3(_roads[RoadIndex], transform.position.y, transform.position.z);

    }
}
