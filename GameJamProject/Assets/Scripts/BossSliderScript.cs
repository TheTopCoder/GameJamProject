using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossSliderScript : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
	void Update ()
    {
        if (boss == null)
        {
            GetComponent<Slider>().value = 0;
        }
        else if (boss.name.Equals("BoneBoss"))
        {
            GetComponent<Slider>().value = (float)boss.GetComponent<BoneBossController>().life / boss.GetComponent<BoneBossController>().maxLife;
        }
        else if(boss.name.Equals("FireBoss"))
        {
            GetComponent<Slider>().value = (float)boss.GetComponent<FireBossController>().life / boss.GetComponent<FireBossController>().maxLife;
        }
    }
}
