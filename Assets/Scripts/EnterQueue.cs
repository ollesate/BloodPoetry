using UnityEngine;
using System.Collections;

public class EnterQueue : MonoBehaviour {

    private QueueSystem queue;
	// Use this for initialization
	void Start () {
        queue = GetComponentInParent<QueueSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Villager")
        {
            if (queue.CanJoinQueue())
            {
                Debug.Log("A villager joined the queue");
                queue.JoinQueue(other.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Villager")
        {
            if (queue.CanJoinQueue())
            {
                Debug.Log("A villager joined the queue");
                queue.JoinQueue(other.gameObject);
            }
        }
    }

}
