using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossController : MonoBehaviour
{
    Vector3 newPosition;

    int life;
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

    
	void Start ()
    {
        life = 50;
        cooldownMovement = 0;
        cooldownAbility = 0;
        minX = player.transform.position.x - range;
        minY = player.transform.position.y - range;
        maxX = player.transform.position.x + range;
        maxY = player.transform.position.y + range;
	}
	
	void Update ()
    {
        if(life <= 0)
        {
            Destroy(gameObject);
        }
        cooldownMovement -= Time.deltaTime;
        cooldownAbility -= Time.deltaTime;
		if(cooldownAbility <= 0)
        {
            Debug.Log("Use ability");
            cooldownAbility = Random.Range(10, 15);
        }
        else if(cooldownMovement <= 0)
        {
            moving = true;
            newPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
            cooldownMovement = Random.Range(3, 5);
        }
        if (moving)
        {
            if (MoveToPosition(newPosition))
            {
                moving = false;
            }
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
}
