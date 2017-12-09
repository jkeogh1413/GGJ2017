using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spawn : MonoBehaviour {
	public GameObject enemy;
	public float spawnTime = 2f;
	public Transform[] eligiblePath;

	//sets random path for each spawn point

	public void SpawnEnemy () {

		int l = eligiblePath.Length;
		int pathIndex = Random.Range(0, l);
		Transform path = eligiblePath[pathIndex];
		Instantiate (enemy, transform.position, transform.rotation, path.Find("Enemies"));
	}

}



