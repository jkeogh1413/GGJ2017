using UnityEngine;


public class KeyboardMovement : MonoBehaviour {
	private Rigidbody rb;

	public float speed = 4F;
	private Vector3 moveDirection = Vector3.zero;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}
		
	void FixedUpdate() {
		move ();
	}

	void move () {
		float xMov = Input.GetAxisRaw ("Horizontal");
		float zMov = Input.GetAxisRaw ("Vertical");

		Vector3 xDirection = transform.right * xMov;
		Vector3 zDirection = transform.forward * zMov;

		Vector3 moveVector = (xDirection + zDirection).normalized * speed;

		if (moveVector != Vector3.zero) {
			rb.MovePosition (rb.position + moveVector * Time.fixedDeltaTime);
		}
	}

}

