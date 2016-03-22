using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;

	float timeBetweenFindingTarget = 1f;
	float timer;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <NavMeshAgent> ();
		nav.SetDestination (player.position);
	}


	void Update () {
		timer += Time.deltaTime;

		if (timer > timeBetweenFindingTarget) {
			timer = 0f;
			SetTarget ();
		}
	}

	void SetTarget () {
		//TODO update to choose to attack towers, etc
		nav.SetDestination (player.position);
	}

}
