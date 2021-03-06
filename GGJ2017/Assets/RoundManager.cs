﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundManager : MonoBehaviour
{

    public RoundConfig[] roundConfigs;
    public Text roundText;

    private int currentRoundNumber = -1;
    private RoundConfig currentRound;
    private int activeCount = 0;
    private Spawn[] spawnPoints;
    private int spawnCount;
    private float spawnDelay;
    private float nextSpawnAt = 0;

	public bool gameStarted = false;
	private Vector3 originalSunScale;

	private Transform sun;
	public bool shouldScaleSun = false;
	bool resetting = false;

	void Start() {
		sun = GameObject.Find ("Environment").transform.Find ("SUN");
		originalSunScale = sun.localScale;
		StartCoroutine (StartWarmup (12f));
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			return;
		}

        roundText.text = "Wave " + (currentRoundNumber + 1);

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
		shouldScaleSun = true;
    }
		
	void Reset() {
		gameStarted = false;
		activeCount = 0;
		currentRoundNumber = -1;
		spawnCount = 0;
		currentRound = null;
		shouldScaleSun = false;
		sun.localScale = originalSunScale;
		sun.Find ("as2").GetComponent<AudioSource> ().Stop ();

		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			Destroy (enemy);
		}

		StartCoroutine (StartWarmup (3f));
	}

	IEnumerator StartWarmup(float warmupTime) {
		if (warmupTime > 10f) {
			sun.GetComponent<AudioSource> ().Play ();
		}
		yield return new WaitForSeconds(warmupTime);  //audiosource length?
		StartRound(0);
	}

	public void GameOver() {
		// enemy noses
		// show Game Over and reset buttons
		gameStarted = false;
		sun.Find("as2").GetComponent<AudioSource> ().Play();
		StartCoroutine(scaleSun());
		Invoke ("Reset", 8f);
	}

	IEnumerator scaleSun() {
		float scaleFactor = 1f;
		for (int i = 0; i < 1000; i++) {
			if (!shouldScaleSun) {
				i = 1000;
			}
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
