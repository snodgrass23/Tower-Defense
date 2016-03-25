using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject groundHit;
	public float maxLifetime = 5f;

	string hitLayer;

	private void Start() {
        InstanceDestroyer.Create(gameObject, 0.5f);
	}

	private void OnCollisionEnter(Collision otherObj) {
		hitLayer = LayerMask.LayerToName (otherObj.gameObject.layer);

		print ("hit " + otherObj.gameObject.name + " on layer " + hitLayer);
		if (hitLayer.Equals ("Damageable") || hitLayer.Equals ("Terrain"))
			blowUp ();
	}

	private void blowUp() {
		print ("blow up!");
		Instantiate (groundHit, this.transform.position, this.transform.rotation);
		Destroy (gameObject);
	}

	private void damageNearbyEnemies () {
		//TODO implement damage of nearby enemies
	}
}
