using UnityEngine;
using System.Collections;

public class PlayState : MonoBehaviour {

    /* Fields */
    public bool splitScreen;
    public bool horizontalSplit;

	// Use this for initialization
	void Start () {
        Player[] players = gameObject.GetComponentsInChildren<Player>();

        int N = 0;
        foreach ( Player p in players )
        {
            if ( p.isHuman )
                ++N;
        }

        if ( N == 2 )
            splitScreen = true;


        float Nreci = 1f / N;
        for ( int i = 0; i != N; ++i )
        {
            float border = (N == 2 ? 0.005f : 0);

            if (horizontalSplit)
            {
                if (i == 0)
                {
                    players[i].GetComponentInChildren<Camera>().rect = new Rect( 0, Nreci + border / 2, 1, Nreci - border / 2 );
                }
                else
                {
                    players[i].GetComponentInChildren<Camera>().rect = new Rect( 0, 0, 1, Nreci - border / 2 );
                }
            }
            else
            {
                if (i == 0)
                {
                    players[i].GetComponentInChildren<Camera>().rect = new Rect( 0, 0, Nreci - border / 2, 1 );
                }
                else
                {
                    players[i].GetComponentInChildren<Camera>().rect = new Rect( Nreci + border / 2, 0, Nreci - border / 2, 1 );
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	}

    public Camera[] GetCameras()
    {
        return GetComponentsInChildren<Camera>();
    }
}
