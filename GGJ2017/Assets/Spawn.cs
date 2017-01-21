using UnityEngine;


public class Spawn : MonoBehaviour {
	public GameObject enemy;
	public float spawnTime = 2f;

	
	void Start () {
		InvokeRepeating ("SpawnEnemy" , spawnTime, spawnTime);
	}

	void SpawnEnemy () {

		Transform path = GameObject.Find ("Path" + Random.Range (1, 1).ToString ()).transform;
		Instantiate (enemy, transform.position, transform.rotation, path.FindChild("Enemies"));
	
	}

}
