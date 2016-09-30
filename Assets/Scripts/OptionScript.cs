using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour {

	public void BackToMainMenu ()
	{
		SceneManager.LoadScene (0);
	}
}
