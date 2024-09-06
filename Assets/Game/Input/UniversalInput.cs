using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UniversalInput : PlayerInput, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Button _shootBtn;
    private bool _pointerDown;
    private Vector2 _drag;

    private void Awake()
    {
        _shootBtn.onClick.AddListener(CallShoot);
    }
    private void CallShoot()
    {
        GetEvent(EventKey.Shoot)?.Invoke();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _drag = Vector2.zero;
        _pointerDown=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _drag = eventData.delta;
    }

    public override bool GetInputAction(InputKey key)
    {
        switch (key)
        {
            case InputKey.PointerDown:
                return _pointerDown;
            default:
                return false;
        }
    }

    public override Vector2 GetInputAxis(AxisKey key)
    {
        switch (key)
        {
            case AxisKey.Swipe:
                return _drag;
            default:
                return Vector2.zero;
        }
    }

    public override Vector2 GetInputAxisRaw(AxisKey key)
    {
        switch (key)
        {
            case AxisKey.Swipe:
                return _drag.normalized;
            default:
                return Vector2.zero;
        }
    }
}
