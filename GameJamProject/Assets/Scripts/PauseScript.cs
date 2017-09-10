using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PauseScript : MonoBehaviour
{
    #region Variables
        [SerializeField]
        AudioSource[] audios;
        [SerializeField]
        Slider volumeSlider;
        [SerializeField]
        GameObject resumeButton;
        [SerializeField]
        GameObject eventSystem;
        [SerializeField]
        GameObject controlsPanel;
        [SerializeField]
        GameObject optionsPanel;
        [SerializeField]
        GameObject mainPanel;
        [SerializeField]
        Dropdown resolutionDropdown;
        [SerializeField]
        Dropdown qualityDropdown;
        [SerializeField]
        Toggle vSyncToggle;
        [SerializeField]
        Toggle antiAlisingToggle;
        [SerializeField]
        Toggle windowedToggle;
   

    bool paused;

    float volumeAux;
    bool volumeSelected;
    #endregion

    void Start()
    {
		paused = false;
        this.GetComponent<Canvas>().enabled = false;
		if (SceneManager.GetActiveScene().name == "MenuScene"){
			Debug.Log ("MenuSceneSound");
			PlayerPrefs.SetFloat("MainVolume", 0.5f);
		}
		if (PlayerPrefs.GetFloat("MainVolume").Equals(null))
		{
			Debug.Log ("MainVolume null");
			PlayerPrefs.SetFloat("MainVolume", 0.5f);
		}
		PlayerPrefs.Save ();
        volumeSlider.value = PlayerPrefs.GetFloat("MainVolume");

        foreach (AudioSource a in audios)
        {
            a.volume = volumeSlider.value;
        }
		 
        volumeAux = volumeSlider.value;
    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("XboxSelect")) {
			if (!paused) {
				paused = true;
				foreach (AudioSource a in audios) {
					a.Pause ();
				}
				this.GetComponent<Canvas> ().enabled = true;
				eventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (resumeButton);
				Time.timeScale = 0;
			} else {
				OnResume ();
			}
		}

        if (volumeSelected)
        {
            if (volumeAux <= 1 || volumeAux >= 0)
            {
                volumeAux += Input.GetAxisRaw("Horizontal") / 100;
            }
            volumeSlider.value = volumeAux;
        }

        qualityDropdown.value = QualitySettings.GetQualityLevel();
        vSyncToggle.isOn = Convert.ToBoolean(QualitySettings.vSyncCount);
        antiAlisingToggle.isOn = Convert.ToBoolean(QualitySettings.antiAliasing);
        windowedToggle.isOn = !Screen.fullScreen;
        if (Screen.width == 1920 && Screen.height == 1080)
        {
            resolutionDropdown.value = 0;
        }
        else if (Screen.width == 1680 && Screen.height == 1050)
        {
            resolutionDropdown.value = 1;
        }
        else if (Screen.width == 1600 && Screen.height == 900)
        {
            resolutionDropdown.value = 2;
        }
        else if (Screen.width == 1440 && Screen.height == 900)
        {
            resolutionDropdown.value = 3;
        }
        else if (Screen.width == 1366 && Screen.height == 768)
        {
            resolutionDropdown.value = 4;
        }
        else if (Screen.width == 1280 && Screen.height == 1024)
        {
            resolutionDropdown.value = 5;
        }
        else if (Screen.width == 1024 && Screen.height == 768)
        {
            resolutionDropdown.value = 6;
        }

    }

    public void OnResume()
    {
		paused = false;
        Time.timeScale = 1;
        this.GetComponent<Canvas>().enabled = false;
		PlayerPrefs.SetFloat ("MainVolume",volumeSlider.value);
		PlayerPrefs.Save ();
        foreach (AudioSource a in audios)
        {
//            a.Play();
			a.UnPause();
        }
    }

    public void OnBackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void OnVolumeSliderChanged()
    {
        foreach (AudioSource a in audios)
        {
            a.volume = volumeSlider.value;
        }
    }

    public void OnVolumeSelected()
    {
        volumeSelected = true;
    }

    public void OnVolumeDeselected()
    {
        volumeSelected = false;
    }

    public void OnOptionsSelected()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnControlsSelected()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OnBackToPauseSelected(GameObject panel)
    {
        mainPanel.SetActive(true);
        panel.SetActive(false);
    }

    public void OnToggleValueChanged(Toggle toggle)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OnVSyncToggleValueChanged(Toggle toggle)
    {
        QualitySettings.vSyncCount = Convert.ToInt32(toggle.isOn);
    }

    public void OnAntiAlisingToggleChanged(Toggle toggle)
    {
        QualitySettings.antiAliasing = Convert.ToInt32(toggle.isOn);
    }

    public void OnDropdownValueChanged(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1440, 900, Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                break;
            case 5:
                Screen.SetResolution(1280, 1024, Screen.fullScreen);
                break;
            case 6:
                Screen.SetResolution(1024, 768, Screen.fullScreen);
                break;
        }
    }

    public void OnQualityDropdownValueChanged(Dropdown dropdown)
    {
        QualitySettings.SetQualityLevel(dropdown.value);
    }
}
