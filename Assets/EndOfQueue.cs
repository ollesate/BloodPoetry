using UnityEngine;
using System.Collections;

public class EndOfQueue : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Villager")
        {
            other.GetComponent<MoveForward>().Stop();   
        }
    }
}
