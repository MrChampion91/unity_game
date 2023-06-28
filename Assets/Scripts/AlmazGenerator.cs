using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlmazGenerator : MonoBehaviour
{
    public GameObject Container;
    public GameObject GenerationPoint;

    [SerializeField] private GameObject[] Objects = Array.Empty<GameObject>();
    [SerializeField] private float Range1;
    [SerializeField] private float Range2;

    private float _lastCheckedDepth = 0;
    private float currentDepth;

    [SerializeField] private int deepGen = 100;
    [SerializeField] private int tresureCount = 10;

    private void Update()
    {
        CheckMaxDeep();
        Generator();
    }

    private void CheckMaxDeep()
    {
        currentDepth = gameObject.transform.position.y;
        if (currentDepth >= _lastCheckedDepth)
            return;

        if ((int)Math.Ceiling(Math.Abs(currentDepth - _lastCheckedDepth)) % deepGen != 0)
            return;
        _lastCheckedDepth = currentDepth;
    }
    private void Generator()
    {
        for (var i = 1; i <= tresureCount; i++)
        {
            var a = Random.Range(Range1, Range2);
            var b = Random.Range(Range1, Range2);

            CreateObject(Objects[Random.Range(0, Objects.Length)],
                GenerationPoint.transform.position + new Vector3(a, b, Random.Range(-0.5f, 0.5f)),
                Quaternion.Euler(0, 0, 0));
        }
    }

    private GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }
}