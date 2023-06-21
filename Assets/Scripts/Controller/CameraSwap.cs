using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    private float screenWidth;
    private float cameraOffset;
    private Vector3 cameraPosition;

    [SerializeField] private GameObject mainCamera;
    
    void Start()
    {
    // Получение текущего разрешения экрана
    screenWidth = Screen.width;

    // Расчет смещения камеры как процент от ширины экрана
    cameraOffset = screenWidth * 0.05f; // Например, смещение на 50% от ширины экрана

    cameraPosition = mainCamera.transform.position;
    cameraPosition.x -= cameraOffset;
    }

    public void Swap()
    {
        //mainCamera.transform.position = cameraPosition;
        StartCoroutine(SmoothCameraTransition());
    }

    public void SwapBack()
    {
        //mainCamera.transform.position = cameraPosition;
        StartCoroutine(SmoothCameraTransition());
    }

    IEnumerator SmoothCameraTransition()
    {
        Vector3 initialPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x - cameraOffset, initialPosition.y, initialPosition.z);
        float elapsedTime = 0f;
        float transitionDuration = 1f; // Длительность перехода в секундах

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }

        // По достижении конечной позиции выполните необходимые действия
    }
}
