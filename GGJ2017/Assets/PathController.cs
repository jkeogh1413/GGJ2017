using UnityEngine;
using System.Collections;

public class PathController : MonoBehaviour {

	Transform enemies;
	Transform waypoints;

	int curWaypoint = 1;
	float walkSpeed = 1.5f;

	// Use this for initialization
	void Start () {
		enemies = transform.Find ("Enemies");
		waypoints = transform.Find ("Waypoints");
	}
	
	// Update is called once per frame
	void Update () {
		Transform curWaypointTransform = waypoints.Find ("Waypoint" + curWaypoint.ToString ());
		foreach (Transform enemy in enemies) {
			enemy.LookAt(curWaypointTransform);
			enemy.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
		}
	}
}
