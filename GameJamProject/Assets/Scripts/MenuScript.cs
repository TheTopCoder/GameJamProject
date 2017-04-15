using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    Canvas exitCanvas;
    [SerializeField]
    GameObject EventSytem;
    [SerializeField]
    GameObject yesButton;
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    GameObject creditsButton;
    [SerializeField]
    GameObject exitButton;

    Canvas thisCanvas;
    void Awake()
    {
        exitCanvas.enabled = false;
    }
    public void OnPlay()
    {
        SceneManager.LoadScene("Playground");
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OnExit()
    {
        exitCanvas.enabled = true;
        this.GetComponent<Canvas>().enabled = false;
        playButton.GetComponent<Button>().enabled = false;
        creditsButton.GetComponent<Button>().enabled = false;
        exitButton.GetComponent<Button>().enabled = false;
        EventSytem.GetComponent<EventSystem>().SetSelectedGameObject(yesButton);
    }

    public void OnYes()
    {
        Application.Quit();
    }

    public void OnNo()
    {
        exitCanvas.enabled = false;
        this.GetComponent<Canvas>().enabled = true;
        playButton.GetComponent<Button>().enabled = true;
        creditsButton.GetComponent<Button>().enabled = true;
        exitButton.GetComponent<Button>().enabled = true;
        EventSytem.GetComponent<EventSystem>().SetSelectedGameObject(playButton);
    }

    
}
