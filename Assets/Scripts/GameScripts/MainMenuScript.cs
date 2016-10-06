using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void LoadScene(int level)
	{
		SceneManager.LoadScene (level);
	}

	public void Logout() {
		Application.Quit ();
	}

	public void showMenu(Vector3 cameraPostion) {
		gameObject.SetActive (true);
		transform.position = cameraPostion + new Vector3(260, 0, 350);
	}

	public void hideMenu() {
		gameObject.SetActive (false);
	}
}
