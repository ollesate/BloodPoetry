using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    /* Fields */
    public int xBound;
    public float speed;
    Player player;
    Camera cam;

    /* Methods */
    void Awake() {
        player = gameObject.GetComponentInParent<Player>();
        cam = player.GetComponentInChildren<Camera>();

        PlayState playState = gameObject.GetComponentInParent<PlayState>();
        if ( playState.horizontalSplit )
        {
            xBound -= 9;
        }
    }

    void Update() {
        if (player.isHuman) {
            float move = speed * player.Get( AxisAction.CamX ) * Time.deltaTime;

            if ( cam.transform.position.x + move > xBound )
            {
                move = xBound - cam.transform.position.x;
            }
            else if ( cam.transform.position.x + move < -xBound )
            {
                move = -xBound - cam.transform.position.x;
            }

            cam.transform.Translate( move, 0, 0 );
        }
    }
}
