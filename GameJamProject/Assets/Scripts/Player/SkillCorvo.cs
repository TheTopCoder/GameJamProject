using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SkillCorvo : MonoBehaviour {

	bool damaged=false;

	// Use this for initialization
	void Start () {
		if (GameObject.Find("Player") != null && GameObject.Find("Player").transform.localScale.x > 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-6f, 0);
			transform.localScale = new Vector3(-Mathf.Abs (transform.localScale.x),transform.localScale.y,transform.localScale.z);
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (6f, 0);
			transform.localScale = new Vector3(Mathf.Abs (transform.localScale.x),transform.localScale.y,transform.localScale.z);
		}
		damaged = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DieCorvo(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		float damage=20f;
		Debug.Log ("Corvo Trigger");
//		if (damaged==false) {
			damaged = true;
			if (other.tag == "Cenario") {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				GetComponent<Animator> ().SetTrigger ("Die");
			}
			if (other.tag == "Tambor") {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				other.GetComponent<TamborScript> ().Raio (other.transform.position - transform.position);
				GetComponent<Animator> ().SetTrigger ("Die");
			}
			if (other.tag == "Enemy") {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				other.GetComponent<EnemyStats> ().ReceiveDamage (damage);
				GetComponent<Animator> ().SetTrigger ("Die");
			}
			if (other.tag == "Boss") {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				if (SceneManager.GetActiveScene ().name == "Tempestade") {				
					other.GetComponent<TempestadeController> ().ReceiveDamage (damage);
				} else {
					other.GetComponent<FomeController> ().ReceiveDamage (damage);
				}
				GetComponent<Animator> ().SetTrigger ("Die");
			}
//		}
	}
}
