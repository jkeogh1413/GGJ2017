using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy" && col.transform.position.y > 0f && !col.GetComponent<EnemyController> ().done) {
			if (GameObject.Find ("RoundManager").GetComponent<RoundManager> ().gameStarted) {
				GameObject.Find ("RoundManager").GetComponent<RoundManager> ().GameOver ();
			}
		}
	}
}
