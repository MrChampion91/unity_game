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
    public GameObject dropdownMenu2; //������������ ���� 
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

    public void SelectLevel1()// ����������, ����� ������������ �������� ������ "������"
    {
        playScene = "SandBox";
        OnSelectLevelButtonClicked();
    }
    public void SelectLevel2()// ����������, ����� ������������ �������� ������ "������"
    {
        playScene = "IronSourceDemo";
        OnSelectLevelButtonClicked();
    }

    public void OnPlayButtonClicked()// ����������, ����� ������������ �������� ������ "������"
    {
        // ��������� ����� � �������� ��������
        SceneManager.LoadScene(playScene);
    }


    public void OnMainMenuButtonClicked()// ����������, ����� ������������ �������� ������ "���� ����"
    {
        // ��������� ����� � �������� ��������
        Time.timeScale = 1f;
        //�������� ���� �� �������
        
            SaveGame.Instance.UpdateScore(Wallet.coins);
        
        SceneManager.LoadScene("MainMenu");
    }

    // ����������, ����� ������������ �������� ������ "�����"
    public void OnQuitButtonClicked()
    {
        // ����� �� ����
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnOptionsButtonClicked()//�������� 1 ������������ ����
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

    public void OnSelectLevelButtonClicked()//�������� ������ ������
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

    public void SetVolume()// ������������� ��������� � ������������ �� ��������� ��������
    {
        audioMixer.SetFloat("MyExposedParam", volumeSlider.value);
        Debug.Log(volumeSlider.value);
    }

    //����� � ���������� ����.

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