using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWaveDetection : MonoBehaviour {
    
    public Transform head;
    public bool isWaving = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool aboveHead = transform.position.y - head.position.y > 0;
        isWaving = aboveHead;
        Color debugColor = Color.white;
        if (isWaving)
        {
            debugColor = Color.blue;
        }
        Debug.DrawRay(transform.position, -2.0f * transform.up, debugColor);
    }
}
