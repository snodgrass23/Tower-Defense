public class Cannonball : WeaponProjectile {

	public int CannonballDamage;
	public float CannonballExplosionRadius;
	public float CannonballExplosionForce;

	float CannonballMaxLifetime = 5f;

	private void Start() {
		damage = CannonballDamage;
		maxLifetime = CannonballMaxLifetime;
		explosionRadius = CannonballExplosionRadius;
		explosionForce = CannonballExplosionForce;
	}
}
