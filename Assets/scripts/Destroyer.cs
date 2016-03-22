using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	public float maxLifetime = 5f;

	// Use this for initialization
	void Start () {
//		print ("instantiated object of " + gameObject.name + " set to destroy in " + maxLifetime + " seconds.");
		Destroy(gameObject, maxLifetime);
	}
}
