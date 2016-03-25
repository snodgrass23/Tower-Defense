using UnityEngine;

// Component that can be added to make sure objects are removed from scene as needed
public class InstanceDestroyer : MonoBehaviour {

	public float maxLifetime;

	void Start () {
        Debug.Log("InstanceDestroyer started " + this.gameObject.name);
		Destroy(gameObject, maxLifetime);
	}
    
    public static InstanceDestroyer Create (GameObject where, float ml = 5f) {
        Debug.Log("adding destroyer to " + where.name);
        InstanceDestroyer InsDes = where.AddComponent<InstanceDestroyer>();
        InsDes.maxLifetime = ml;
        return InsDes;
    }
}
