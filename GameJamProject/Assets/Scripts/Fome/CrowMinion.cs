using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMinion : MonoBehaviour {

	GameObject player;
	GameObject attackHitbox;
	float speed;
	float attackDist;
	public string state;
	bool canHit;
	float attackCooldownMax;
	float attackCooldown;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");	
		speed = 1.4f;
		state = "spawn";
		attackDist = 1.0f;
		attackCooldownMax = 1.5f;
		attackCooldown = 0;
		GetComponent<Animator> ().SetTrigger ("Spawn");
	}

	public void FinishSpawn(){
		state = "movement";
	}

	public void Die(){
		state = "die";
	}

	public void EnableHit(){
		canHit = true;
	}

	public void DisableHit(){
		canHit = false;
	}

	public void FinishAttack(){
		state = "movement";
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "Player") {
			if (canHit) {
				canHit = false;
				StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.transform.tag == "Player") {
			if (canHit) {
				canHit = false;
				StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
			}
		}
	}

	void CreateHitbox(string name){
		UnityEngine.Object hitboxprefab = Resources.Load ("Fome/" + name);
		attackHitbox = (GameObject) Instantiate (hitboxprefab, transform.position, Quaternion.identity,transform);
	}
	void DestroyHitbox(){
		if (attackHitbox != null) {
			Destroy (attackHitbox);
		}
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {
		}
		else if (state == "movement") {
			Vector3 dir = -(transform.position - player.transform.position);
			dir.z = 0;
			dir.Normalize ();
			if (dir.x > 0) {
				transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			} else {
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
			}
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (dir.x, dir.y) * speed;
			attackCooldown -= Time.deltaTime;
			if ((transform.position - player.transform.position).magnitude <= attackDist && attackCooldown <= 0) {
				//Careful with the Z axis
				state = "begin_attack";
			}
		} else if (state == "begin_attack") {
			GetComponent<Animator> ().SetTrigger ("Attack");
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			attackCooldown = attackCooldownMax;
			state = "attack";
		}
		else if (state == "attack") {
			
		}
	}
}
