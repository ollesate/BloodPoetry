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

    private float xSteps = 13;
    private float currentXSteps;
    private float startSize;
	// Use this for initialization
	void Start () {
        mBoxCollider = GetComponent<BoxCollider2D>();
        startSize = mBoxCollider.size.x;
        health = GetComponent<Health>();
        health.ShouldDestroy = false;
        health.OnTakeDamage += (damage, damager) => Attack(damager);
        mQueueSystem = GetComponentInChildren<QueueSystem>();
        stepHeight = MeasureHeight.bounds.extents.y;
        currentXSteps = xSteps;
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
        if(health.currentHealth > 0)
        {
            pushDownPyramid();
            mQueueSystem.DestroyStep();
            gameObject.GetComponent<WarriorAI>().target = null;
            Destroy(gameObject, .2f);
            if(--currentXSteps != 0)
            {
                mBoxCollider.size = new Vector2((currentXSteps/xSteps) * startSize, mBoxCollider.size.y);
            }
            
        } else
        {
            this.gameObject.tag = "Untagged";
            gameObject.GetComponent<WarriorAI>().target = null;
        }

    }

    private void pushDownPyramid()
    {
        if (Steps != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - stepHeight, 1.5f);
        }
        else
        {
            //Debug.Log("Cant destroy pyramid with 0 steps. Please set nr of steps");
        }
    }
}
