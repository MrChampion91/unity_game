using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    private string playScene = "MainMenu";  // start scene

    public GameObject dropdownMenu;
    public GameObject dropdownMenu2;

    public AudioMixer audioMixer;//mixer
    public Slider volumeSlider; //slider for volume

    private float currentVolume; 

    private AudioListener audioListener;//camera ear for teke it off in pause
    private static SaveGame instance;   //for saveing game

    void Start()
    {
        audioListener = Camera.main.GetComponent<AudioListener>();
        if (!audioMixer)
        {
            return;
            //float currentVolume;
            bool result = audioMixer.GetFloat("MyExposedParam", out currentVolume);

            volumeSlider.value = currentVolume;
        }
    }

    public void SelectLevel1()
    {
        playScene = "SandBox";
        SelectLevelButton();
    }
    public void SelectLevel2()
    {
        playScene = "SandBox";
        SelectLevelButton();
    }
    public void SelectLevel3()
    {
        playScene = "SandBox";
        SelectLevelButton();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(playScene);
    }
    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        //TODO save game
        //SaveGame.Instance.SaveData(Wallet.coins);
        SceneManager.LoadScene("MainMenu");
    }

    public void OptionsButton()
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

    public void SelectLevelButton()
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

    public void SetVolume()
    {
        audioMixer.SetFloat("MyExposedParam", volumeSlider.value);
        Debug.Log(volumeSlider.value);
    }

    //pause and continue game
    public void PauseButton()
    {
        if (Time.timeScale == 0f) ResumeGame();
        else PauseGame();
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        OptionsButton();
        audioListener.enabled = false;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        OptionsButton();
        audioListener.enabled = true;
    }
}