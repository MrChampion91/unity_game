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
            // ��������� ��� �������������
    }

    public static SaveGame Instance
    {
        get { return instance; }
    }

    private void SaveData()
    {
        // ����������� ������ � JSON-������
        string json = JsonUtility.ToJson(data);
        // ���������� ������ � ����
        Debug.Log("Saved!");
        string filePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log("Save Path: " + filePath);
        Debug.Log("Save Data: " + json);
    }

    // �������� ������ ��� ���������� � ������ ������

    // ������ ������ ��� ���������� �����
    public void UpdateScore(int newScore)
    {
        if (data != null)
        {
            data.coins = newScore;
            // ��������� ������ ����� ������� ����������
            SaveData();
        }
        else
        {
            Debug.LogError("GameData is not initialized!");
        }
    }

    // ������ ������ ��� ������ �����
    private void LoadData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
        // ���������, ���������� �� ���� ����������
        if (System.IO.File.Exists(saveFilePath))
        {
            
            //string saveFilePath = Path.Combine(Application.persistentDataPath, "save_data.json");
            // ��������� ������ �� �����
            string loadedJson = System.IO.File.ReadAllText(saveFilePath);
            data = JsonUtility.FromJson<GameData>(loadedJson);
            Debug.Log("LOADED!");
        }
        else
        {
            // ������� ����� ��������� ������
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