using UnityEngine;
using System.Collections;

public class GameWorldScript : MonoBehaviour {

	public void ToggleButtons(GameObject buttonGroup) 
	{
		
		bool setStatus = !buttonGroup.activeSelf;
		buttonGroup.SetActive(setStatus);
	}

}
