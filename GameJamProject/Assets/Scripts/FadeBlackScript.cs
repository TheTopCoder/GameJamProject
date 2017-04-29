using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeBlackScript : MonoBehaviour {
		[SerializeField]
		Image panel;
	//	public string nome;

		void Start()
		{
			StartCoroutine(LoadScene());
		}

		IEnumerator LoadScene()
		{
			for (int i = 0; i <= 100; i++)
			{
				Color aux = panel.color;
				aux.a += 0.01f;
				panel.color = aux;
				yield return new WaitForSeconds(0.01f);
			}
		}
	}