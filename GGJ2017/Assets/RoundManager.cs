using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoundManager : MonoBehaviour
{

    public RoundConfig[] roundConfigs;

    private int currentRoundNumber = 0;
    private RoundConfig currentRound;
    private int activeCount = 0;
    private Spawn[] spawnPoints;
    private int spawnCount;
    private float spawnDelay;
    private float nextSpawnAt = 0;

    // Use this for initialization
    void LateStart () {
        // start immediately for now
        StartRound(0);
	}
	
	// Update is called once per frame
	void Update () {
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
        currentRoundNumber = roundNumber;
        currentRound = roundConfigs[currentRoundNumber];
        spawnPoints = currentRound.spawnPoints;
        spawnCount = currentRound.spawnCount;
        spawnDelay = currentRound.spawnDelay;
        nextSpawnAt = Time.time;
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
