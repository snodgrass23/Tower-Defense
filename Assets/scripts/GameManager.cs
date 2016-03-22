using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject[] enemyTypes;
	public int timeBeforeFirstWave = 5;
	public int timeBetweenWaves = 30;
	public int startingWaveSize = 3;

	int waveNumber = 1;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", timeBeforeFirstWave, timeBetweenWaves);
	}

	void Spawn () {
		int numEnemies = startingWaveSize + waveNumber;

		GameObject enemyType = enemyTypes [Random.Range (0, enemyTypes.Length)];
		Transform spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];

		print("Starting wave " + waveNumber + " with " + numEnemies + " enemies of type: " + enemyType.name);

		//TODO instantiate at slightly different positions so they don't collide immmediately with each other
		for (int i = 0; i < numEnemies; i++) {
			Instantiate (enemyType, spawnPoint.position, spawnPoint.rotation);	
		}

		waveNumber++;
	}
}
