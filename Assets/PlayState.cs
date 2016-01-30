using UnityEngine;
using System.Collections;

public class PlayState : MonoBehaviour {

    /* Fields */
    public bool splitScreen;

	// Use this for initialization
	void Start () {
        Player[] players = gameObject.GetComponentsInChildren<Player>();

        int N = 0;
        foreach ( Player p in players )
        {
            if ( p.isHuman )
                ++N;
        }

        float Nreci = 1f / N;
        for ( int i = 0; i != N; ++i )
        {
            players[i].GetComponentInChildren<Camera>().rect = new Rect( i * Nreci, 0, Nreci, 1 );
        }

    }
	
	// Update is called once per frame
	void Update () {
	}
}
