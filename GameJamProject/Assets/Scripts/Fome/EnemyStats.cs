using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	int life;
	bool die = false;
	// Use this for initialization
	void Start () {
		life = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0&&!die) {
			GetComponent<Animator> ().SetTrigger ("Die");
			die = true;
			GetComponent<CrowMinion> ().Die ();
		}
	}

	public void ReceiveDamage(int damage){
		life -= damage;
	}
}
