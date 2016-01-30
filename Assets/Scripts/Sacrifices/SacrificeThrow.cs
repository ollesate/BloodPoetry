using UnityEngine;
using System.Collections;

public class SacrificeThrow : MonoBehaviour {

    /* Fields */
    public GameObject villagerPrefab;
    public GameObject targetSprite;
    public int playerIndex;
    public float loadTime;

    bool isThrowing;
    float power_newton;
    float angle_rad;
    Vector2 dir;
    float time;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( time < 1 ) {
            time += Time.deltaTime / loadTime;

            if ( time > 1) {
                time = 1;
            }
        }

        if ( Input.GetButtonDown( "Throw " + playerIndex.ToString() ) )
        {
            // Commence
            isThrowing = true;
            power_newton = 0;
            time = 0;
        }
        else if ( Input.GetButtonUp( "Throw " + playerIndex.ToString() ) )
        {
            // Complete
            GameObject thrown = Instantiate( villagerPrefab );
            thrown.transform.position = gameObject.transform.position;

            thrown.GetComponent<Rigidbody2D>().AddForce( dir * 2, ForceMode2D.Impulse );
            isThrowing = false;
        }
        else if ( isThrowing )
        {
            // Aim
            dir = power_newton * new Vector2( Mathf.Cos( angle_rad ), Mathf.Sin( angle_rad ) );
            targetSprite.transform.position = gameObject.transform.position + new Vector3( dir.x, dir.y ) / 5;
            power_newton = Mathf.Sin( time * Mathf.PI / 2 );


        }

        float x = Input.GetAxisRaw( "Horizontal " + playerIndex.ToString() );
        float y = Input.GetAxisRaw( "Vertical " + playerIndex.ToString() );

        angle_rad = Mathf.Atan2( y, x );
        
        //Debug.Log( angle_rad );
    }
}
