using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	
	public void LoadScene(int level)
	{
		SceneManager.LoadScene (level);
	}

	public void Quit() {
		Application.Quit ();
	}
}
