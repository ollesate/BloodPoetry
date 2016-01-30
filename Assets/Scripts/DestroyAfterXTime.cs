using UnityEngine;
using System.Collections;

public class DestroyAfterXTime : MonoBehaviour {

	public float lifetime = 2.0f;
	void Start () {
		Destroy(gameObject, lifetime);	
	}

}
