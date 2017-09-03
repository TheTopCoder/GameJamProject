using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifebarScript : MonoBehaviour {

	GameObject boss;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		if (boss != null) {
			if (boss.name == "Fome") {
				GetComponent<Image> ().fillAmount = (float)boss.GetComponent<FomeController> ().life / boss.GetComponent<FomeController> ().maxLife;
			}
			else if (boss.name == "Tempestade") {
				GetComponent<Image> ().fillAmount = (float)boss.GetComponent<TempestadeController> ().life / boss.GetComponent<TempestadeController> ().maxLife;
			}
		}
		else
			GetComponent<Image> ().fillAmount = 0;
	}
}
