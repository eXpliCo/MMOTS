using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {
	public float scrollSpeed = 500;

	private Vector3 desiredPostion;
	public float xMin = -2500;
	public float xMax = 2500;
	public float yMin = 200;
	public float yMax = 370;
	public float zMin = -2500;
	public float zMax = 2500;

	void Start () {
		transform.position = new Vector3 (0, 350, 0);
		desiredPostion = transform.position;
	}

	// Update is called once per frame
	void Update () {

		float speed = scrollSpeed * Time.deltaTime;
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			transform.Translate (-touchDeltaPosition.x * speed, 0, -touchDeltaPosition.y * speed);
		}

		float x = 0, y = 0, z = 0;

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			x -= speed;
		} if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			x += speed;
		} if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			z -= speed;
		} if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			z += speed;
		}

		y -= Input.GetAxis ("Mouse ScrollWheel") * speed * 20;

		Vector3 move = new Vector3 (x, y, z) + desiredPostion;
		move.x = Mathf.Clamp (move.x, xMin, xMax);
		move.y = Mathf.Clamp (move.y, yMin, yMax);
		move.z = Mathf.Clamp (move.z, zMin, zMax);
		desiredPostion = move;
		transform.position = Vector3.Lerp(transform.position, desiredPostion, 0.2f);
	}
}
