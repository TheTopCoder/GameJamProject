using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    AudioSource[] audios;
    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    GameObject resumeButton;
    [SerializeField]
    GameObject eventSystem;
	bool paused;

    float volumeAux;
    bool volumeSelected;

    void Start()
    {
		paused = false;
        this.GetComponent<Canvas>().enabled = false;

		if (PlayerPrefs.GetFloat("MainVolume").Equals(null))
		{
			PlayerPrefs.SetFloat("MainVolume", /*audios[1].volume*/0.5f);
		}
		else if (PlayerPrefs.GetFloat("MainVolume").Equals(0))
		{
			PlayerPrefs.SetFloat("MainVolume", /*audios[1].volume*/0.5f);
		}

        volumeSlider.value = PlayerPrefs.GetFloat("MainVolume");

        foreach (AudioSource a in audios)
        {
            a.volume = volumeSlider.value;
        }

        volumeAux = volumeSlider.value;
    }
    void Update()
    {
		if (Input.GetKeyDown (KeyCode.P) || Input.GetButtonDown ("XboxSelect")) {
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

    }
    public void OnResume()
    {
		paused = false;
        Time.timeScale = 1;
        this.GetComponent<Canvas>().enabled = false;
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
}
