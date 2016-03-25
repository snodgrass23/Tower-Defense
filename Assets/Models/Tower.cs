using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public string targetTag = "Enemy";
	public float turnSpeed;
	public float minDistance;
	public float maxDistance;
	public float reloadTime;

	public GameObject cannonExplosion;
	public Rigidbody cannonBall;
	public Transform cannonEnd;
	public GameObject turret;

	GameObject currentTarget;
	float timer = 100;
	float deltaAngle = 360f;

	//TODO find better way to handle power of shot
	float lobAngle = 7f;
	float powerMultiplyer = 27;
	

	void Update () {

		timer += Time.deltaTime;
		FindTarget ();

		if (currentTarget != null) {
			LookAtTarget ();

			if (timer >= reloadTime && deltaAngle < 10f)
				Fire ();
		}
	}

	void Fire () {
		timer = 0f;
		Rigidbody ballInstance = Instantiate (cannonBall, cannonEnd.position, cannonEnd.rotation) as Rigidbody;
		ballInstance.AddForce (ShotPower () * cannonEnd.forward, ForceMode.Acceleration);

		Instantiate (cannonExplosion, cannonEnd.position, cannonEnd.rotation);
	}

	float ShotPower () {
		float distanceToTarget = Vector3.Distance (currentTarget.transform.position, this.transform.position);
		return distanceToTarget * powerMultiplyer;
	}

	void LookAtTarget () {
		Vector3 direction = currentTarget.transform.position - turret.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Quaternion targetRotation = Quaternion.Euler (lookRotation.eulerAngles.x - lobAngle, lookRotation.eulerAngles.y, 0);
		turret.transform.rotation = Quaternion.Lerp (turret.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

		deltaAngle = Quaternion.Angle (targetRotation, turret.transform.rotation);
	}

	void FindTarget () {
		GameObject[] targets = GameObject.FindGameObjectsWithTag (targetTag);
		GameObject closest = null;

		float distance = Mathf.Infinity;
		Vector3 position = this.transform.position;

		foreach (GameObject go in targets) {
			float curDistance = Vector3.Distance(go.transform.position, position);
			if (curDistance < distance && curDistance > minDistance && curDistance < maxDistance) {
				closest = go;
				distance = curDistance;
			}
		}
		currentTarget = closest;
	}
}
