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

	public void toggleBuilding() {
		isBuilding = !isBuilding;
		if (isBuilding) {
			lastTouchDelta = new Vector3 (0, 0, 0);
			cameraScript.startBuild ();
			builderModel.SetActive (true);
			builderModel.transform.position = getTerrainPos (new Vector2 (Screen.width / 2, Screen.height / 2));
			buildingMenuScript.showBuildingMenu ();
		} else {
			cameraScript.showGame ();
			builderModel.SetActive (false);
			buildingMenuScript.hideBuildingMenu ();
		}
	}

	private void Update () {
		if (isBuilding) {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				lastTouchDelta = builderModel.transform.position - getTerrainPos(Input.GetTouch (0).position);
			}
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				builderModel.transform.position = getTerrainPos(Input.GetTouch (0).position) + lastTouchDelta;
				builderModel.transform.position = new Vector3(builderModel.transform.position.x, terrain.SampleHeight (builderModel.transform.position), builderModel.transform.position.z);
			}
			/*if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition + currentBuildBlock;
				touchDeltaPosition.x = Mathf.Clamp (touchDeltaPosition.x, 0, Screen.width);
				touchDeltaPosition.y = Mathf.Clamp (touchDeltaPosition.y, 0, Screen.height);
				currentBuildBlock = touchDeltaPosition;
			}

			builderModel.transform.position = getTerrainPos(currentBuildBlock);
			*/

		}
	}

	private Vector3 getTerrainPos(Vector2 screenSpacePos) {
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
