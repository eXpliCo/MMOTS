using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	enum GameState {Game, Menu};

	public GameObject mainCamera;
	public GameObject mainMenu;
	private RTSCamera mainCameraScript;
	private MainMenuScript mainMenuScript;

	private GameState state = GameState.Menu;
	// Use this for initialization
	void Start () {
		mainCameraScript = (RTSCamera) mainCamera.GetComponent<RTSCamera> ();
		mainMenuScript = (MainMenuScript) mainMenu.GetComponent<MainMenuScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (state == GameState.Game) {
				state = GameState.Menu;
				mainMenuScript.showMenu (mainCamera.transform.position);
				mainCameraScript.hideGame(mainMenu.transform.position);
			} else {
				state = GameState.Game;
				mainMenuScript.hideMenu ();
				mainCameraScript.showGame ();
			}
		}
	
	}
}
