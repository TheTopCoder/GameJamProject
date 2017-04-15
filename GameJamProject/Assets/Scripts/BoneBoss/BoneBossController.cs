using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBossController : MonoBehaviour
{

    #region Variables
    Vector3 newPosition;

	public GameObject waveAttack;
	public GameObject groundAttack;
    public int life;
    public int maxLife = 50;
	int dashAttackDamage = 10;
	float dashSpeed = 1.5f;
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
	bool canHit = false;
	bool hit = false;
	GameObject wave;

    #endregion
    void Start()
    {
        life = maxLife;
        cooldownMovement = 0;
        cooldownAbility = 0;
        prob1 = 1;
        prob2 = 1;
        prob3 = 1;
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
                cooldownAbility = Random.Range(10, 15);
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
            StartCoroutine(DashAttack());
            prob1Current = prob1;
            prob2Current = prob2 + 1;
            prob3Current = prob3 + 1;
        }
        else if (abilityNumber >= prob1Current + 1 && abilityNumber <= prob1Current + prob2Current)
        {
			StartCoroutine(WaveAttack());
            prob2Current = prob2;
            prob1Current = prob1 + 1;
            prob3Current = prob3 + 1;
        }
        else if (abilityNumber >= prob1Current + prob2Current + 1 && abilityNumber <= prob1Current + prob2Current + prob3Current)
        {
			StartCoroutine(GroundAttack());
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
    IEnumerator DashAttack()
    {
		Debug.Log("Dash Attack");
		yield return new WaitForSeconds(0.2f);
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		Vector3 playerPosition = player.transform.position;
		float dirX = playerPosition.x - transform.position.x;
		float dirY = playerPosition.y - transform.position.y;
		float dirAbs = Mathf.Sqrt (dirX * dirX + dirY * dirY);
		if (dirAbs != 0) {
			dirX = dirX / dirAbs;
			dirY = dirY / dirAbs;
		}
		float speedX = dashSpeed * dirX;
		float speedY = dashSpeed * dirY;
		rb.velocity = new Vector2 (speedX,speedY);
		while (Mathf.Abs (transform.position.x) < arenaX && Mathf.Abs (transform.position.y) < arenaY) {
			yield return new WaitForSeconds(Time.deltaTime);
			if (canHit&&!hit) {
				hit = true;
				player.GetComponent<PlayerStats>().DamagePlayer (dashAttackDamage);
				break;
			}
		}
        state = "movement";
    }
	IEnumerator WaveAttack(){
        Debug.Log("Wave Attack");
        yield return new WaitForSeconds(0.2f);
		wave = (GameObject) Instantiate (waveAttack,transform.position,Quaternion.Euler(0f,0f,0f));
		if (transform.position.x < player.transform.position.x) {
			wave.GetComponent<WaveAttackScript> ().dir = 1;
		} else {
			wave.GetComponent<WaveAttackScript> ().dir = -1;
		}

		yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
    IEnumerator GroundAttack()
    {
        Debug.Log("Ground Attack");
		yield return new WaitForSeconds(0.2f);
		GameObject earthquake = (GameObject) Instantiate (groundAttack,transform.position,Quaternion.Euler(0f,0f,0f));
		yield return new WaitForSeconds(0.5f);
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
