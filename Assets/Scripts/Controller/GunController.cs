using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{
    
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float rotationSpeed = 5f;

    public Joystick joystick;

    public float shootInterval = 0.2f;
    private float nextActionTime = 0.0f;

    private Transform gunTransform;
    private Transform _bulletContainer;
    private Camera mainCamera;
    private bool isShooting;
    private Rigidbody capsuleRigidbody;

    private void Awake()
    {
        _bulletContainer = new GameObject("_bulletContainer").transform; //contener for bullet
        
        capsuleRigidbody = transform.parent.GetComponent<Rigidbody>(); //capsul rb for bullet speed correction
        gunTransform = transform;
        //mainCamera = Camera.main;

    }
    private void Update()
    {

        /*// �������� ������� ��������� ����
        Vector3 mousePosition = Input.mousePosition;

        // ����������� ���������� ���� � ������� ����������
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y, gunTransform.position.z - mainCamera.transform.position.z));

        // ��������� ����������� �������� �����
        Vector3 direction = worldMousePosition - gunTransform.position;
        direction.z = 0f;

        // ��������� ������� �����
        gunTransform.up = Vector3.Lerp(gunTransform.up, -direction.normalized, Time.deltaTime * rotationSpeed);*/


        // �������� ����������� �������� ����� �� ���������
        Vector3 direction = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        // ��������� ������� �����
        gunTransform.up = Vector3.Lerp(gunTransform.up, -direction.normalized, Time.deltaTime * rotationSpeed);


        // ��������� ��������
        if (joystick.Horizontal != 0.0f || joystick.Vertical != 0.0f)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        if (isShooting && Time.time - nextActionTime > shootInterval)
        {
            // ��������
            Shoot();
            Debug.Log("SHOT!");
            // ������������� ����� ���������� ��������
            nextActionTime = Time.time;
        }
        
    }

    public void Shoot()
    {
        // ������� ����� ��������� �������
        GameObject projectile = Instantiate(projectilePrefab, gunTransform.GetChild(2).position, gunTransform.GetChild(2).rotation, _bulletContainer);

        // �������� ����������� ��������
        Vector3 shootDirection = gunTransform.up;

        // capsela speed correction
        Vector3 capsuleVelocity = capsuleRigidbody.velocity;
        Vector3 correctedVelocity = -shootDirection * projectileSpeed + capsuleVelocity;

        // ��������� �������� �������
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = correctedVelocity;

        Destroy(projectile, 3f);

    }
}
