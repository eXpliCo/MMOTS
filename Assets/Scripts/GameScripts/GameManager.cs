using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	enum GameState {Game, Menu};

	public GameObject mainCamera;
	public GameObject mainMenu;
	public GameObject gameUi;
	private RTSCamera mainCameraScript;
	private MainMenuScript mainMenuScript;

	private GameState state = GameState.Menu;
	// Use this for initialization
	void Start () {
		mainCameraScript = (RTSCamera) mainCamera.GetComponent<RTSCamera> ();
		mainMenuScript = (MainMenuScript) mainMenu.GetComponent<MainMenuScript> ();
		goToGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (state == GameState.Game) {
				goToMenu ();
			} else {
				goToGame ();
			}
		}
	}

	private void goToMenu () {
		state = GameState.Menu;
		gameUi.SetActive (false);
		mainMenuScript.showMenu (mainCamera.transform.position);
		mainCameraScript.hideGame (mainMenu.transform.position);
	}

	private void goToGame() {
		state = GameState.Game;
		gameUi.SetActive (true);
		mainMenuScript.hideMenu ();
		mainCameraScript.showGame ();
	}
}
