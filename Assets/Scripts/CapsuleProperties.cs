using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class CapsuleProperties : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxOxygen = 100f;

    private float currentHealth; // Текущее значение прочности
    private float currentOxygen; // Текущее значение кислорода

    private int capsulDeep;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI oxigenText;
    [SerializeField] private TextMeshProUGUI deepText;


    private void OnTriggerEnter(Collider collision)
    {
        // Проверяем, что объект, с которым столкнулись, имеет определенный тег
        if (collision.gameObject.tag == "Oxigen")
        {
            currentOxygen += 1;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth; // Устанавливаем начальное значение прочности
        currentOxygen = maxOxygen; // Устанавливаем начальное значение кислорода

        healthText.text = "healt - " + currentHealth;
        oxigenText.text = "healt - " + currentOxygen;        
    }

    private void FixedUpdate()
    {

        capsulDeep = Mathf.FloorToInt(gameObject.GetComponent<Transform>().position.y);
        deepText.text = "deepText - " + capsulDeep;

        oxigenText.text = "oxigen - " + Mathf.FloorToInt(currentOxygen);
        

        currentOxygen -= Time.fixedDeltaTime; // Уменьшаем значение кислорода каждый кадр
        //Debug.Log(currentOxygen);

        if (currentHealth <= 0f)
        {
            GameOver(); // Если кислород закончился, вызываем функцию конца игры
        }

        if (currentOxygen <= 0f)
        {
            GameOver(); // Если кислород закончился, вызываем функцию конца игры
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy");
        if (collision.gameObject.tag == "Enemy")
        {
            // Если столкнулись с врагом, уменьшаем здоровье игрока
            currentHealth -= collision.gameObject.GetComponent<MonsterBehaviour>().GetDamage();
            // Обновляем значение здоровья на экране
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

        SceneManager.LoadScene("MainMenu"); // Загружаем сцену GameOver при конце игры
    }
}