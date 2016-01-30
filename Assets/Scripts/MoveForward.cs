using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveForward : MonoBehaviour {

    public float Speed = 0.5f;

    private Rigidbody2D mRigidbody;

	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        mRigidbody.velocity = new Vector2(Speed, mRigidbody.velocity.y);
	}
}
