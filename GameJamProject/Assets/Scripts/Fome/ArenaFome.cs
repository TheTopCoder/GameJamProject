using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaFome : MonoBehaviour {
	bool activated = false;
	GameObject fome;
	// Use this for initialization
	void Start () {
		fome = GameObject.Find ("Fome");
	}

	IEnumerator Flash(){
		Color c;
		c = GetComponent<Image> ().color;
		c.a = 1.0f;
		GetComponent<Image> ().color = c;
		GameObject.Find ("CenarioOriginal").SetActive (false);
//		GameObject.Find ("CenarioFinal").SetActive (false);
		yield return new WaitForSeconds (0.2f);
		c = GetComponent<Image> ().color;
		c.a = 0.0f;
		GetComponent<Image> ().color = c;
	}

	// Update is called once per frame
	void Update () {
		if (!activated&&fome.GetComponent<FomeController> ().life <= fome.GetComponent<FomeController> ().maxLife/2) {
			activated = true;
			StartCoroutine (Flash());
		}
		;
	}
}
