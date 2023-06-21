using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ������, �� ������� ������ ��������� ������
    [SerializeField] private float distance = 18.0f; // ���������� ����� ������� � ��������

    [SerializeField] private float height = -5.0f; // ������ ������ ��� ��������
    [SerializeField] private float smoothSpeed = 0.125f; // �������� ���������� ������ �� ��������
    [SerializeField] private Vector3 speedDistance = new Vector3(0, 0, 0);

    private Vector3 targetPosition;

    public VariableJoystick variableJoystick;



    private void FixedUpdate()
    {
        //float vertical = Input.GetAxis("Vertical");
        float vertical = variableJoystick.Vertical;


        // ��������� �������, ���� ������ ������������� ������
        targetPosition = target.position - target.forward * distance + target.up * height; //transform.up ������ ������ up

        //transform.LookAt(target);

        if (vertical != 0)
        {
            // ������ ���������� ������ � ����� ������� � �������� ���������.
            transform.position = Vector3.Lerp(transform.position, targetPosition + speedDistance, smoothSpeed * Time.deltaTime);

        }

        else
        {
            // ������ ���������� ������ � ������� �������
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            
        }
    }
}