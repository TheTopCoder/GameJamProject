using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBossController : MonoBehaviour
{
	Vector3 newPosition;

	public int life;
	[SerializeField]
	GameObject player;
	[SerializeField]
	float range;
	float minX;
	float minY;
	float maxX;
	float maxY;
	float cooldownMovement;
	float cooldownAbility;
	bool moving;
	float attackDamage;
	string state;
	bool canHit;

	void Start ()
	{
		life = 50;
		cooldownMovement = 0;
		cooldownAbility = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		minX = player.transform.position.x - range;
		minY = player.transform.position.y - range;
		maxX = player.transform.position.x + range;
		maxY = player.transform.position.y + range;
		state = "movement";
	}

	void Update ()
	{
		Debug.Log (state);
		if(life <= 0)
		{
			Destroy(gameObject);
		}
		if (state == "movement") {
			cooldownMovement -= Time.deltaTime;
			cooldownAbility -= Time.deltaTime;
			if (cooldownAbility <= 0) {
				Debug.Log ("Use ability");
				cooldownAbility = Random.Range (10, 15);
				state = "chooseAbility";
			} else if (cooldownMovement <= 0) {
				moving = true;
				minX = player.transform.position.x - range;
				minY = player.transform.position.y - range;
				maxX = player.transform.position.x + range;
				maxY = player.transform.position.y + range;
				newPosition = new Vector3 (Random.Range (minX, maxX), Random.Range (minY, maxY));
				cooldownMovement = Random.Range (3, 5);
			}
			if (moving) {
				if (MoveToPosition (newPosition)) {
					moving = false;
				}
			}
		} else if (state == "chooseAbility"){
			state = "attack";
			ChooseAbility ();
		}
	}

	bool MoveToPosition(Vector3 newPosition)
	{
		if (Vector3.Distance(transform.position, newPosition) < 0.01f)
		{
			return true;
		}
		else
		{
			GetComponent<PolyNavAgent>().SetDestination(newPosition);
			return false;
		}
	}

	void ChooseAbility(){
		int ability;
		ability = (int) Mathf.Floor(Random.Range (1f,4f));
		Debug.Log (ability);
		if (ability == 1) {
			BasicAttack ();
		}
		else if (ability == 2){
			WaveAttack ();
		}
		else if (ability == 3){
			GroundAttack ();
		}
	}

	void BasicAttack(){
		newPosition = new Vector3(player.transform.position.x, player.transform.position.y);
		GetComponent<PolyNavAgent>().SetDestination(newPosition);
		if (Vector3.Distance (transform.position, newPosition) < 0.01f) {
			state = "movement";
		}
		if (canHit) {
//			player.life -= attackDamage;
		}
		state = "movement";
	}
	void WaveAttack(){
		state = "movement";
	}
	void GroundAttack(){
		state = "movement";
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			canHit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			canHit = false;
		}
	}
}
