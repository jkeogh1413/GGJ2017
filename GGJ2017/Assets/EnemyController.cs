using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	Transform waypoints;

	int curWaypoint = 1;
	float walkSpeed = 1.5f;
	bool done = false;
    bool isWalking = false;

    private RoundManager roundManager;
    private EnemyGetsWavedAt getsWavedAtBehavior;

	// Use this for initialization
	void Start () {
		// Based on spawn, choose a path, and become child of Path.Enemies

		waypoints = transform.parent.parent.FindChild ("Waypoints");
        getsWavedAtBehavior = GetComponent<EnemyGetsWavedAt>();
        GameObject roundManagerGO = GameObject.Find("RoundManager");
        roundManager = roundManagerGO.GetComponent<RoundManager>();
	}

	// Update is called once per frame
	void Update () {
        if (getsWavedAtBehavior.IsBeingWavedAt())
        {
            // Do some thing
        } else if (transform.position.y > 4f || transform.position.y < -1f) {
			Destroy (gameObject, 5f);
			done = true;
            roundManager.DudeDied();
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
