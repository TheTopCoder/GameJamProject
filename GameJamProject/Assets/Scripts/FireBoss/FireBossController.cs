﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossController : MonoBehaviour
{

    #region Variables
    Vector3 newPosition;

    public int life;
    public int maxLife = 50;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float range;
	[SerializeField]
	GameObject fireball;
	[SerializeField]
	GameObject groundfire;
	[SerializeField]
	GameObject firebreath;
    float minX;
    float minY;
    float maxX;
    float maxY;
    float cooldownMovement;
    float cooldownAbility;
    int prob1;
    int prob1Current;
    int prob2;
    int prob2Current;
    int prob3;
    int prob3Current;
    string state;
    bool moving;
	float arenaX = 1.5f;
	float arenaY = 0.7f;
    #endregion
    void Start()
    {
        life = maxLife;
        cooldownMovement = 0;
        cooldownAbility = 0;
        prob1 = 1;
        prob2 = 1;
        prob3 = 5;
        prob1Current = prob1;
        prob2Current = prob2;
        prob3Current = prob3;
        minX = player.transform.position.x - range;
        minY = player.transform.position.y - range;
        maxX = player.transform.position.x + range;
        maxY = player.transform.position.y + range;
        state = "movement";
    }
    void Update()
    {
        if (state == "movement")
        {
            if (life <= 0)
            {
                Destroy(gameObject);
            }
            cooldownMovement -= Time.deltaTime;
            cooldownAbility -= Time.deltaTime;
            if (cooldownAbility <= 0)
            {
                state = "ChooseAbility";
                cooldownAbility = Random.Range(6, 10);
            }
            else if (cooldownMovement <= 0)
            {
                moving = true;
                minX = player.transform.position.x - range;
                minY = player.transform.position.y - range;
                maxX = player.transform.position.x + range;
                maxY = player.transform.position.y + range;
                newPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
                cooldownMovement = Random.Range(1, 2);
            }
            if (moving)
            {
                if (MoveToPosition(newPosition))
                {
                    moving = false;
                }
            }
        }
        else if (state == "ChooseAbility")
        {
            state = "ability";
            ChooseAbility();
        }

    }
    void ChooseAbility()
    {
        int abilityNumber = Random.Range(1, prob1Current + prob2Current + prob3Current + 1);
        if (abilityNumber >= 1 && abilityNumber <= prob1Current)
        {
            StartCoroutine(FireArea());
            prob1Current = prob1;
            prob2Current = prob2 + 1;
            prob3Current = prob3 + 1;
        }
        else if (abilityNumber >= prob1Current + 1 && abilityNumber <= prob1Current + prob2Current)
        {
            StartCoroutine(FloorOnFire());
            prob2Current = prob2;
            prob1Current = prob1 + 1;
            prob3Current = prob3 + 1;
        }
        else if (abilityNumber >= prob1Current + prob2Current + 1 && abilityNumber <= prob1Current + prob2Current + prob3Current)
        {
            StartCoroutine(Fireball());
            prob3Current = prob3;
            prob2Current = prob2 + 1;
            prob1Current = prob1 + 1;
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
            transform.position = Vector3.MoveTowards(this.transform.position, newPosition, Time.deltaTime);
            return false;
        }
    }
    IEnumerator FireArea()
    {
        transform.position = new Vector3(player.transform.position.x - range/2, player.transform.position.y);
		if (transform.position.x < player.transform.position.x) {
			Instantiate (firebreath, transform.position, Quaternion.Euler (0f, 0f, 0f));
		} else {
			Instantiate (firebreath, transform.position, Quaternion.Euler (0f, 0f, 0f));
		}
        Debug.Log("Fire Area");
        yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
    IEnumerator FloorOnFire()
    {
        Debug.Log("Floor on Fire");
        yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < 4; i++) {
			float fireX = Random.Range (-arenaX,arenaX);
			float fireY = Random.Range (-arenaY,arenaY);
			Instantiate (groundfire, new Vector3(fireX,fireY,0), Quaternion.Euler (0f, 0f, 0f));
		}
		yield return new WaitForSeconds (0.4f);
        state = "movement";
    }
    IEnumerator Fireball()
    {
		yield return new WaitForSeconds (0.2f);
		Instantiate (fireball, transform.position, Quaternion.Euler (0f, 0f, 0f));
		yield return new WaitForSeconds (0.5f);
        state = "movement";
    }
}
