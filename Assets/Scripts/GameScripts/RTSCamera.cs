using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {
	private const float CAMERA_TRANSITION_SPEED = 3.0f;
	enum CameraState {Game, Menu};

	public GameObject mainMenuGameObject;

	public float scrollSpeed = 500;

	public float xMin = -2500;
	public float xMax = 2500;
	public float yMin = 200;
	public float yMax = 370;
	public float zMin = -2500;
	public float zMax = 2500;
	public Quaternion defaultRotation;

	private Vector3 desiredPostion;
	private CameraState cameraState;
	private Quaternion cameraDesiredRotation;

	void Start () {
		cameraState = CameraState.Game;

		transform.position = new Vector3 (0, 350, 0);
		desiredPostion = transform.position;

		defaultRotation = Quaternion.AngleAxis(50, new Vector3(1, 0, 0));
		Debug.Log (defaultRotation);
		transform.rotation = defaultRotation;

		showMenu ();
	}

	private void showMenu() {
		cameraState = CameraState.Menu;

		mainMenuGameObject.SetActive (true);
		mainMenuGameObject.transform.position = transform.position + new Vector3(260, 0, 350);

		cameraDesiredRotation = Quaternion.LookRotation(mainMenuGameObject.transform.position - transform.position);
	}

	private void showGame() {
		cameraState = CameraState.Game;

		transform.position = new Vector3 (transform.position.x, 350, transform.position.z);
		desiredPostion = transform.position;
		cameraDesiredRotation = defaultRotation;
	}

	private bool cameraAnimationDone() {
		return (transform.rotation == cameraDesiredRotation);
	}

	void Update () {
		if (cameraState == CameraState.Game && cameraAnimationDone()) {
			if (mainMenuGameObject.activeSelf) {
				mainMenuGameObject.SetActive (false);
			}

			float speed = scrollSpeed * Time.deltaTime;
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				transform.Translate (-touchDeltaPosition.x * speed, 0, -touchDeltaPosition.y * speed);
			}

			float x = 0, y = 0, z = 0;

			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
				x -= speed;
			}
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
				x += speed;
			}
			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
				z -= speed;
			}
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				z += speed;
			}
			if (Input.GetKeyDown (KeyCode.Escape)) {
				showMenu ();
			}

			y -= Input.GetAxis ("Mouse ScrollWheel") * speed * 20;

			desiredPostion = new Vector3 (x, y, z) + desiredPostion;
		} 
		else if(cameraState == CameraState.Menu) 
		{
			if (Input.GetKeyDown (KeyCode.Escape)) {
				showGame ();
			}
		}
		updateTransform ();
	}

	private void updateTransform() {
		updatePosition ();
		updateRotation ();
	}

	private void updatePosition() {
		desiredPostion.x = Mathf.Clamp (desiredPostion.x, xMin, xMax);
		desiredPostion.y = Mathf.Clamp (desiredPostion.y, yMin, yMax);
		desiredPostion.z = Mathf.Clamp (desiredPostion.z, zMin, zMax);
		transform.position = Vector3.Lerp (transform.position, desiredPostion, 0.05f);
	}

	private void updateRotation() {
		transform.rotation = Quaternion.Slerp (transform.rotation, cameraDesiredRotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
	}
}
