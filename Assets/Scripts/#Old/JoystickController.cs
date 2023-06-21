using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // позиция, где был нажат палец на экране
    private Vector2 fingerDownPosition;

    // ссылка на RectTransform джойстика
    public RectTransform joystick;

    // максимальное расстояние, на которое может передвигаться джойстик
    public float joystickRange;

    // текущее направление джойстика в диапазоне от -1 до 1 по каждой оси
    public Vector2 joystickDirection { get; private set; }

    // вызывается при нажатии на экран
    public void OnPointerDown(PointerEventData eventData)
    {
        // сохраняем позицию, где был нажат палец на экране
        fingerDownPosition = eventData.position;
    }

    // вызывается при отпускании экрана
    public void OnPointerUp(PointerEventData eventData)
    {
        // сбрасываем направление джойстика
        joystickDirection = Vector2.zero;
        // перемещаем джойстик обратно в центр
        joystick.anchoredPosition = Vector2.zero;
    }

    // вызывается при перемещении пальца по экрану
    public void OnDrag(PointerEventData eventData)
    {
        // вычисляем смещение от начальной позиции пальца на экране
        Vector2 delta = eventData.position - fingerDownPosition;
        // ограничиваем смещение до максимального расстояния джойстика
        delta = Vector2.ClampMagnitude(delta, joystickRange);

        // устанавливаем новую позицию для джойстика
        joystick.anchoredPosition = delta;

        // вычисляем направление джойстика в диапазоне от -1 до 1 по каждой оси
        joystickDirection = new Vector2(delta.x / joystickRange, delta.y / joystickRange);
    }
}