using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(SettingsMenu);
        DontDestroyOnLoad(AudioSettings);
        DontDestroyOnLoad(VideoSettings);
    }

    public void FindObjects()
    {
        SettingsMenu = GameObject.Find("SettingsMenu");
        AudioSettings = GameObject.Find("AudioSettings");
        VideoSettings = GameObject.Find("VideoSettings");
    }

    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject AudioSettings;
    [SerializeField] private GameObject VideoSettings;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> fillteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        fillteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();

        for(int i = 0; i < resolutions.Length; i++)
        {
            fillteredResolutions.Add(resolutions[i]);
        }

        List<string> options = new List<string>();
        for(int i = 0; i < fillteredResolutions.Count ;i++)
        {
            string resolutionOption = fillteredResolutions[i].width + "x" + fillteredResolutions[i].height + " " + fillteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (fillteredResolutions[i].width == Screen.width && fillteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex= i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value= currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = fillteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
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
