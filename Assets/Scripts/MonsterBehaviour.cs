using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterBehaviour : MonoBehaviour
{
    private GameObject capsula;
    private Rigidbody rb;
    private float distance; // дистанци€ между капсулой и врагом
    private Vector3 direction;//направление

    [SerializeField] private float pushForce=4f;   //сила с которой толкает монстр
    [SerializeField] private float enemyDistance = 1f; // дистанци€ остановки
    public int damage = 1;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int health = 2;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        //gameObject.transform.Rotate(0f, 180f, 0f);
        capsula = GameObject.FindWithTag("Player");

        rb = capsula.GetComponent<Rigidbody>(); // берем капсулу дл€ толчка

        animator.SetFloat("random", Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // ¬ычисл€ем направление, в котором нужно двигатьс€
        direction = (capsula.transform.position - transform.position).normalized;

        distance = Vector3.Distance(transform.position, capsula.transform.position);

        // ¬ычисл€ем новую позицию, к которой нужно двигатьс€
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        if (distance >= enemyDistance)
        {
            // ƒвигаем объект к новой позиции
            transform.position = newPosition;
        }

        animator.SetBool("swim", true);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        //transform.LookAt(capsula.transform.position);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("bite", true);
            Vector3 pushDirection = collision.transform.position - transform.position;
            rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("bite", false);
    }
}