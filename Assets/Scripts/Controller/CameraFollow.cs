using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // объект, за которым должна следовать камера
    [SerializeField] private float distance = 18.0f; // расстояние между камерой и объектом

    [SerializeField] private float height = -5.0f; // высота камеры над объектом
    [SerializeField] private float smoothSpeed = 0.125f; // скорость следования камеры за объектом
    [SerializeField] private Vector3 speedDistance = new Vector3(0, 0, 0);

    private Vector3 targetPosition;

    public VariableJoystick variableJoystick;



    private void FixedUpdate()
    {
        //float vertical = Input.GetAxis("Vertical");
        float vertical = variableJoystick.Vertical;


        // вычисляем позицию, куда должна переместиться камера
        targetPosition = target.position - target.forward * distance + target.up * height; //transform.up вместо таргет up

        //transform.LookAt(target);

        if (vertical != 0)
        {
            // Плавно перемещаем камеру к новой позиции с заданной скоростью.
            transform.position = Vector3.Lerp(transform.position, targetPosition + speedDistance, smoothSpeed * Time.deltaTime);

        }

        else
        {
            // плавно перемещаем камеру к целевой позиции
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            
        }
    }
}