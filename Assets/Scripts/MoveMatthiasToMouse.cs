using UnityEngine;
using System.Collections;

public class MoveMatthiasToMouse : MonoBehaviour {

	Vector3 startPos;

	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1) )
		{
			//Vector3 pos = Input.mousePosition;
			//pos.z = 0;
			//transform.position = pos;

			transform.position = startPos;
			Quaternion rot = Random.rotation;
			rot.x = 0;
			rot.y = 0;
			rot.z = 0;
			transform.rotation = rot;
		}
	
	}
}
