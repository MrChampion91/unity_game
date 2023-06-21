using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    public GameObject container;
    public GameObject generationPoint;

    [SerializeField] private GameObject[] objects = Array.Empty<GameObject>();

    [SerializeField] float range1;
    [SerializeField] float range2;

    private int position = 0;
    private int counter = 0;

    private bool hasCollided = false;


    void FixedUpdate()
    {
        
        if (gameObject.GetComponent<Transform>().position.y < position && !hasCollided)
        {
            position = (int)gameObject.GetComponent<Transform>().position.y;
            if (position % 100 == 0)
            {
                Debug.Log("generated!" + (counter += 10));

                for (int i = 1; i <= 10; i++)
                {
                    float a = Random.Range(range1, range2);
                    float b = Random.Range(range1, range2);

                    CreateObject(objects[Random.Range(0, objects.Length)],
                        generationPoint.transform.position + new Vector3(a, b, Random.Range(-0.5f, 0.5f)),
                        Quaternion.Euler(90, 0, 0), container.GetComponent<Transform>());
                }

            }

            hasCollided = true;

        }
        else 
        {
            hasCollided = false;
        }
        
    }



    private static void CreateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject newObject = Instantiate(prefab, position, rotation);

    }

    private static void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Deliter")
        {

            Debug.Log(collision);
            //Destroy(collision.gameObject);
        }
    }
}
