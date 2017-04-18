using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public float speed = 1f;
	public float rollSpeed = 4.5f;
	public float rollTime = 1.8f;
	public float rollCooldown = 0.5f;
	public int maxLife = 20;
	public int life;
	public int attackDamage = 5;
	public float attackTime = 0.3f;
	public int attackStrongDamage = 10;
	public float attackStrongTime = 0.4f;

	void Start ()
    {
		life = maxLife;
	}
	
	void Update ()
    {
		if (life <= 0) {
			Destroy (gameObject);
		}
	}

	public void DamagePlayer(int damage){
		life -= damage;

	}
}
