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
		if (boss != null)
			GetComponent<Image> ().fillAmount = (float)boss.GetComponent<FomeController> ().life / boss.GetComponent<FomeController> ().maxLife;
		else
			GetComponent<Image> ().fillAmount = 0;
	}
}
