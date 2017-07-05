using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Vector3 offset;
	void Start()
	{
		offset = new Vector3 (1,0,-10);
	}

	void Update ()
    {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position + offset;		
		}
	}
}
