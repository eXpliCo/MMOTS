using UnityEngine;
using System.Collections;

public class GameWorldScriptHud : MonoBehaviour {

	public GameObject[] allGroups;

	public void toggleButtons(GameObject buttonGroup) 
	{
		foreach (GameObject closeGroup in allGroups) {
			closeGroup.SetActive (false);
		}

		bool setStatus = !buttonGroup.activeSelf;
		buttonGroup.SetActive(setStatus);
	}

}
