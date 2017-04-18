using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    Image panel;
    [SerializeField]
    Text texto;

    float time;
    bool goingUp;
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StartCoroutine(LoadScene("CorridorScene"));
        }
        BlinkText();
    }

    IEnumerator LoadScene(string scene)
    {
        for (int i = 0; i <= 100; i++)
        {
            Color aux = panel.color;
            aux.a += 0.01f;
            panel.color = aux;
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene(scene);
    }
    void BlinkText()
    {
        time += Time.deltaTime;
        if (goingUp)
        {
            Color aux = texto.color;
            aux.a += Time.deltaTime;
            texto.color = aux;
        }
        else if (!goingUp)
        {
            Color aux = texto.color;
            aux.a -= Time.deltaTime ;
            texto.color = aux;
        }

        if (time >= 1)
        {
            goingUp = !goingUp;
            time = 0;
        }
    }
    
}
