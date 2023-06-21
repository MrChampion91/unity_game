//using System.Collections;
//using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wallet : MonoBehaviour
{

    public static int coins;
    [SerializeField] private TextMeshProUGUI treasureText;

    private SaveGame saveGameInstance; //������ �� ���������
    void Start()
    {
        saveGameInstance = SaveGame.Instance;
        coins = loadCoins();
        treasureText.text = "Coins: " + Wallet.coins.ToString(); // ���������� ���������� �������
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Treasure")
        {
            coins += collision.gameObject.GetComponent<Treasure>().coins;
            treasureText.text = "Coins: " + Wallet.coins.ToString(); // ���������� ���������� �������
        }
    }

    public int loadCoins() //�������� ��������
    {
        int score = saveGameInstance.GetScore();
        return score;
    }

    public void clear()
    {
    treasureText.text = "Coins: " + Wallet.coins.ToString();
    }
}
