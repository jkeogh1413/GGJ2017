using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	Transform waypoints;

	int curWaypoint = 1;
	float walkSpeed = 1.5f;
	bool done = false;

	// Use this for initialization
	void Start () {
		// Based on spawn, choose a path, and become child of Path.Enemies

		waypoints = transform.parent.parent.FindChild ("Waypoints");
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y > 4f || transform.position.y < -1f) {
			Destroy (gameObject, 5f);
			done = true;
		} else if (!done) {
			Transform curWaypointTransform = waypoints.FindChild ("Waypoint" + curWaypoint.ToString ());
			transform.LookAt (curWaypointTransform);
			transform.Translate (Vector3.forward * walkSpeed * Time.deltaTime);

			if (Vector3.Distance (transform.position, curWaypointTransform.position) < 0.1f) {
				if (curWaypoint < waypoints.childCount) {
					curWaypoint++;
				}
			}
		}
	}
}
