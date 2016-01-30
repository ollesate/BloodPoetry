using UnityEngine;

public class SacrificeThrow : MonoBehaviour {

    /* Fields */
    public GameObject villagerPrefab;
    public GameObject targetSprite;
    public float loadTime;
    public float powerFactor;

    Player player;
    bool isThrowing;
    float power_newton;
    float angle_rad;
    Vector2 dir;
    float time;

    /* Lifetime Methods */
    void Start() {
        player = gameObject.GetComponentInParent<Player>();
    }

    void Update()
    {
        if ( time < 1 ) {
            time += Time.deltaTime / loadTime;

            if ( time > 1) {
                time = 1;
            }
        }

        if (player.isHuman)
        {
            if ( player.GetDown( ButtonAction.Green ) )
            {
                // Commence
                isThrowing = true;
                power_newton = 0;
                time = 0;
            }
            else if ( player.GetUp( ButtonAction.Green ) )
            {
                // Complete
                GameObject thrown = Instantiate( villagerPrefab );
                thrown.transform.position = gameObject.transform.position;

                thrown.GetComponentInChildren<Rigidbody2D>().AddForce( dir * powerFactor, ForceMode2D.Impulse );
                isThrowing = false;
            }
            else if ( isThrowing )
            {
                // Aim
                dir = power_newton * new Vector2( Mathf.Cos( angle_rad ), Mathf.Sin( angle_rad ) );
                targetSprite.transform.position = gameObject.transform.position + new Vector3( dir.x, dir.y ) / 5;
                power_newton = Mathf.Sin( time * Mathf.PI / 2 );
            }

            float x = player.Get( AxisAction.AimX );
            float y = player.Get( AxisAction.AimY );

            angle_rad = Mathf.Atan2( y, x );
        }
    }
}
