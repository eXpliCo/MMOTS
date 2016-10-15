using UnityEngine;
using System.Collections;

public class BuilderScript : MonoBehaviour {

	public Camera mainCamera;
	public GameObject builderModel;
	public GameObject builderMenu;
	public Terrain terrain;

	public bool isBuilding = false;

	private Vector3 lastTouchDelta;
	private RTSCamera cameraScript;
	private BuildingMenuScript buildingMenuScript;

	// Use this for initialization
	private void Start () {
		cameraScript = (RTSCamera) mainCamera.GetComponent<RTSCamera> ();
		buildingMenuScript = (BuildingMenuScript)builderMenu.GetComponent<BuildingMenuScript> ();
		builderModel.SetActive (false);
	}

	public void ToggleBuilding() {
		isBuilding = !isBuilding;
		if (isBuilding) {
			lastTouchDelta = new Vector3 (0, 0, 0);
			cameraScript.StartBuild ();
			builderModel.SetActive (true);
			builderModel.transform.position = GetTerrainPos (new Vector2 (Screen.width / 2, Screen.height / 2));
			buildingMenuScript.ShowBuildingMenu ();
		} else {
			cameraScript.ShowGame ();
			builderModel.SetActive (false);
			buildingMenuScript.HideBuildingMenu ();
		}
	}

	private void Update () {
		if (isBuilding) {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastTouchDelta = builderModel.transform.position - GetTerrainPos(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    builderModel.transform.position = GetTerrainPos(Input.mousePosition) + lastTouchDelta;
                    builderModel.transform.position = new Vector3(builderModel.transform.position.x, terrain.SampleHeight(builderModel.transform.position), builderModel.transform.position.z);
                }
            }
            else
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    lastTouchDelta = builderModel.transform.position - GetTerrainPos(Input.GetTouch(0).position);
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    builderModel.transform.position = GetTerrainPos(Input.GetTouch(0).position) + lastTouchDelta;
                    builderModel.transform.position = new Vector3(builderModel.transform.position.x, terrain.SampleHeight(builderModel.transform.position), builderModel.transform.position.z);
                }
            }
		}
	}

	private Vector3 GetTerrainPos(Vector2 screenSpacePos) {
		RaycastHit[] hits;
		Ray ray = mainCamera.ScreenPointToRay (screenSpacePos);
		hits = Physics.RaycastAll (ray);
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			if (hit.transform.gameObject.tag == "Terrain") {
				return hit.point;
			}
		}
		return new Vector3 (0, 0, 0);
	}
}
