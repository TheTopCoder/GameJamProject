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
	GameObject globalController;

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
		globalController = GameObject.Find ("Global Controller");
		string currentScene;
		currentScene = SceneManager.GetActiveScene ().name;
		globalController.GetComponent<GlobalController> ().lastScene = currentScene;
		if (currentScene == "FomeCorridor") {
			globalController.GetComponent<GlobalController> ().completedFomeCorridor = true;
			Debug.Log ("Completed Fome Corridor");
		}
		if (currentScene == "TempestadeCorridor") {
			globalController.GetComponent<GlobalController> ().completedTempestadeCorridor = true;
		}
		if (currentScene == "Fome") {
			globalController.GetComponent<GlobalController> ().defeatedFome = true;
		}
		if (currentScene == "Tempestade") {
			globalController.GetComponent<GlobalController> ().defeatedTempestade = true;
		}
        SceneManager.LoadScene(scene);
    }
}
