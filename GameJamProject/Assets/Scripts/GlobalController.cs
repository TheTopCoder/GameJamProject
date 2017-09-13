using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GlobalController : MonoBehaviour {

	public static GlobalController Instance;
	public bool defeatedFome = false;
	public bool defeatedTempestade = false;
	public bool completedFomeCorridor = false;
	public bool completedTempestadeCorridor = false;
	public string lastScene="Noone";

	void Start(){
		if (completedFomeCorridor && SceneManager.GetActiveScene ().name == "FomeCorridor") {
			GameObject.FindGameObjectWithTag ("Portal").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject portal = GameObject.FindGameObjectWithTag ("Portal");
			portal.GetComponentInChildren <BackToLobby>().enabled = true;
			GameObject.Find ("Player").transform.position = new Vector3 (GameObject.Find ("Portal 2").transform.position.x + 3f, GameObject.Find ("Player").transform.position.y, 0);
		}
	}

	void Awake() {
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void OnLevelWasLoaded() {
		if (completedFomeCorridor && SceneManager.GetActiveScene ().name == "FomeCorridor") {
			GameObject.FindGameObjectWithTag ("Portal").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject portal = GameObject.FindGameObjectWithTag ("Portal");
			portal.GetComponentInChildren <BackToLobby>().enabled = true;
			GameObject.Find ("Player").transform.position = new Vector3 (GameObject.Find ("Portal 2").transform.position.x + 2f, GameObject.Find ("Player").transform.position.y, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
