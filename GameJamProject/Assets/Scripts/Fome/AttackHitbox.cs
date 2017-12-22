using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour {

	GameObject player;
	GameObject boss;
	bool hit;
	bool hitBoss=false;

	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ResolveTrigger(Collider2D other){
		if (transform.FindChild("Zmin")&&transform.FindChild("Zmax")&&other.transform.FindChild("Zmin")&&other.transform.FindChild("Zmax")){
			if (other.transform.FindChild("Zmin").position.y <= transform.FindChild("Zmax").position.y&&other.transform.FindChild("Zmax").position.y >= transform.FindChild("Zmin").position.y){
				if (!hit&&other.transform.tag == "Player"&&!(transform.name == "RaioHitbox(Clone)"||transform.name == "Tempestade_JumpAttack_Hitbox(Clone)"||transform.name == "Tempestade_GroundAttack_Hitbox(Clone)")) {
					hit = true;
					StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
					//boss.GetComponent<FomeController>().canHit = true;
				}
				if (!hit && other.transform.tag == "Player" && (transform.name == "RaioHitbox(Clone)"||transform.name == "Tempestade_JumpAttack_Hitbox(Clone)"||transform.name == "Tempestade_GroundAttack_Hitbox(Clone)")) {
					hit = true;
					StartCoroutine(player.GetComponent<PlayerMovement>().DamagedPlayer());
				}
				if (!hitBoss&&other.transform.tag == "Boss"&&transform.name=="RaioHitbox(Clone)") {
					hitBoss = true;
					boss.GetComponent<TempestadeController>().ReceiveDamage(player.GetComponent<PlayerStats>().attackDamage*2.0f);
					//boss.GetComponent<FomeController>().canHit = true;
				}
				if (!hit && transform.name!="RaioHitbox(Clone)" && other.transform.tag == "TamborTrigger") {
					Debug.Log (transform.name);
					hit = true;
					Debug.Log ("Raio");
					GameObject.FindGameObjectWithTag("Tambor").GetComponent<TamborScript> ().Raio (other.transform.position-boss.transform.position);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		ResolveTrigger (other);
	}
	void OnTriggerStay2D(Collider2D other){
		ResolveTrigger (other);
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			//boss.GetComponent<FomeController>().canHit = false;
		}
	}
}
