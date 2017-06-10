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
<<<<<<< HEAD
        else if(boss.name.Equals("FireBoss"))
        {
//            GetComponent<Slider>().value = (float)boss.GetComponent<FireBossController>().life / boss.GetComponent<FireBossController>().maxLife;
        }
=======
>>>>>>> 4178732143e2f731bc145c7823c2aea5f24eba7a
    }
}
