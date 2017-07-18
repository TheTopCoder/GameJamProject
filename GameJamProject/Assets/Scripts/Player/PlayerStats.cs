using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
	public float speed = 1f;
	public float rollSpeed=6.5f;
	public float rollTime=1.9f;
	public float rollCooldown = 0.4f;
	public int maxLife = 20;
	public int life;
	public int attackDamage = 5;
	public float attackTime = 0.3f;
	public int attackStrongDamage = 10;
	public float attackStrongTime = 0.4f;
	GameObject fade;
	[SerializeField]
	GameObject Heart1;
	[SerializeField]
	GameObject Heart2;
	[SerializeField]
	GameObject Heart3;
	[SerializeField]
	GameObject FadeOut;

	void Start ()
    {
		life = maxLife;
	}
	
	void Update ()
    {
		if (SceneManager.GetActiveScene().name == "CorridorScene"||SceneManager.GetActiveScene().name == "Lobby"){
			
		}
		else if (life == 3) {
			Heart1.SetActive (true);
			Heart2.SetActive (true);
			Heart3.SetActive (true);
		}
		else if (life == 2) {
//			Heart3.GetComponent<HeartScript> ().DestroyHeart ();
			Heart1.SetActive (true);
			Heart2.SetActive (true);
			Heart3.SetActive (false);
		}
		else if (life == 1) {
//			Heart2.GetComponent<HeartScript> ().DestroyHeart ();
			Heart1.SetActive (true);
			Heart2.SetActive (false);
			Heart3.SetActive (false);
		}
		else if (life <= 0) {
//			Heart1.GetComponent<HeartScript> ().DestroyHeart ();
			Heart1.SetActive (false);
			Heart2.SetActive (false);
			Heart3.SetActive (false);
			fade = (GameObject) Instantiate (FadeOut, transform.position, new Quaternion(0f,0f,0f,0f));
			fade.GetComponent<FadeTransition>().nextScene = "GameOver";
			Destroy (GameObject.FindGameObjectWithTag ("PlayerBase"));
			Destroy(gameObject);
		}
	}

	public void DamagePlayer(){
		life --;

	}
}
