using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    bool goingUp;
    public float maxLight;
    public float minLight;
	float rate;
	void Start () {
        goingUp = true;
		GetComponent<Light>().range = minLight;
		rate = (maxLight - minLight)*Time.deltaTime/20;
	}
	void FixedUpdate () {
        if(goingUp)
        {
			GetComponent<Light>().range += rate;
        }
        else if(!goingUp)
        {
            GetComponent<Light>().range -= rate;
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
