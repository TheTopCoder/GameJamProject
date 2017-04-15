using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CreditsScript : MonoBehaviour
{
    public void OnBackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
