using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void SummonCorvo(){
		UnityEngine.Object corvoPrefab = Resources.Load("SkillCorvo");
		GameObject corvo = (GameObject)Instantiate (corvoPrefab,transform.position,Quaternion.identity);
	}

	public void SummonRaio(){
		UnityEngine.Object raioPrefab = Resources.Load("SkillRaio");
		GameObject raio = (GameObject)Instantiate (raioPrefab, transform.position, Quaternion.identity);
	}


	public void CallFinishAttack(){
		player.GetComponent<PlayerController> ().FinishState ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
