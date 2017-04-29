using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour {
	[SerializeField]
	Image panel;
	public string nextScene="";
	//	public string nome;

	void Start()
	{
		//nextScene = "";
	}

	void Update()
	{
		if (nextScene != "") {
			StartCoroutine (LoadScene (nextScene));
			nextScene = "";
		}
	}

	IEnumerator LoadScene(string nextScene)
	{
		for (int i = 0; i <= 100; i++)
		{
			Color aux = panel.color;
			aux.a += 0.01f;
			panel.color = aux;
			yield return new WaitForSeconds(0.01f);
		}
		SceneManager.LoadScene (nextScene);
	}
}