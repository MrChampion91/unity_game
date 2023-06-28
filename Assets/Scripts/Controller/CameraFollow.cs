using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; // transform of capsula player
    [SerializeField] private float distance = 18.0f; // distance bitwine player
    [SerializeField] private float height = -5.0f; // high above player
    [SerializeField] private float smoothSpeed = 0.125f; // follow speed
    [SerializeField] private Vector3 speedDistance = new Vector3(0, 0, 0);

    private Vector3 targetPosition;
    public VariableJoystick variableJoystick;

    public void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        FollowToPlayer();
    }

    private void FollowToPlayer() 
    {
        //float vertical = Input.GetAxis("Vertical");
        float vertical = variableJoystick.Vertical;
        targetPosition = target.position - target.forward * distance + target.up * height;
        //transform.LookAt(target);

        if (vertical != 0)
        {
            // make camera farr with high speed
            transform.position = Vector3.Lerp(transform.position, targetPosition + speedDistance, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // normal speed without velocity
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}