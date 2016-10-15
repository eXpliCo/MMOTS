using UnityEngine;
using System.Collections;

public class PlaceholderController : MonoBehaviour {

	private Material material;
	public bool canBuild;
	private int collisionCounter;

	void Start () {
		material = gameObject.GetComponent<MeshRenderer> ().material;
		canBuild = true;
		collisionCounter = 0;
	}

	void OnEnable () {
		collisionCounter = 0;
		canBuild = true;
		material.color = Color.grey;
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Placeholder")) 
		{
			material.color = Color.red;
			canBuild = false;
			collisionCounter++;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Placeholder")) {
			collisionCounter--;
			if (collisionCounter <= 0) {
				material.color = Color.grey;
				canBuild = true;
			}
		}
	}
}
