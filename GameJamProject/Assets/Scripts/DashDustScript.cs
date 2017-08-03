using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDustScript : MonoBehaviour
{
    [SerializeField]
    AnimationClip dashDustClip;
	// Use this for initialization
	void Start () {
//        StartCoroutine(dashDust());
	}

    IEnumerator dashDust()
    {
        yield return new WaitForSeconds(dashDustClip.length * 2);
        Destroy(gameObject);
    }
}
