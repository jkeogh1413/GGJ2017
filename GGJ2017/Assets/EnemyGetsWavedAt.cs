using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetsWavedAt : MonoBehaviour {

    private bool isBeingWavedAt = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isBeingWavedAt)
        {
            Debug.Log(gameObject.GetHashCode() + " likes being waved at.");
        }
        isBeingWavedAt = false;
	}

    public void GetWavedAt()
    {
        isBeingWavedAt = true;
    }

    public bool IsBeingWavedAt()
    {
        return isBeingWavedAt;
    }

}
