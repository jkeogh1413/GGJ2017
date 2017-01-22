using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	Transform waypoints;
	Transform player;

	int curWaypoint = 1;
	float walkSpeed = 1.5f;
	bool done = false;

	public string state = "neutral";
    private string previousState;

    private RoundManager roundManager;
    private EnemyGetsWavedAt getsWavedAtBehavior;
    public Animator animator;

	public AudioSource enemyAudio;

	private bool ready = false;

	// Use this for initialization
	void Awake () {
		// Based on spawn, choose a path, and become child of Path.Enemies

		waypoints = transform.parent.parent.FindChild ("Waypoints");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        getsWavedAtBehavior = GetComponent<EnemyGetsWavedAt>();
        GameObject roundManagerGO = GameObject.Find("RoundManager");
        roundManager = roundManagerGO.GetComponent<RoundManager>();

		enemyAudio = GetComponent<AudioSource> ();
		enemyAudio.enabled = true;

        SetState("neutral");
	}

	// Update is called once per frame
	void Update () {
		if (done) {
			return;
		}

        if (getsWavedAtBehavior.IsBeingWavedAt())
        {
            transform.LookAt (player);
			if (state == "neutral") {
				SetState("happy1");
			} else if (state == "sad1" || state == "sad2") {
				SetState("happy2");
			}
		} else if (transform.position.y > 2.5f) {
			Destroy (gameObject, 5f);
			SetState("flying");
			done = true;
            roundManager.DudeDied();
		} else if (transform.position.y < 1f) {
			SetState("drowning");
			done = true;
			roundManager.DudeDied();
		} else
        {
            Transform curWaypointTransform = waypoints.FindChild ("Waypoint" + curWaypoint.ToString ());
			transform.LookAt (curWaypointTransform);
			transform.Translate (Vector3.forward * walkSpeed * Time.deltaTime);

			if (state == "happy2") {
				SetState("sad2");
			} else if (state == "happy1") {
				SetState("sad1");
			}

			if (Vector3.Distance (transform.position, curWaypointTransform.position) < 0.1f) {
				if (curWaypoint < waypoints.childCount) {
					curWaypoint++;
				}
			}
		}
        Debug.Log(gameObject.GetHashCode() + " state is " + state);
	}

    void SetState(string newState)
    {
        state = newState;
        if (state != previousState)
        {
            switch(state)
            {
                case "neutral":
                    triggerSound("neutral");
                    animator.SetTrigger("startWalking");
                    animator.Play("walk");
                    break;
                case "happy1":
                    triggerSound(state);
                    animator.SetTrigger("startWaving");
                    animator.Play("wave");
                    break;
                case "sad1":
                    triggerSound(state);
                    animator.SetTrigger("startWalking");
                    animator.Play("walk");
                    break;
                case "happy2":
                    triggerSound(state);
                    animator.SetTrigger("startWaving");
                    animator.Play("wave");
                    break;
                case "sad2":
                    triggerSound(state);
                    animator.SetTrigger("startWalking");
                    animator.Play("walk");
                    break;
                case "flying":
                    triggerSound(state);
                    animator.SetTrigger("die");
                    animator.Play("fetal");
                    break;
                case "drowning":
                    triggerSound(state);
                    animator.SetTrigger("die");
                    animator.Play("fetal");
                    break;
            }
        }
    }

	public void triggerSound(string category) {

		Transform soundGroup = GameObject.Find ("EnemyAudio").transform.FindChild (category [0].ToString ().ToUpper () + category.Substring (1));
		AudioSource audioSource = soundGroup.GetChild (Random.Range (0, soundGroup.childCount)).GetComponent<AudioSource> ();

		enemyAudio = GetComponent<AudioSource> ();
		enemyAudio.enabled = true;
        enemyAudio.clip = audioSource.clip;
        enemyAudio.Play();
    }
}
