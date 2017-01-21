using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	public Transform[] traps;

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Clicker") {
			Debug.Log ("this time we are really doing it");
			foreach (Transform trap in traps) {
				trap.GetComponent<TriggerController> ().triggered = true;
			}
		}
	}
}
