using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
	}

	public void backToMainMenu () {
		SceneManager.LoadScene (0);
	}

	public void applyOptions(Dropdown dropdownOption) {
		Screen.SetResolution (800,800,false);
		SceneManager.LoadScene (0);
		Debug.Log ("TEST");
		//SceneManager.LoadScene (0);
	}
}
