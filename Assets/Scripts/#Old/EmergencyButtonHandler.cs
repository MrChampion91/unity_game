using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmergencyButtonHandler : MonoBehaviour
{
    public Button emergencyButton;
    public GameObject capsule;

    public float surfaceLevel = 0f; // Уровень поверхности
    public float ascentSpeed = 1f; // Скорость всплытия капсулы

    private bool isAscending = false; // Флаг подъема капсулы
    private Vector3 targetPosition; // Конечная позиция для перемещения капсулы

    private Rigidbody rb;// для всплытия


    private void Start()
    {
        rb = capsule.GetComponent<Rigidbody>();
    }
    public void OnClickButton()
    {
        StartCoroutine(RiseCapsule());

    }

    private IEnumerator RiseCapsule()
    {
        float t = 0f;
        Vector3 startVelocity = rb.velocity;
        Vector3 targetVelocity = -startVelocity;
        while (t < 1f)
        {
            rb.velocity = Vector3.Lerp(startVelocity, targetVelocity, t);
            t += Time.deltaTime / ascentSpeed; // riseTime - время всплытия
            yield return null;
        }
        rb.velocity = targetVelocity;
    }
}
