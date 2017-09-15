using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillRaio : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		GetComponent<Rigidbody2D>().velocity
		if ( GameObject.FindGameObjectWithTag ("Boss") != null){
			transform.position = new Vector2(GameObject.FindGameObjectWithTag ("Boss").transform.position.x-0.7f,GameObject.FindGameObjectWithTag ("Boss").transform.position.y-0.48f);
		}
		else if ( GameObject.FindGameObjectWithTag ("Enemy") != null){
			transform.position = GameObject.FindGameObjectWithTag("Enemy").transform.position;
		}
		else if (GameObject.Find("Player") != null){
			transform.position = new Vector3(GameObject.Find("Player").transform.position.x+2.5f,GameObject.Find("Player").transform.position.y,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		float damage=15f;
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyStats> ().ReceiveDamage(damage);
			Destroy (gameObject);
		}
		if (other.tag == "Boss") {
			if (SceneManager.GetActiveScene ().name == "Tempestade") {				
				other.GetComponent<TempestadeController> ().ReceiveDamage (damage);
			} else {
				other.GetComponent<FomeController> ().ReceiveDamage (damage);
			}
			Destroy (gameObject);
		}
	}
}
