using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject AudioSettings;
    [SerializeField] private GameObject VideoSettings;
    public void LoadSettings()
    {
        SettingsMenu.SetActive(true);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void BcakButton()
    {
        SettingsMenu.SetActive(false);
    }

    public void AudioExit()
    {
        AudioSettings.SetActive(false);
    }

    public void VideoExit()
    {
        VideoSettings.SetActive(false);
    }

    public void Audio()
    {
        AudioSettings.SetActive(true);
    }

    public void Video()
    {
        VideoSettings.SetActive(true);
    }
}
