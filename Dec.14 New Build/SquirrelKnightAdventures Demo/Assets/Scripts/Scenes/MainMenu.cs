using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject SettingsMenu;
    public void LoadSettings()
    {
        SettingsMenu.SetActive(true);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void BcakButton()
    {
        SceneManager.LoadScene(0);
    }
}
