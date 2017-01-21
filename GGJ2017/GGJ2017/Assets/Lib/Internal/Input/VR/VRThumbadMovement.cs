using UnityEngine;
using System.Collections;

public class VRThumbadMovement : MonoBehaviour {

	public GameObject player;
	public GameObject cam;
	private GameObject guideObject;

	[Tooltip("If true, pressing touchpad will follow controller direction.  If false, will follow camera direction.")]
	public bool followController = false;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private Valve.VR.EVRButtonId padDown = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	private float movementSpeed = 3f;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		if (followController) {
			guideObject = gameObject;
		} else {
			guideObject = cam;
		}
	}
	// Update is called once per frame
	void Update () {
		if (controller.GetPress(padDown)) {
			Vector3 movementVector = new Vector3 (guideObject.transform.forward.x, 0, guideObject.transform.forward.z);
			player.transform.position += movementVector * movementSpeed * Time.deltaTime;
		}
	
	}
}
