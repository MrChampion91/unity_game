using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxigen : MonoBehaviour
{
    public int oxigen = 3;

    [SerializeField] private float delDistance;
    [SerializeField] private float speed = 1;

    private GameObject capsula;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        speed += Random.Range(0f, 5f);

        capsula = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

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
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y + (speed * Time.deltaTime), currentPosition.z);

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
