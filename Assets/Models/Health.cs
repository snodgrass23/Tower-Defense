using UnityEngine;

public class Health {
    
    public int hitPoints = 0;
    
    public Health (int startingHitPoints) {
        hitPoints = startingHitPoints;
    }

	public void TakeDamage (int damage) {
        // Debug.Log(gameObject.name + " took " + damage + " damage");
        Debug.Log("Took " + damage + " damage");
        hitPoints -= damage;
    }
    
    public bool IsDead () {
        return hitPoints <= 0;
    }
}
