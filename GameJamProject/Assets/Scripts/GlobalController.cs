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
	public int playerEnergy;
	public int playerLife;

	void Start(){
		volume = 0.5f;
		lastScene = SceneManager.GetActiveScene ().name;
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
		if ((!completedFomeCorridor && !completedTempestadeCorridor) && (newScene == "FomeCorridor" || newScene == "TempestadeCorridor" || newScene == "Lobby New")) {
			GameObject.Find ("Player").GetComponent<PlayerStats> ().energy = 50;
		} else if ((completedFomeCorridor || completedTempestadeCorridor) && (newScene == "FomeCorridor" || newScene == "TempestadeCorridor" || newScene == "Lobby New")) {
			GameObject.Find ("Player").GetComponent<PlayerStats> ().energy = 0;
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

	void Awake() {
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Update(){
		if (Input.GetKey (KeyCode.R) && Input.GetKey (KeyCode.X)) {
			lastScene = "Lobby New";
			completedFomeCorridor = false;
			defeatedFome = false;
			completedTempestadeCorridor = false;
			defeatedTempestade = false;
//			Debug.Log ("Reset");
			GameObject.Find ("TransitionCanvas").GetComponent<TransitionScript> ().nome = "Lobby New";
			GameObject.Find ("TransitionCanvas").GetComponent<TransitionScript> ().ChangeScene ();
		}
	}

	// Use this for initialization
	void OnLevelWasLoaded() {
		string newScene = SceneManager.GetActiveScene ().name;
		GameObject player = GameObject.Find("Player");
		if ((completedFomeCorridor && newScene == "FomeCorridor")||(completedTempestadeCorridor && newScene == "TempestadeCorridor")) {
			GameObject.FindGameObjectWithTag ("Portal").GetComponent<SpriteRenderer> ().enabled = true;
			GameObject portal = GameObject.FindGameObjectWithTag ("Portal");
			portal.GetComponentInChildren <BackToLobby>().enabled = true;
			GameObject.Find ("Player").transform.position = new Vector3 (GameObject.Find ("Portal 2").transform.position.x + 2f, GameObject.Find ("Player").transform.position.y, 0);
		}
		if ((completedFomeCorridor || completedTempestadeCorridor) && (newScene == "FomeCorridor" || newScene == "TempestadeCorridor")) {
			GameObject.Find ("Tutorial Vida").SetActive(false);
		}
		if ((!completedFomeCorridor&&!completedTempestadeCorridor)&&(newScene == "FomeCorridor"||newScene == "TempestadeCorridor"||newScene == "Lobby New")) {
			GameObject.Find ("Player").GetComponent<PlayerStats>().energy = 50;
		} else if ((completedFomeCorridor || completedTempestadeCorridor) && (newScene == "FomeCorridor" || newScene == "TempestadeCorridor" || newScene == "Lobby New")) {
			GameObject.Find ("Player").GetComponent<PlayerStats> ().energy = 0;
		}
		if (newScene == "FomeTriangularArena" || newScene == "Tempestade") {
//			player.GetComponent<PlayerStats>().life = playerLife;
//			player.GetComponent<PlayerStats>().energy = playerEnergy;
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


}
