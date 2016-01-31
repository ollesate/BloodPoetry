using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DestroyablePyramid : MonoBehaviour {

    private BoxCollider2D mBoxCollider;
    private QueueSystem mQueueSystem;
    private float stepHeight;
    private Health health;

    public BoxCollider2D MeasureHeight;
    public int Steps;

	// Use this for initialization
	void Start () {
        health = GetComponent<Health>();
        health.OnTakeDamage += (damage, damager) => Attack(damager);
        mQueueSystem = GetComponentInChildren<QueueSystem>();
        stepHeight = MeasureHeight.bounds.extents.y;
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
        Destroy(gameObject, .5f);
    }

    private void pushDownPyramid()
    {
        if (Steps != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - stepHeight, 1.5f);
        }
        else
        {
            Debug.Log("Cant destroy pyramid with 0 steps. Please set nr of steps");
        }
    }
}
