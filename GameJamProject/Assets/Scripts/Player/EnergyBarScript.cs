using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour {
	GameObject player;
	PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image> ().fillAmount = ((float)playerStats.energy) / ((float)playerStats.maxEnergy);
	}
}
