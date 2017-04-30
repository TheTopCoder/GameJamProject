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

    float volumeAux;
    bool volumeSelected;

    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;

        if (PlayerPrefs.GetFloat("MainVolume").Equals(null))
        {
            PlayerPrefs.SetFloat("MainVolume", audios[1].volume);
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
        Debug.Log(volumeAux);
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (Input.GetKey(KeyCode.Escape) || Input.GetButtonDown("XboxSelect"))
        {
            foreach (AudioSource a in audios)
            {
                a.Stop();
            }
            this.GetComponent<Canvas>().enabled = true;
            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(resumeButton);
            Time.timeScale = 0;
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
        Time.timeScale = 1;
        this.GetComponent<Canvas>().enabled = false;
        foreach (AudioSource a in audios)
        {
            a.Play();
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
