using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Vector3 offset;
	void Update ()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position - offset;		
	}
}
