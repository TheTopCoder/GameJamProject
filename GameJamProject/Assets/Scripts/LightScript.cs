using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    bool goingUp;
    public float maxLight;
    public float minLight;
	void Start () {
        goingUp = true;
	}
	void FixedUpdate () {
        if(goingUp)
        {
            GetComponent<Light>().range += 0.004f;
        }
        else if(!goingUp)
        {
            GetComponent<Light>().range -= 0.004f;
        }

        if(GetComponent<Light>().range >= maxLight)
        {
            goingUp = false;
        }
        else if (GetComponent<Light>().range <= minLight)
        {
            goingUp = true;
        }

    }
}
