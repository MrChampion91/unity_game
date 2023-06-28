using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int coins;
    //private int almaz;

    [SerializeField] private TextMeshProUGUI coinsText;
    //[SerializeField] private TextMeshProUGUI almazText;

    void Start()
    {
        coins = loadCoins();
        coinsText.text = "Coins: " + coins.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Treasure")
        {
            coins += collision.gameObject.GetComponent<Treasure>().coins;
            coinsText.text = "Coins: " + coins.ToString();
        }
    }

    private int loadCoins() //load from file
    {
        int score = SaveGame.Instance.GetSaveData().coins;
        return score;
    }
    public int GetCoins()
    {
        return coins;
    }
}