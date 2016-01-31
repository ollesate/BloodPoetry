using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveForward : MonoBehaviour {

    public float Speed = 0.5f;

    private Rigidbody2D mRigidbody;
    public bool stopped = false;

	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (stopped == false)
        {   
             mRigidbody.velocity = new Vector2(Speed, mRigidbody.velocity.y);
        }
	}

    public void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        stopped = true;
    }

    public void Go()
    {
        stopped = false;
    }
}
