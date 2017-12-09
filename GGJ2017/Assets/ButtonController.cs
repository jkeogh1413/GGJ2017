using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	public Transform[] traps;
	public float cooldownTime = 3f;
	bool ready = true;

	Material buttonOn;
	Material buttonOff;

	void Start() {
		buttonOn = Resources.Load ("Materials/TransparentGreen") as Material;
		buttonOff = Resources.Load ("Materials/TransparentRed") as Material;
	}

	IEnumerator Cooldown() {
		yield return new WaitForSeconds (cooldownTime);
		gameObject.GetComponent<Renderer> ().material = buttonOn;
		ready = true;
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Clicker" && ready) {
			//Debug.Log ("this time we are really doing it");
			foreach (Transform trap in traps) {
				trap.GetComponent<TriggerController> ().triggered = true;
			}
			ready = false;
			gameObject.GetComponent<Renderer> ().material = buttonOff;
			//GameObject.Find ("springTrap").GetComponent<Animator> ();
			StartCoroutine (Cooldown ());
		}
	}
}
