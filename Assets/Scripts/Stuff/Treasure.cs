using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Treasure : MonoBehaviour
{

    public int coins = 1;
    [SerializeField] private float rotator;
    [SerializeField] private float delDistance;
    [SerializeField] private float speed = 1;

    private GameObject capsula;
    private float distance;

    private void Start()
    {
        speed += Random.Range(0f, 1f);

        transform.rotation = Quaternion.Euler(Vector3.one * Random.Range(0f, 360f));

        capsula = GameObject.FindWithTag("Player");
    }
    void Update()
    {

        transform.Rotate(Vector3.one * rotator * Time.deltaTime);
        //transform.Rotate(0f, Time.deltaTime * rotator, 0f);

        Vector3 direction = (capsula.transform.position - transform.position).normalized;
        distance = Vector3.Distance(transform.position, capsula.transform.position);

        if (distance >= delDistance)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        // Получаем текущую позицию объекта
        Vector3 currentPosition = transform.position;

        // Вычисляем новую позицию, перемещая ее по глобальной оси Y
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y - (speed * Time.deltaTime), currentPosition.z);

        // Устанавливаем новую позицию объекта
        transform.position = newPosition;
    }
        private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
