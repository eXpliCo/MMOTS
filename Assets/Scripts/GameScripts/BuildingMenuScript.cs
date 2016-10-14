using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingMenuScript : MonoBehaviour {

	public GameObject cube;
	public GameObject buildingMenu;

	private List<GameObject> buildingHistory;

	// Use this for initialization
	void Start () {
		buildingHistory = new List<GameObject> ();
		buildingMenu.SetActive (false);
	}

	public void showBuildingMenu() {
		buildingMenu.transform.position = cube.transform.position + new Vector3 (0, 100, 0);
		buildingMenu.transform.rotation = Camera.main.transform.rotation;
		buildingMenu.SetActive (true);
	}

	public void hideBuildingMenu() {
		buildingMenu.SetActive (false);
	}

	public void buildPart() {
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = cube.transform.position;
		sphere.transform.rotation = cube.transform.rotation;
		sphere.transform.localScale = new Vector3(50, 50, 50);
		buildingHistory.Add(sphere);
	}

	// Update is called once per frame
	void Update () {
		buildingMenu.transform.position = cube.transform.position + new Vector3 (0, 100, 0);
	}
}
