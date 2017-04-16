using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackScript : MonoBehaviour {
	float waveSpeed = 5f;
	float duration = 1f;
	float time;
	public int dir;
     GameObject boss;
    public GameObject player;

	// Use this for initialization
	void Start () {
        boss = GameObject.FindGameObjectWithTag("Boss");
		time = duration;
        if (boss.GetComponent<BoneBossController>().faceRight)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        if (dir > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
		time -= Time.deltaTime;
		transform.position += new Vector3(dir * waveSpeed,0,0) * Time.deltaTime;
		if (time < 0) {
			Destroy (gameObject);	
		}
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag.Equals("Player"))
        {
            StartCoroutine(col.GetComponent<PlayerMovement>().DamagedPlayer());
        }
    }


}
