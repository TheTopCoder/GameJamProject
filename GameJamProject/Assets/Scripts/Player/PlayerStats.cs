using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public float speed = 0.8f;
	public float rollSpeed = 2f;
	public float rollTime = 0.3f;
	public float rollCooldown = 0.5f;
	public int maxLife = 20;
	public int life;
	public int attackDamage = 5;
	public float attackTime = 0.3f;
	public int attackStrongDamage = 10;
	public float attackStrongTime = 0.4f;
	GameObject lifeBar;
	GameObject boss;

	// Use this for initialization
	void Start () {
		life = maxLife;
		lifeBar = GameObject.Find ("Life");
		boss = GameObject.FindGameObjectWithTag ("Boss");
	}
	
	// Update is called once per frame
	void Update () {
		lifeBar.transform.localScale = new Vector3 ((float)boss.GetComponent<BoneBossController>().life/(float)boss.GetComponent<BoneBossController>().maxLife,lifeBar.transform.localScale.y,1);
		if (life <= 0) {
			Destroy (gameObject);
		}
	}
}
