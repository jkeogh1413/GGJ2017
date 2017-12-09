using UnityEngine;


public class MouseExtend : MonoBehaviour {
	private Camera cam;

	public float moveDistance = 8f;
	private float distanceMoved = 0f;
	private float distanceMin = 0f;
	private float distanceMax = 24f;

	private Transform hand;

	void Start() {
		cam = GetComponentInChildren<Camera> ();

		hand = cam.transform.Find ("Hand");
	}

	void extend () {


		// Extend hand with scroll
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (distanceMoved < distanceMax) {
				hand.position += cam.transform.forward * moveDistance * Time.deltaTime;
				distanceMoved += moveDistance;
			}
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (distanceMoved > distanceMin) {
				hand.position -= cam.transform.forward * moveDistance * Time.deltaTime;
				distanceMoved -= moveDistance;
			}
		}
	}

	void FixedUpdate() {
		extend();
	}
}
