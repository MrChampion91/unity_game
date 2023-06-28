using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class CapsuleProperties : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxOxygen = 100f;

    private float currentHealth; // ������� �������� ���������
    private float currentOxygen; // ������� �������� ���������

    private int capsulDeep;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI oxigenText;
    [SerializeField] private TextMeshProUGUI deepText;


    private void OnTriggerEnter(Collider collision)
    {
        // ���������, ��� ������, � ������� �����������, ����� ������������ ���
        if (collision.gameObject.tag == "Oxigen")
        {
            currentOxygen += 1;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth; // ������������� ��������� �������� ���������
        currentOxygen = maxOxygen; // ������������� ��������� �������� ���������

        healthText.text = "healt - " + currentHealth;
        oxigenText.text = "healt - " + currentOxygen;        
    }

    private void FixedUpdate()
    {

        capsulDeep = Mathf.FloorToInt(gameObject.GetComponent<Transform>().position.y);
        deepText.text = "deepText - " + capsulDeep;

        oxigenText.text = "oxigen - " + Mathf.FloorToInt(currentOxygen);
        

        currentOxygen -= Time.fixedDeltaTime; // ��������� �������� ��������� ������ ����
        //Debug.Log(currentOxygen);

        if (currentHealth <= 0f)
        {
            GameOver(); // ���� �������� ����������, �������� ������� ����� ����
        }

        if (currentOxygen <= 0f)
        {
            GameOver(); // ���� �������� ����������, �������� ������� ����� ����
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy");
        if (collision.gameObject.tag == "Enemy")
        {
            // ���� ����������� � ������, ��������� �������� ������
            currentHealth -= collision.gameObject.GetComponent<MonsterBehaviour>().GetDamage();
            // ��������� �������� �������� �� ������
            healthText.text = "healt - " + currentHealth;
        }
    }
    public void LessOxigen()
    {
        currentOxygen -= 1;
    }

    private void GameOver()
    {
        if (SaveGame.Instance != null && SaveGame.Instance.GetType().GetMethod("UpdateScore") != null)
        {
            //TODO save game
            //SaveGame.Instance.SaveData(Wallet.GetCoins();
           
        }

        SceneManager.LoadScene("MainMenu"); // ��������� ����� GameOver ��� ����� ����
    }
}