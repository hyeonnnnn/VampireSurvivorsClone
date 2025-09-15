using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] Image _background;
    [SerializeField] Image _handler;

    private Vector2 _touchPos;
    private Vector2 _moveDir;
    private float _joystickRadius;

    void Start()
    {
        _joystickRadius = _background.rectTransform.sizeDelta.y / 2;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPos = eventData.position;
        _background.transform.position = _touchPos;
        _handler.transform.position = _touchPos;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handler.transform.position = _touchPos;
        _moveDir = Vector2.zero;

        Managers.Game.MoveDir = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchDir = eventData.position - _touchPos;
        float dist = Mathf.Min(touchDir.magnitude, _joystickRadius);

        _moveDir = touchDir.normalized;
        _handler.transform.position = _touchPos + (_moveDir * dist);

        Managers.Game.MoveDir = _moveDir;
    }

}
