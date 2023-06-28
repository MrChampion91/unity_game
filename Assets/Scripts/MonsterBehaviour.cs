using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterBehaviour : MonoBehaviour
{
    private GameObject capsula;
    private Rigidbody rb;
    private float distance;
    private Vector3 direction;

    [SerializeField] private float pushForce=4f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int health = 2;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        capsula = GameObject.FindWithTag("Player");
        rb = capsula.GetComponent<Rigidbody>();

        animator.SetFloat("random", Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Swim();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Bite(collision);
        }
    }

    private void Swim()
    {
        // find direction to capsula
        direction = (capsula.transform.position - transform.position).normalized;
        distance = Vector3.Distance(transform.position, capsula.transform.position);

        // finde new position
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        if (distance >= 1)//
        {
            // move object to capsula
            transform.position = newPosition;
        }
        animator.SetBool("swim", true);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        //transform.LookAt(capsula.transform.position);
    }
    private void Damage()
    {
        health--;
        if (health <= 0)
        {
            //TODO add monster death animation
            Destroy(gameObject);
        }
    }
    private void Bite(Collision collision) 
    {   //bite and push capsula
        animator.SetBool("bite", true);
        Vector3 pushDirection = collision.transform.position - transform.position;
        rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("bite", false);
    }

    public int GetDamage()
    {
        return damage;
    }
}