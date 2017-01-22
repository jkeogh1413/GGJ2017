using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy") {
			GameObject.Find ("RoundManager").GetComponent<RoundManager> ().GameOver ();
		}
	}
}
