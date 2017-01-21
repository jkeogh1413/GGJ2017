using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaveAtBehavior : MonoBehaviour {

    public ControllerWaveDetection[] controllers;

    private bool waving = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        waving = AreWeWaving();
        if (waving)
        {
            Debug.Log("Waving...");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyGetsWavedAt egwa = other.GetComponent<EnemyGetsWavedAt>();
        if (waving && egwa)
        {
            Debug.Log("Waving at " + other.GetHashCode());
            egwa.GetWavedAt();
        }
    }

    bool AreWeWaving ()
    {
        foreach (ControllerWaveDetection c in controllers)
        {
            if (c.isWaving)
            {
                return true;
            }
        }
        return false;
    }
}
