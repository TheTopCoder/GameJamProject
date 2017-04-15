using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class CreditsScript : MonoBehaviour
{
	[SerializeField]
	EventSystem eventSystem;
	[SerializeField]
	GameObject BackToMenu;
	void Awake()
	{
		eventSystem.SetSelectedGameObject (BackToMenu);
	}
    public void OnBackToMenu()
	{
        SceneManager.LoadScene("MenuScene");
    }
}
