using UnityEngine;
using System.Collections;

public class TreasureGenerator : MonoBehaviour
{
    public GameObject[] treasures;
    public float treasureDistance = 10f;
    public float delay = 2f;
    public float speed = 5f;

    private Transform _treasureContainer;

    private void Start()
    {
        // Создаем контейнер для сокровищ
        _treasureContainer = new GameObject("TreasureContainer").transform;
    }

    private void Update()
    {
        // Вычисляем расстояние между капсулой и дном
        float distanceToBottom = transform.position.y;

        // Вычисляем, сколько сокровищ должно быть сгенерировано
        int treasureCount = Mathf.FloorToInt(distanceToBottom / treasureDistance);

        // Если есть хотя бы одно сокровище, начинаем генерацию
        if (treasureCount > 0)
        {
            StartCoroutine(GenerateTreasures(treasureCount));
        }
    }

    private IEnumerator GenerateTreasures(int count)
    {
        // Задержка перед началом генерации сокровищ
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < count; i++)
        {
            // Выбираем случайное сокровище из массива
            int randomIndex = Random.Range(0, treasures.Length);
            GameObject treasurePrefab = treasures[randomIndex];

            // Вычисляем случайную позицию для сокровища
            float randomX = Random.Range(-10f, 10f);
            Vector3 treasurePosition = new Vector3(transform.position.x + randomX, 0f, transform.position.z - i * treasureDistance);

            // Создаем сокровище и помещаем его в контейнер
            GameObject treasure = Instantiate(treasurePrefab, treasurePosition, Quaternion.identity, _treasureContainer);

          
        }
    }
}