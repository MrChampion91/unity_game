using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

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
            // Остальной код инициализации
    }

    public static SaveGame Instance
    {
        get { return instance; }
    }

    private void SaveData()
    {
        // Преобразуем данные в JSON-строку
        string json = JsonUtility.ToJson(data);
        // Записываем данные в файл
        Debug.Log("Saved!");
        string filePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log("Save Path: " + filePath);
        Debug.Log("Save Data: " + json);
    }

    // Добавьте методы для обновления и чтения данных

    // Пример метода для обновления счета
    public void UpdateScore(int newScore)
    {
        if (data != null)
        {
            data.coins = newScore;
            // Сохраняем данные после каждого обновления
            SaveData();
        }
        else
        {
            Debug.LogError("GameData is not initialized!");
        }
    }

    // Пример метода для чтения счета
    private void LoadData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        // Проверяем, существует ли файл сохранения
        if (System.IO.File.Exists(saveFilePath))
        {
            
            //string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
            // Загружаем данные из файла
            string loadedJson = System.IO.File.ReadAllText(saveFilePath);
            data = JsonUtility.FromJson<GameData>(loadedJson);
            Debug.Log("LOADED!");
        }
        else
        {
            // Создаем новый экземпляр данных
            data = new GameData();
        }
    }

    public int GetScore()
    {
        return data.coins;

    }
    public void DeleteSave()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        System.IO.File.Delete(saveFilePath);
        Instance.data.ResetData();
        SceneManager.LoadScene("MainMenu");
    }
}