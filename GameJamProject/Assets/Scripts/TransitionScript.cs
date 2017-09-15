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
            yield return new WaitForSeconds(0.0075f);
        }
		globalController = GameObject.Find ("Global Controller");
		string currentScene;
		currentScene = SceneManager.GetActiveScene ().name;
		if (currentScene == "FomeCorridor"&&scene=="FomeTriangularArena") {
			globalController.GetComponent<GlobalController> ().completedFomeCorridor = true;
			globalController.GetComponent<GlobalController> ().playerEnergy = GameObject.Find ("Player").GetComponent<PlayerStats> ().energy;
			globalController.GetComponent<GlobalController> ().playerLife = GameObject.Find ("Player").GetComponent<PlayerStats> ().life;
		}
		if (currentScene == "TempestadeCorridor"&&scene=="Tempestade") {
			globalController.GetComponent<GlobalController> ().completedTempestadeCorridor = true;
			globalController.GetComponent<GlobalController> ().playerEnergy = GameObject.Find ("Player").GetComponent<PlayerStats> ().energy;
			globalController.GetComponent<GlobalController> ().playerLife = GameObject.Find ("Player").GetComponent<PlayerStats> ().life;
		}
		if (currentScene == "FomeTriangularArena"&&scene!="GameOver") {
			scene = "Lobby New";
		}
		if (currentScene == "Tempestade"&&scene!="GameOver") {
			scene = "Lobby New";
		}
		if (currentScene == "GameOver") {
			Debug.Log (globalController.GetComponent<GlobalController> ().lastScene);
			if (globalController.GetComponent<GlobalController> ().lastScene == "FomeTriangularArena"||globalController.GetComponent<GlobalController> ().lastScene == "FomeCorridor") {
				scene = "FomeCorridor";
			} else {
				scene = "TempestadeCorridor";
			}
		}
		if (currentScene == "BeatDemo") {
			scene = "Lobby New";
		}

		if ((currentScene == "FomeTriangularArena" || currentScene == "Tempestade")&&scene!="GameOver"&&globalController.GetComponent<GlobalController> ().defeatedFome&&globalController.GetComponent<GlobalController> ().defeatedTempestade){
			scene = "BeatDemo";
		}

		globalController.GetComponent<GlobalController> ().lastScene = currentScene;
		SceneManager.LoadScene(scene);
    }
}
