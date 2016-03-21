using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4;
        public float explosionRadius = 100;


        private IEnumerator Start()
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;

			var cols = Physics.OverlapSphere(transform.position, explosionRadius);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
				string hitLayer = LayerMask.LayerToName (col.gameObject.layer);

				if (hitLayer.Equals ("Damageable") || hitLayer.Equals ("Terrain")) {
					if (col.attachedRigidbody != null && !rigidbodies.Contains (col.attachedRigidbody)) {
						rigidbodies.Add (col.attachedRigidbody);
					}
				}
            }
            foreach (var rb in rigidbodies)
            {
				rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
            }
        }
    }
}
