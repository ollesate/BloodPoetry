using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    /* Fields */
    public float speed;
    Player player;
    Camera cam;

    /* Methods */
    void Awake() {
        player = gameObject.GetComponentInParent<Player>();
        cam = player.GetComponentInChildren<Camera>();
	}

    void Update() {
        if (player.isHuman) {
            cam.transform.Translate( speed * player.Get( AxisAction.CamX ) * Time.deltaTime, 0, 0 );
        }
    }
}
