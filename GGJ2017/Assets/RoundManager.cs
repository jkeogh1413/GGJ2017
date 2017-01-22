using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoundManager : MonoBehaviour
{

    public RoundConfig[] roundConfigs;

    private int currentRoundNumber = -1;
    private RoundConfig currentRound;
    private int activeCount = 0;
    private Spawn[] spawnPoints;
    private int spawnCount;
    private float spawnDelay;
    private float nextSpawnAt = 0;

	private bool gameStarted = false;

	private Transform sun;

	void Start() {
		sun = GameObject.Find ("Environment").transform.FindChild ("SUN");
		StartCoroutine (StartWarmup ());
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			return;
		}

        if (spawnCount > 0)
        {
            if (Time.time >= nextSpawnAt)
            {
                SpawnADude();
            }
        }

        // killed them all, start next round
        if (activeCount == 0)
        {
            int nextRoundIndex = currentRoundNumber + 1;
            if (nextRoundIndex > roundConfigs.Length - 1)
            {
                Debug.Log("YOU WINN!!!");
            }
            else
            {
                StartRound(nextRoundIndex);
            }
        }
	}

    void StartRound (int roundNumber)
    {
		gameStarted = true;
        Debug.Log("Starting round " + roundNumber);
        currentRoundNumber = roundNumber;
        currentRound = roundConfigs[currentRoundNumber];
        spawnPoints = currentRound.spawnPoints;
        spawnCount = currentRound.spawnCount;
        spawnDelay = currentRound.spawnDelay;
        nextSpawnAt = Time.time;
    }
		
	void Reset() {
		gameStarted = false;
		activeCount = 0;
		currentRoundNumber = -1;
		spawnCount = 0;
		currentRound = null;

		StartRound (0);
	}

	IEnumerator StartWarmup() {
		sun.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds(12f);  //audiosource length?
		StartRound(0);
	}

	public void GameOver() {
		// enemy noses
		// show Game Over and reset buttons
		sun.FindChild("as2").GetComponent<AudioSource> ().Play();

		StartCoroutine(scaleSun());
		return;
	}

	IEnumerator scaleSun() {
		float scaleFactor = 1f;
		for (int i = 0; i < 1000; i++) {
			sun.localScale += Vector3.one * scaleFactor;
			yield return 0;
		}
	}

    void SpawnADude ()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length - 1);
        Spawn spawnPoint = spawnPoints[spawnPointIndex];
        spawnPoint.SpawnEnemy();
        nextSpawnAt = Time.time + spawnDelay;
        spawnCount -= 1;
        activeCount += 1;
    }

    public void DudeDied()
    {
        activeCount -= 1;
    }
}
