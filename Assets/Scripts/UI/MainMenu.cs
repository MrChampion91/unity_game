using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    private string playScene = "MainMenu";  // ������� ����� ����� ��� ��������, ����� ������� ������ "������"

    public GameObject dropdownMenu; //������������ ���� 
    public GameObject dropdownMenu2; //������������ ���� 2

    public AudioMixer audioMixer;   //���� ����������� ��� ������������� ������� ���������
    public Slider volumeSlider; //slider ��� �������� ���������

    private float currentVolume;    
    private AudioListener audioListener;//��� �� ������

    private static SaveGame instance;

    void Start()
    {
        //��� �� ������
        audioListener = Camera.main.GetComponent<AudioListener>();

        if (!audioMixer)
        {
            return;
            // �������� ������� �������� ������ ��������� �� AudioMixer
            //float currentVolume;
            bool result = audioMixer.GetFloat("MyExposedParam", out currentVolume);

            // ������������� �������� ��������
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

    public void PlayButton()
    {
        // ��������� ����� � �������� ��������
        SceneManager.LoadScene(playScene);
    }


    public void MainMenuButton()// ����������, ����� ������������ �������� ������ "���� ����"
    {
        // ��������� ����� � �������� ��������
        Time.timeScale = 1f;
        //�������� ���� �� �������

        //TODO save game
        //SaveGame.Instance.SaveData(Wallet.coins);

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OptionsButton()//�������� 1 ������������ ����
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