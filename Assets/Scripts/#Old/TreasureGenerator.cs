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
        // ������� ��������� ��� ��������
        _treasureContainer = new GameObject("TreasureContainer").transform;
    }

    private void Update()
    {
        // ��������� ���������� ����� �������� � ����
        float distanceToBottom = transform.position.y;

        // ���������, ������� �������� ������ ���� �������������
        int treasureCount = Mathf.FloorToInt(distanceToBottom / treasureDistance);

        // ���� ���� ���� �� ���� ���������, �������� ���������
        if (treasureCount > 0)
        {
            StartCoroutine(GenerateTreasures(treasureCount));
        }
    }

    private IEnumerator GenerateTreasures(int count)
    {
        // �������� ����� ������� ��������� ��������
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < count; i++)
        {
            // �������� ��������� ��������� �� �������
            int randomIndex = Random.Range(0, treasures.Length);
            GameObject treasurePrefab = treasures[randomIndex];

            // ��������� ��������� ������� ��� ���������
            float randomX = Random.Range(-10f, 10f);
            Vector3 treasurePosition = new Vector3(transform.position.x + randomX, 0f, transform.position.z - i * treasureDistance);

            // ������� ��������� � �������� ��� � ���������
            GameObject treasure = Instantiate(treasurePrefab, treasurePosition, Quaternion.identity, _treasureContainer);

          
        }
    }
}