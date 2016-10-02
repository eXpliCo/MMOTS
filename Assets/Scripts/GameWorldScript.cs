using UnityEngine;
using System.Collections;

public class GameWorldScript : MonoBehaviour {

	public void ToggleButtons(Button[] buttons) 
	{
		for (int i = 0; i < buttons.Length; i++) 
		{
			Button but = buttons [i];
			but.SetActive (but.Active);
		}
	}
}
