using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossTransitionScript : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Image image2;
    void Start()
    {
        StartCoroutine(imageTransition());
    }
    IEnumerator imageTransition()
    {
		yield return new WaitForSeconds(0.2f);
        for (int i = 0; i <= 100; i++)
        {
            Color aux = image.color;
            aux.a += 0.01f;
            image.color = aux;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i <= 100; i++)
        {
            Color aux = image2.color;
            aux.a += 0.01f;
            image2.color = aux;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("FomeTriangularArena");

    }
}
