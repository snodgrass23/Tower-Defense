using UnityEngine;

public class DamageableObject : MonoBehaviour {

    public GameObject hitParticles;
    public int startingHitPoints = 0;
    
    Health health = null;
	AudioSource hurtAudio;

    public void Start () {
        health = new Health(startingHitPoints);
		hurtAudio = gameObject.GetComponent<AudioSource> ();
    }

	public void ApplyHit (int damage, float explosionForce, Vector3 explosionPosition, float explosionRadius) {
		TriggerHitParticles (explosionPosition);

		if (CanReceiveDamage ()) {
			TakeDamage (damage);
			ReceiveExplosiveForce (explosionForce, explosionPosition, explosionRadius);

			if (health.IsDead ())
				DestroyObject ();
		}
	}
    
	public void TriggerHitParticles (Vector3 explosionPosition) {
		if (hitParticles == null)
			return;

		Vector3 particlePosition = explosionPosition;
		if (CanReceiveDamage ()) {
			particlePosition = this.transform.position;
		}
		Instantiate (hitParticles, particlePosition, this.transform.rotation);    
	}

    public void TakeDamage (int damage) {
		health.TakeDamage(damage);	
		if (hurtAudio != null)
			hurtAudio.Play ();
		Debug.Log (gameObject.name + " now has " + health.hitPoints + " HP left");
    }

	public void ReceiveExplosiveForce (float explosionForce, Vector3 explosionPosition, float explosionRadius) {
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();

		if (rb) {
			Debug.Log (gameObject.name + " is being hit with " + explosionForce + " force");
			rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
		}
	}

    public void DestroyObject() {
        //TODO: add reference to play any death animation/particles
		Debug.Log(gameObject.name + "is dead");
		Destroy (gameObject);
    }

	public bool CanReceiveDamage () {
		return startingHitPoints > 0 && !health.IsDead ();
	}
}
