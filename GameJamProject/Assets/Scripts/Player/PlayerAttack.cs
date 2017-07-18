﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour {

	public string state;//wait,attack,attackStrong,recoverAttack
	bool canHit;
	ArrayList hit;
	int attackDamage;
	float attackTime;
	int attackStrongDamage;
	float attackStrongTime;
	float attackCurrentTime;
	PlayerStats playerStats;
	GameObject boss;
    [SerializeField]
    Animator handAnim;
    [SerializeField]
    Animator bodyAnim;
    [SerializeField]
    AnimationClip handAttackAnim;

	// Use this for initialization
	void Start () {
		playerStats = GetComponent<PlayerStats> ();
		attackDamage = playerStats.attackDamage;
		attackTime = handAttackAnim.length * 1.05f;
		attackStrongDamage = playerStats.attackStrongDamage;
		attackStrongTime = playerStats.attackStrongTime;
		state = "wait";
		canHit = false;
		hit = new ArrayList();
		attackCurrentTime = attackTime;
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "wait") {
			if (Input.GetAxisRaw ("XboxX")>0 || Input.GetAxisRaw("XboxR2") > 0 || Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0)) {
				state = "attack";
                transform.FindChild("ShakeWeaponSound").GetComponent<AudioSource>().Play();
                handAnim.SetTrigger("Attack");
            }
			else if (Input.GetAxisRaw ("XboxL2")>0) {
//				state = "attackStrong";
			}
		}
		if (state == "attack") {
			canHit = true;
//				boss.GetComponent<FomeController>().ReceiveDamage(attackDamage);
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
				attackCurrentTime = attackTime;
//				hit = false;
				canHit = false;
				if (hit.Count > 0) {
					hit.Clear ();
				}
				state = "wait";
			}
		}
		//Aqui atira o tacape
		if (state == "attackStrong") {
			handAnim.SetBool ("Estocada", true);
			bodyAnim.SetBool ("Estocada", true);
			Debug.Log ("AttackStrong");
			if (boss.name.Equals ("BoneBoss")) {
				boss.GetComponent<BoneBossController> ().life -= attackStrongDamage;
			} else if (boss.name.Equals ("FireBoss")) {
//                    boss.GetComponent<FireBossController>().life -= attackStrongDamage;
                    
			}
			attackCurrentTime -= Time.deltaTime;
			if (attackCurrentTime < 0) {
                handAnim.SetBool("Estocada", false);
                bodyAnim.SetBool("Estocada", false);
                attackCurrentTime = attackStrongTime;
				state = "wait";
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (canHit&&(other.tag == "Boss"||other.tag == "Enemy")) {
			bool alreadyHit = false;
			for (int i = 0; i < hit.Count; i++) {
				if ((int)hit [i] == other.gameObject.GetInstanceID ()) {
					alreadyHit = true;
				}
			}
			if (!alreadyHit) {
				if (other.tag == "Boss") {
					boss.GetComponent<FomeController> ().ReceiveDamage (attackDamage);
					hit.Add (boss.GetInstanceID ());
				} else {
					other.gameObject.GetComponent<EnemyStats> ().ReceiveDamage (attackDamage);
					hit.Add (other.gameObject.GetInstanceID ());
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (canHit&&(other.tag == "Boss"||other.tag == "Enemy")) {
			bool alreadyHit = false;
			for (int i = 0; i < hit.Count; i++) {
				if ((int)hit [i] == other.gameObject.GetInstanceID ()) {
					alreadyHit = true;
				}
			}
			if (!alreadyHit) {
				if (other.tag == "Boss") {
					boss.GetComponent<FomeController> ().ReceiveDamage (attackDamage);
					hit.Add (boss.GetInstanceID ());
				} else {
					other.gameObject.GetComponent<EnemyStats> ().ReceiveDamage (attackDamage);
					hit.Add (other.gameObject.GetInstanceID ());
				}
			}
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Boss") {
			
		}
	}
/*	IEnumerator DamageTime()
	{
		yield return new WaitForSeconds(handAttackAnim.length);
		if (boss != null) {
			if (boss.name.Equals ("BoneBoss")) {
				boss.GetComponentInChildren<BoneBossController> ().life -= attackDamage;
				boss.GetComponentInChildren<SpriteRenderer> ().color = Color.red;
				yield return new WaitForSeconds (0.05f);
				boss.GetComponentInChildren<SpriteRenderer> ().color = Color.white;
			} else if (boss.name.Equals ("Fome")) {
				boss.GetComponent<FomeController> ().life -= attackDamage;
				boss.GetComponent<SpriteRenderer> ().color = Color.red;
				yield return new WaitForSeconds (0.05f);
				boss.GetComponent<SpriteRenderer> ().color = Color.white;
			} else if (boss.name.Equals ("FireBoss")) {
				//            boss.GetComponent<FireBossController>().life -= attackDamage;
			}
		}
	}
*/		

}

