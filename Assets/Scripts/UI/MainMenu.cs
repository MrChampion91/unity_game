using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    private string playScene = "MainMenu";  // Задайте номер сцены для загрузки, когда нажмете кнопку "Играть"

    public GameObject dropdownMenu; //высплывающее меню 
    public GameObject dropdownMenu2; //высплывающее меню 
    public AudioMixer audioMixer;   //весь аудиомиксер для регулирования уровнем громкости
    public Slider volumeSlider; //slider для контроля громкости

    private float currentVolume;    
    private AudioListener audioListener;//ухо на камере

    private static SaveGame instance;

    void Start()
    {
        //ухо на камере
        audioListener = Camera.main.GetComponent<AudioListener>();

        if (!audioMixer)
        {
            return;
            // Получаем текущее значение уровня громкости из AudioMixer
            //float currentVolume;
            bool result = audioMixer.GetFloat("MyExposedParam", out currentVolume);

            // Устанавливаем значение слайдера
            volumeSlider.value = currentVolume;
        }

    }

    public void SelectLevel1()// Вызывается, когда пользователь нажимает кнопку "Играть"
    {
        playScene = "SandBox";
        OnSelectLevelButtonClicked();
    }
    public void SelectLevel2()// Вызывается, когда пользователь нажимает кнопку "Играть"
    {
        playScene = "IronSourceDemo";
        OnSelectLevelButtonClicked();
    }

    public void OnPlayButtonClicked()// Вызывается, когда пользователь нажимает кнопку "Играть"
    {
        // Загрузить сцену с заданным индексом
        SceneManager.LoadScene(playScene);
    }


    public void OnMainMenuButtonClicked()// Вызывается, когда пользователь нажимает кнопку "глав меню"
    {
        // Загрузить сцену с заданным индексом
        Time.timeScale = 1f;
        //проверка есть ли кошелек
        
            SaveGame.Instance.UpdateScore(Wallet.coins);
        
        SceneManager.LoadScene("MainMenu");
    }

    // Вызывается, когда пользователь нажимает кнопку "Выход"
    public void OnQuitButtonClicked()
    {
        // Выйти из игры
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnOptionsButtonClicked()//открытие 1 всплывающего меню
    {
        if (dropdownMenu.activeSelf)
        {
            dropdownMenu.SetActive(false);
            return;
        }
        
        else
        {
            dropdownMenu.SetActive(true);
            return;
        }

    }

    public void OnSelectLevelButtonClicked()//открытие выбора уровня
    {
        StartCoroutine(OpenLevelDropdown());
    }
    private IEnumerator OpenLevelDropdown()
    {
        if (dropdownMenu2.activeSelf)
        {
            yield return new WaitForSeconds(0.0f);
            dropdownMenu2.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            dropdownMenu2.SetActive(true);
        }
    }

    private void WaitForSeconds(float v)
    {
        throw new NotImplementedException();
    }

    public void SetVolume()// Устанавливаем громкость в соответствии со значением слайдера
    {
        audioMixer.SetFloat("MyExposedParam", volumeSlider.value);
        Debug.Log(volumeSlider.value);
    }

    //пауза и продолжить игру.

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        OnOptionsButtonClicked();
        audioListener.enabled = true;
    }
    public void PauseButton()
    {
        if (Time.timeScale == 0f) ResumeGame();
        else PauseGame();
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        OnOptionsButtonClicked();
        audioListener.enabled = false;
    }
}