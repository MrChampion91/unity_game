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

        /*// Получаем текущее положение мыши
        Vector3 mousePosition = Input.mousePosition;

        // Преобразуем координаты мыши в мировые координаты
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y, gunTransform.position.z - mainCamera.transform.position.z));

        // Вычисляем направление поворота пушки
        Vector3 direction = worldMousePosition - gunTransform.position;
        direction.z = 0f;

        // Выполняем поворот пушки
        gunTransform.up = Vector3.Lerp(gunTransform.up, -direction.normalized, Time.deltaTime * rotationSpeed);*/


        // Получаем направление поворота пушки от джойстика
        Vector3 direction = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);

        // Выполняем поворот пушки
        gunTransform.up = Vector3.Lerp(gunTransform.up, -direction.normalized, Time.deltaTime * rotationSpeed);


        // Обработка стрельбы
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
            // Стреляем
            Shoot();
            Debug.Log("SHOT!");
            // Устанавливаем время следующего выстрела
            nextActionTime = Time.time;
        }
        
    }

    public void Shoot()
    {
        // Создаем новый экземпляр снаряда
        GameObject projectile = Instantiate(projectilePrefab, gunTransform.GetChild(2).position, gunTransform.GetChild(2).rotation, _bulletContainer);

        // Получаем направление выстрела
        Vector3 shootDirection = gunTransform.up;

        // capsela speed correction
        Vector3 capsuleVelocity = capsuleRigidbody.velocity;
        Vector3 correctedVelocity = -shootDirection * projectileSpeed + capsuleVelocity;

        // Применяем скорость снаряда
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = correctedVelocity;

        Destroy(projectile, 3f);

    }
}
