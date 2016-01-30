using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {

    public int rotationOffset_deg;
    public int minutesPerDay;

    float rotation;
    float rotationSpeed;

    // Use this for initialization
    void Start() {
        transform.eulerAngles = new Vector3( -rotationOffset_deg, 90, 0 );
        float rotationDistance = rotationOffset_deg * 2 + 180;
        rotationSpeed = (rotationDistance * Mathf.PI / 180) / (minutesPerDay * 60);
	}

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(rotationSpeed, 0, 0));
	}
}
