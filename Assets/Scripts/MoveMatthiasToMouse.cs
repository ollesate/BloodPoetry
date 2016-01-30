using UnityEngine;
using System.Collections;

public class MoveMatthiasToMouse : MonoBehaviour {

	Vector3 startPos;

	float lifetime = 8.0f;
	float rad = 5.0f;

	void Start () {
		startPos = transform.position;
		Destroy(transform.parent.gameObject, lifetime);
	}
	
	void FixedUpdate () {
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1) )
		{
			transform.position = startPos;
			Quaternion rot = Random.rotation;
			rot.x = 0;
			rot.y = 0;
			rot.z = 0;
			transform.rotation = rot;
		}


		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rad);
		int nowCollidingWith = 0;
		foreach (Collider2D col in colliders)
		{
			if (col.name == "Body")
			{
				nowCollidingWith += 1;
			}
		}
		if (nowCollidingWith >= 6)
		{
			Destroy(transform.parent.gameObject);
		}
	}

}
