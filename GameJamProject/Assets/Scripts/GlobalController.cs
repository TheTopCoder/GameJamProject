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
	public float volume=0.5f;

	void Start(){
		volume = 0.5f;
		lastScene = SceneManager.GetActiveScene ().name;
		if ((completedFomeCorridor && SceneManager.GetActiveScene ().name == "FomeCorridor")||(completedTempestadeCorridor && SceneManager.GetActiveScene ().name == "TempestadeCorridor")) {
			GameObject.FindGameObjectWithTag ("Portal").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject portal = GameObject.FindGameObjectWithTag ("Portal");
			portal.GetComponentInChildren <BackToLobby>().enabled = true;
			GameObject.Find ("Player").transform.position = new Vector3 (GameObject.Find ("Portal 2").transform.position.x + 1.5f, GameObject.Find ("Player").transform.position.y, 0);
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
		string newScene = SceneManager.GetActiveScene ().name;
		if ((completedFomeCorridor && newScene == "FomeCorridor")||(completedTempestadeCorridor && newScene == "TempestadeCorridor")) {
			GameObject.FindGameObjectWithTag ("Portal").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject portal = GameObject.FindGameObjectWithTag ("Portal");
			portal.GetComponentInChildren <BackToLobby>().enabled = true;
			GameObject.Find ("Player").transform.position = new Vector3 (GameObject.Find ("Portal 2").transform.position.x + 2f, GameObject.Find ("Player").transform.position.y, 0);
		}
		if ((completedFomeCorridor || completedTempestadeCorridor) && (newScene == "FomeCorridor" || newScene == "TempestadeCorridor")) {
			GameObject.Find ("Tutorial Vida").SetActive(false);
		}
		if (newScene == "FomeCorridor"||newScene == "TempestadeCorridor") {
		}
		if (newScene == "Lobby New") {
			if (defeatedFome) {
				GameObject.Find ("PortaFome").GetComponent<Collider2D> ().enabled = false;
				GameObject.Find ("PortaFomeFechada").GetComponent<SpriteRenderer> ().enabled = true;
			}
			if (defeatedTempestade) {
				GameObject.Find ("PortaTempestade").GetComponent<Collider2D> ().enabled = false;
				GameObject.Find ("PortaTempestadeFechada").GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
