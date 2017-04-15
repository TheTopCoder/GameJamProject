using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBossController : MonoBehaviour
{

    #region Variables
    Vector3 newPosition;

    public int life;
    public int maxLife = 50;
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
        transform.position = new Vector3(player.transform.position.x - range, player.transform.position.y);
        Debug.Log("Fire Area");
        yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
	IEnumerator WaveAttack(){
        Debug.Log("Wave Attack");
        yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
    IEnumerator GroundAttack()
    {
        Debug.Log("Ground Attack");
        yield return new WaitForSeconds(0.5f);
        state = "movement";
    }
}
