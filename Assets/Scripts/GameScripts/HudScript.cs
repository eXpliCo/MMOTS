using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {
	public GameObject builder;

	private BuilderScript builderScript;

	private void Start() {
		builderScript = (BuilderScript) builder.GetComponent<BuilderScript> ();
	}

	public void PressRailroad() {
		builderScript.ToggleBuilding ();
	}
}
