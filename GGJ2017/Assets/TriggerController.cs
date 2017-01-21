using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	public float triggerForce = 1000f;
	public Rigidbody triggerRB;

	private bool forceTrapButtonPressed = false;

	void Start() {
		triggerRB = GameObject.Find("ForceTrap").GetComponent<Rigidbody> ();
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Enemy" && Input.GetKeyDown(KeyCode.Space)) {
			// do the thing
			Debug.Log("doing the thing");
			col.gameObject.GetComponent<Rigidbody> ().AddExplosionForce(triggerForce, new Vector3(triggerRB.transform.position.x, triggerRB.transform.position.y - 0.5f, triggerRB.transform.position.z), 10f);
		}
	}
}
