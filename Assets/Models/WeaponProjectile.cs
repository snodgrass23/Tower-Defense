using UnityEngine;

public class WeaponProjectile : MonoBehaviour {
   
    protected float maxLifetime;
    protected int damage;
    protected float explosionRadius;
    protected float explosionForce;

	void Start () {
	    InstanceDestroyer.Create(gameObject, maxLifetime);
	}
	
	private void OnCollisionEnter(Collision otherObj) { 
        DamageableObject obj = otherObj.gameObject.GetComponent<DamageableObject>();
        
        if (obj != null) 
            DoImpact (obj);    
	}
    
    private void DoImpact(DamageableObject hitObject) {
		HitNearbyObjects ();
		Destroy (gameObject);
	}
    
    private void HitNearbyObjects () {
		Collider[] colliders = Physics.OverlapSphere (this.transform.position, explosionRadius);
        
        Debug.Log("weapon impact hit " + colliders + " objects");

		for (int i = 0; i < colliders.Length; i++) {
			DamageableObject obj = colliders[i].gameObject.GetComponent<DamageableObject>();
            
            if (obj != null) {
                obj.ApplyHit(damage, explosionForce, this.transform.position, explosionRadius);
            }
		}
	}
}
