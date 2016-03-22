using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public string targetTag = "Player";
	public float turnSpeed;
	public float minDistance;
	public float maxDistance;
	public float reloadTime;
	public GameObject shotExplosion;

	public Rigidbody cannonBall;
	public Transform cannon;

	GameObject currentTarget;
	float timer;
	AudioSource shotAudio;
	float deltaAngle;
	float lobAngle = 2.5f;
	float powerMultiplyer = 155;

	void Awake () {
		shotAudio = GetComponent<AudioSource> ();	
//		currentTarget = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		FindTarget ();

		if (currentTarget != null) {
			LookAtTarget ();

			if (timer >= reloadTime && deltaAngle < 15f)
				Fire ();
		}
	}

	void Fire () {
		timer = 0f;
		Rigidbody ballInstance = Instantiate (cannonBall, cannon.position, cannon.rotation) as Rigidbody;
		ballInstance.AddForce (ShotPower () * cannon.forward, ForceMode.Acceleration);
		shotAudio.Play ();

		foreach (var effect in shotExplosion.GetComponentsInChildren<ParticleSystem> ()) {
			effect.Play ();
		}
	}

	float ShotPower () {
		float distanceToTarget = Vector3.Distance (currentTarget.transform.position, this.transform.position);
		return distanceToTarget * powerMultiplyer;
	}

	void LookAtTarget () {
		Vector3 direction = currentTarget.transform.position - this.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Quaternion targetRotation = Quaternion.Euler (lookRotation.eulerAngles.x - lobAngle, lookRotation.eulerAngles.y, 0);
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

		deltaAngle = Quaternion.Angle (targetRotation, this.transform.rotation);
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
