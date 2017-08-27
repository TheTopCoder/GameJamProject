using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	public float life;
	bool die = false;
	// Use this for initialization
	void Start () {
		life = 1;
		die = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0&&!die) {
			GetComponent<Animator> ().SetTrigger ("Die");
			die = true;
			transform.tag = "Dying";
			Destroy (GetComponent<CrowMinion> ());
			//GetComponent<CrowMinion> ().Die ();
		}
	}


	public void DestroyEnemy(){
		Destroy (gameObject);
	}

	public void ReceiveDamage(float damage){
		if (GetComponent<CrowMinion>() != null && GetComponent<CrowMinion>().state != "spawn"){
			life -= damage;
		}
	}
}
