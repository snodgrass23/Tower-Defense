using UnityEngine;
using System.Collections;

public class CannonBallExplosion : MonoBehaviour {

//	public LayerMask damagableLayer;
	public GameObject shotExplosion;
	public float maxLifetime = 5f;


	AudioSource explosionAudio;
	string hitLayer;

	private void Start() {
		Destroy(gameObject, maxLifetime);
	}

	void Awake () {
		explosionAudio = GetComponent<AudioSource> ();	
	}

	private void OnCollisionEnter(Collision otherObj) {
		hitLayer = LayerMask.LayerToName (otherObj.gameObject.layer);

		if (hitLayer.Equals ("Damageable") || hitLayer.Equals ("Terrain")) {
			print ("Cannonbal hit a " + otherObj.gameObject.name);
			blowUp ();
		}
	}

	private void blowUp() {
		explosionAudio.Play ();
		shotExplosion.transform.parent = null;
		foreach (var effect in shotExplosion.GetComponentsInChildren<ParticleSystem> ()) {
			effect.Play ();
		}

		Destroy (shotExplosion.gameObject, shotExplosion.GetComponentInChildren<ParticleSystem> ().duration);
		Destroy (gameObject);

		GetComponent<MeshRenderer> ().enabled = false;
	}
}
