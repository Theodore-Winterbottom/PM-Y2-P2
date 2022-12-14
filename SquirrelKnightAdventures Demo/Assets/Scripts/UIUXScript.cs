using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIUXScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    [SerializeField] private GameObject SettingsMenu;

    [SerializeField] private LevelLoader LevelLoader;

    private bool GameIsPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } 
            else
            {
                LoadPauseMenu();
            }     
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (!GameIsPaused)
            {
                RestartGame();
            }
        }
    }
    public void LoadPauseMenu()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    public void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    public void RestartGame()
    {
        LevelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }
    public void LoadSettings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
