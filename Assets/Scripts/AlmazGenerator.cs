using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlmazGenerator : MonoBehaviour
{
    public GameObject Container;
    public GameObject GenerationPoint;

    [SerializeField]
    private GameObject[] Objects = Array.Empty<GameObject>();

    [SerializeField]
    private float Range1;

    [SerializeField]
    private float Range2;

    private float _lastCheckedDepth = 0;
    private int _counter;

    [SerializeField] private int deepGen = 100;
    [SerializeField] private int tresureCount = 10;

    private void Update()
    {
        var currentDepth = gameObject.transform.position.y;
        if (currentDepth >= _lastCheckedDepth)
            return;

        if ((int)Math.Ceiling(Math.Abs(currentDepth - _lastCheckedDepth)) % deepGen != 0)
            return;

        _lastCheckedDepth = currentDepth;

        Debug.Log($"generated! {_counter+=10}");

        for (var i = 1; i <= tresureCount; i++)
        {
            var a = Random.Range(Range1, Range2);
            var b = Random.Range(Range1, Range2);

            CreateObject(Objects[Random.Range(0, Objects.Length)],
                GenerationPoint.transform.position + new Vector3(a, b, Random.Range(-0.5f, 0.5f)),
                Quaternion.Euler(0, 0, 0));
        }
    }

/*    private void OnTriggerExit(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Deliter"))
            return;

        Debug.Log("DELITER");
        //Destroy(collision.gameObject);
    }*/

    private static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation) =>
        Instantiate(prefab, position, rotation);
}