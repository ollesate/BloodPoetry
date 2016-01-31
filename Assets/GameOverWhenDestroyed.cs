using UnityEngine;
using System.Collections;

public class GameOverWhenDestroyed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        GetComponentInParent<Player>().GameOver();
    }
}
