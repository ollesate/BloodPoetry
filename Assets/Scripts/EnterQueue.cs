using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class EnterQueue : MonoBehaviour {

	private BoxCollider2D mBoxCollider;
	private QueueSystem queue;
	// Use this for initialization
	void Start () {
		mBoxCollider = GetComponent<BoxCollider2D>();
		queue = GetComponentInParent<QueueSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Villager")
		{
			if (queue.JoinQueueAtStep(other.gameObject, mBoxCollider))
			{
				//Debug.Log("A villager joined the queue");
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Villager")
		{
			if (queue.JoinQueueAtStep(other.gameObject, mBoxCollider))
			{
                //Debug.Log("A villager joined the queue");
			}
		}
	}

}
