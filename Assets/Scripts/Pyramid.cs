using UnityEngine;

public class Pyramid : MonoBehaviour {

    /* Fields */
    public int maxHitPoints;

    Player player;
    int hitPoints;

    /* Lifetime Methods */
    void Start() {
        player = gameObject.GetComponentInParent<Player>();
        hitPoints = maxHitPoints;
	}
	
	void Update () {
	
	}
}
