using UnityEngine;
using System.Collections;

public class SunCycle : MonoBehaviour {

    /* Fields */
    public float offsetX;
    public float offsetY;
    public float dayTime_minutes;
    public float angleStart;
    public Color zenithColor;
    public Color dawnColor;
    public float dawnT;
    public float zenithT;

    float angle;
    float colorAmount;
    float rotationSpeed;

    Vector3 startPosition;
    Camera[] cams;

    /* Methods */
    void Start () {

        angle = 0;
        colorAmount = 0;

        float seconds = dayTime_minutes * 60;
        float totalRotate = Mathf.PI;
        rotationSpeed = totalRotate / seconds;
        startPosition = transform.position;

        cams = GetComponentInParent< PlayState>().GetCameras();
	}
	
	void Update () {

        angle += rotationSpeed * Time.deltaTime;
        transform.position = startPosition + new Vector3( offsetX * Mathf.Cos( angle ), offsetY * Mathf.Sin( angle ), 0 );

        colorAmount = Mathf.Max( Mathf.Sin( angle ), 0 );

        float t = 0;
        if ( colorAmount < dawnT )
        {
            t = colorAmount / dawnT;
            foreach ( var cam in cams )
            {
                cam.backgroundColor = Color.Lerp( Color.black, dawnColor, t );
            }
        }
        else if (colorAmount < zenithT)
        {
            t = ( colorAmount - dawnT ) / ( zenithT - dawnT );
            foreach ( var cam in cams )
            {
                cam.backgroundColor = Color.Lerp( dawnColor, zenithColor, t );
            }
        }
        else
        {
            foreach ( var cam in cams )
            {
                cam.backgroundColor = zenithColor;
            }
        }
    }
}
