using UnityEngine;
using System.Collections;

public class DestroyablePyramid : MonoBehaviour {

    private BoxCollider2D mBoxCollider;
    private QueueSystem mQueueSystem;
    private float height;

    public int Steps;

	// Use this for initialization
	void Start () {
        mBoxCollider = GetComponent<BoxCollider2D>();
        mQueueSystem = GetComponentInChildren<QueueSystem>();
        height = mBoxCollider.bounds.extents.y * 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(null);
        }
	}

    public void Attack(GameObject gameObject)
    {
        pushDownPyramid();
        mQueueSystem.DestroyStep();
        Destroy(gameObject);
    }

    private void pushDownPyramid()
    {
        if (Steps != 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - height / Steps);
        }
        else
        {
            Debug.Log("Cant destroy pyramid with 0 steps. Please set nr of steps");
        }
    }
}
