using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // �������, ��� ��� ����� ����� �� ������
    private Vector2 fingerDownPosition;

    // ������ �� RectTransform ���������
    public RectTransform joystick;

    // ������������ ����������, �� ������� ����� ������������� ��������
    public float joystickRange;

    // ������� ����������� ��������� � ��������� �� -1 �� 1 �� ������ ���
    public Vector2 joystickDirection { get; private set; }

    // ���������� ��� ������� �� �����
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��������� �������, ��� ��� ����� ����� �� ������
        fingerDownPosition = eventData.position;
    }

    // ���������� ��� ���������� ������
    public void OnPointerUp(PointerEventData eventData)
    {
        // ���������� ����������� ���������
        joystickDirection = Vector2.zero;
        // ���������� �������� ������� � �����
        joystick.anchoredPosition = Vector2.zero;
    }

    // ���������� ��� ����������� ������ �� ������
    public void OnDrag(PointerEventData eventData)
    {
        // ��������� �������� �� ��������� ������� ������ �� ������
        Vector2 delta = eventData.position - fingerDownPosition;
        // ������������ �������� �� ������������� ���������� ���������
        delta = Vector2.ClampMagnitude(delta, joystickRange);

        // ������������� ����� ������� ��� ���������
        joystick.anchoredPosition = delta;

        // ��������� ����������� ��������� � ��������� �� -1 �� 1 �� ������ ���
        joystickDirection = new Vector2(delta.x / joystickRange, delta.y / joystickRange);
    }
}