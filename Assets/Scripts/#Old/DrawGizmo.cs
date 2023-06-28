using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class DrawGizmo : MonoBehaviour
{

    [SerializeField] private bool setActive = false;
    [SerializeField] private bool setActiveLine = false;
    [SerializeField] private float lineWidth = 10;

    [SerializeField] private float width = 10;
    [SerializeField] private float height = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log (transform.childCount);
        
    }

    public void OnDrawGizmos()
    {
        if (setActive)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
        }

        if (setActiveLine)
        {
            Vector3 direction = transform.TransformDirection(Vector3.forward) * lineWidth;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position , direction);
        }


    }
}
