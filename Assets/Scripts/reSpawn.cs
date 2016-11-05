using UnityEngine;
using System.Collections;

public class reSpawn : MonoBehaviour {
	public Transform[] spawnPoints;
	public float spawnTime = 1.5f;
	// Use this for initialization

	void Start () {
		spawnPoints = new Transform[2];
		//spawnPoints[0]= GameObject.FindGameObjectsWithTag("sp1");
		//spawnPoints[1]= GameObject.FindGameObjectsWithTag("sp2");

		InvokeRepeating ("SpawnEnemy", spawnTime, spawnTime);


	}

	// Update is called once per frame
	void Update () {

	}

	void SpawnEnemy(){
		//GameObject obj = GameObject.FindGameObjectWithTag("Freshman_student");
		//int spawnIndex = Random.Range (0, spawnPoints.Length);
		//Instantiate (obj, spawnPoints [spawnIndex].position, spawnPoints [spawnIndex].rotation);
	}
}