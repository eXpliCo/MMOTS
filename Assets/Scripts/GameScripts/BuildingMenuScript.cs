using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

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
		foreach (var part in buildingHistory) {
			Destroy (part);
		}
		buildingHistory = new List<GameObject> ();
	}

	public void buildPart() {
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = cube.transform.position;
		sphere.transform.rotation = cube.transform.rotation;
		sphere.transform.localScale = new Vector3(50, 50, 50);
		buildingHistory.Add(sphere);
	}

	public void undo() {
		if (buildingHistory.Count > 0) {
			Destroy (buildingHistory [buildingHistory.Count - 1]);
			buildingHistory.RemoveAt (buildingHistory.Count - 1);
		} else {
			
		}
	}

	public void saveRailroad() {
        var json = new JSONClass();
        json["method"] = "BuildRails";
        var jsonList = new JSONArray();
        int i = 0;
        foreach (var part in buildingHistory)
        {
            var jsonObject = new JSONClass();
            jsonObject["x"] = part.gameObject.transform.position.x.ToString();
            jsonObject["y"] = part.gameObject.transform.position.y.ToString();
            jsonObject["z"] = part.gameObject.transform.position.z.ToString();
            jsonList.Add(jsonObject);
            i++;
        }
        json["parts"] = jsonList;
        SocketClient.SendMessage(json.ToString());
        //buildingHistory = new List<GameObject>();
    }

	// Update is called once per frame
	void Update () {
		buildingMenu.transform.position = cube.transform.position + new Vector3 (0, 100, 0);
	}
}
