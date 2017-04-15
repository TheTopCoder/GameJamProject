using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    Canvas exitCanvas;
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
    }

    public void OnYes()
    {
        Application.Quit();
    }

    public void OnNo()
    {
        exitCanvas.enabled = false;
        this.GetComponent<Canvas>().enabled = true;
    }

    
}
