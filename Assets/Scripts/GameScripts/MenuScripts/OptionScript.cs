using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour {

	private const string MAINMENUSCENE = "MainMenu";

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (MAINMENUSCENE);
		}
	}

	public void backToMainMenu () {
		SceneManager.LoadScene (MAINMENUSCENE);
	}

	public void applyOptions(Dropdown dropdownOption) {
		SceneManager.LoadScene (MAINMENUSCENE);
		//SceneManager.LoadScene (0);
	}
}
