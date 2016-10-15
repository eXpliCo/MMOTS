using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {
	private const float CAMERA_ROTATION_SPEED = 3.0f;
	private const float CAMERA_MOVESPEED_SPEED = 0.05f;
	enum CameraState {Game, Menu, Build};


	private float scrollSpeed = 10.0f;

	public Terrain terrain;

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
		transform.rotation = defaultRotation;
	}

	public void hideGame(Vector3 mainMenuPosition) {
		cameraState = CameraState.Menu;
		cameraDesiredRotation = Quaternion.LookRotation(mainMenuPosition - transform.position);
	}

	public void showGame() {
		cameraState = CameraState.Game;
		transform.position = new Vector3 (transform.position.x, 350, transform.position.z);
		desiredPostion = transform.position;
		cameraDesiredRotation = defaultRotation;
	}

	public void startBuild() {
		cameraState = CameraState.Build;
	}

	void Update () {
		if (cameraState == CameraState.Game) {
			float speed = scrollSpeed * Time.deltaTime;
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                float x = 0, y = 0, z = 0;

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    x -= speed;
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    x += speed;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    z -= speed;
                }
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    z += speed;
                }

                y -= Input.GetAxis("Mouse ScrollWheel") * speed * 20;

                float desktopMultiplier = 100.0f;
                x *= desktopMultiplier;
                y *= desktopMultiplier;
                z *= desktopMultiplier;
                desiredPostion = (new Vector3(x, y, z) + desiredPostion);
            }
            else
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    float cameraHeight = transform.position.y - terrain.SampleHeight(transform.position);
                    transform.Translate(-touchDeltaPosition.x * speed, 0, -touchDeltaPosition.y * speed);
                    transform.position = new Vector3(transform.position.x, terrain.SampleHeight(transform.position) + cameraHeight, transform.position.z);
                    desiredPostion = transform.position;
                }
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
		transform.position = Vector3.Lerp (transform.position, desiredPostion, CAMERA_MOVESPEED_SPEED);
	}

	private void updateRotation() {
		transform.rotation = Quaternion.Slerp (transform.rotation, cameraDesiredRotation, CAMERA_ROTATION_SPEED * Time.deltaTime);
	}
}
