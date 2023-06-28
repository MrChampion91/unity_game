using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private static SaveGame instance;
    private GameData data;
    
    private void Awake()
    {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            LoadData();
    }

    public static SaveGame Instance
    {
        get { return instance; }
    }
    public GameData GetSaveData()
    {
        return data;
    }

    public void LoadData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        // check exist save file
        if (System.IO.File.Exists(saveFilePath))
        {
            //string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
            // load file
            string loadedJson = System.IO.File.ReadAllText(saveFilePath);
            data = JsonUtility.FromJson<GameData>(loadedJson);
            Debug.Log("LOADED!");
        }
        else
        {
            // create new load file
            data = new GameData();
        }
    }

    public void SaveData(int coins)
    {
        if (data != null)
        {
            data.coins = coins;

            // Сохраняем данные после каждого обновления
            // Преобразуем данные в JSON-строку
            string json = JsonUtility.ToJson(data);
            // Записываем данные в файл
            Debug.Log("Saved!");
            string filePath = Path.Combine(Application.persistentDataPath, "save_data.json");
            System.IO.File.WriteAllText(filePath, json);

            Debug.Log("Save Path: " + filePath);
            Debug.Log("Save Data: " + json);
        }
        else
        {
            Debug.LogError("GameData is not initialized!");
        }
    }

    public void DeleteData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        System.IO.File.Delete(saveFilePath);
        Instance.data.ResetData();
        SceneManager.LoadScene("MainMenu");
    }
}