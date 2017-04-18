using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    [SerializeField]
    Image panel;
    public string nome;

    public void ChangeScene()
    {
        StartCoroutine(LoadScene(nome));
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
}
