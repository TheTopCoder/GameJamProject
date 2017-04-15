using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttackScript : MonoBehaviour 
{
	float growingSpeed;
	void Update () 
	{
		transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime) * growingSpeed;
		if (transform.localScale.x >= 9) 
		{
			Destroy (gameObject);
		}
	}
}
