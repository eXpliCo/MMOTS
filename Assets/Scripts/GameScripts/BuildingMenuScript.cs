using UnityEngine;
using System.Collections;

public class BuildingMenuScript : MonoBehaviour {

	public GameObject cube;
	public GameObject buildingMenu;

	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update () {
		buildingMenu.transform.position = cube.transform.position + new Vector3 (0, 100, 0);
	}
}
