using UnityEngine;
using System.Collections;

public class PathController : MonoBehaviour {

	Transform enemies;
	Transform waypoints;

	int curWaypoint = 1;
	float walkSpeed = 1.5f;

	// Use this for initialization
	void Start () {
		enemies = transform.FindChild ("Enemies");
		waypoints = transform.FindChild ("Waypoints");
	}
	
	// Update is called once per frame
	void Update () {
		Transform curWaypointTransform = waypoints.FindChild ("Waypoint" + curWaypoint.ToString ());
		foreach (Transform enemy in enemies) {
			//enemy.position = Vector3.Lerp (enemy.position, curWaypointTransform.position, 0.5f * Time.deltaTime);
			enemy.LookAt(curWaypointTransform);
			enemy.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
		}
	}
}
