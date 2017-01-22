﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	Transform waypoints;
	Transform player;

	int curWaypoint = 1;
	float walkSpeed = 1.5f;
	bool done = false;
    bool isWalking = false;

	public string state = "neutral";
	private bool hasBeenHappy = false;
	private bool hasBeenSad = false;

    private RoundManager roundManager;
    private EnemyGetsWavedAt getsWavedAtBehavior;

	// Use this for initialization
	void Start () {
		// Based on spawn, choose a path, and become child of Path.Enemies

		waypoints = transform.parent.parent.FindChild ("Waypoints");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        getsWavedAtBehavior = GetComponent<EnemyGetsWavedAt>();
        GameObject roundManagerGO = GameObject.Find("RoundManager");
        roundManager = roundManagerGO.GetComponent<RoundManager>();
	}

	// Update is called once per frame
	void Update () {
        if (getsWavedAtBehavior.IsBeingWavedAt())
        {
			transform.LookAt (player);
			if (!hasBeenHappy) {
				state = "happy1";
				//triggerSound (state);
				hasBeenHappy = true;
			} else if (hasBeenSad) {
				state = "happy2";
				//triggerSound (state);
			}
		} else if (transform.position.y > 4f) {
			Destroy (gameObject, 5f);
			//triggerSound ("flying");
			done = true;
            roundManager.DudeDied();
		} else if (transform.position.y < 1f) {
			//triggerSound ("drowning");
			done = true;
			roundManager.DudeDied();
		} else if (!done) {
			Transform curWaypointTransform = waypoints.FindChild ("Waypoint" + curWaypoint.ToString ());
			transform.LookAt (curWaypointTransform);
			transform.Translate (Vector3.forward * walkSpeed * Time.deltaTime);

			if (hasBeenHappy && hasBeenSad) {
				state = "sad2";
				//triggerSound (state);
			} else if (hasBeenHappy) {
				state = "sad1";
				//triggerSound (state);
			}

			if (Vector3.Distance (transform.position, curWaypointTransform.position) < 0.1f) {
				if (curWaypoint < waypoints.childCount) {
					curWaypoint++;
				}
			}
		}
	}

	public void triggerSound(string category) {
		Transform soundGroup = GameObject.Find ("EnemyAudio").transform.FindChild (category [0].ToString ().ToUpper () + category.Substring (1));
		AudioSource audioSource = soundGroup.GetChild (Random.Range (0, soundGroup.childCount)).GetComponent<AudioSource> ();
		audioSource.Play ();
	}
}
