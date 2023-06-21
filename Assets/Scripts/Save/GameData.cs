using UnityEngine;

public class GameData
{
    public int coins;
    public int almaz;

    public string playerName;
    public string maxDeep;
    public string lastLevel;

    public int color;

    // Добавьте другие необходимые поля для сохранения

    public void ResetData()
    {
        coins = 0;
        almaz = 0;
        playerName = "player";
        maxDeep = "0";
        color = 0;
        lastLevel = "Scene1";
    }
}