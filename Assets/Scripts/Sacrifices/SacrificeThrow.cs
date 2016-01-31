using UnityEngine;
using System.Collections;

public class SacrificeThrow : MonoBehaviour {

	/* Fields */
	public GameObject villagerPrefab;
	public GameObject targetSprite;
	public GameObject myPriest;
	public float loadTime;
	public float forceFactor;

	Player player;
	public bool isUsing;
	float power_newton;
	float angle_rad;
	Vector2 dir;
	float time;

	// Use this for initialization
	void Start()
	{
		isUsing = false;
		player = gameObject.GetComponentInParent<Player>();
	}

	public void StartUsing()
	{
		isUsing = true;
		GetComponent<Fading>().FadeIn();
	}

	// Update is called once per frame
	void Update()
	{
		myPriest.GetComponent<Animator>().SetBool("Throw", false);
		if (isUsing)
		{
			if ( time < 1 )
			{
				time += Time.deltaTime / loadTime;

				if ( time > 1 )
				{
					time = 1;
				}
			}

			if ( player.GetDown( ButtonAction.Green ) )
			{
				// Complete
				GameObject thrown = Instantiate(villagerPrefab);
				thrown.transform.position = gameObject.transform.position;

				thrown.GetComponentInChildren<Rigidbody2D>().AddForce( dir * forceFactor, ForceMode2D.Impulse );
				power_newton = 0;
				time = 0;
				player.SetState( Player.State.Idle );
				isUsing = false;
				GetComponent<Fading>().FadeOut();
				GetComponent<PlaySoundEffect>().PlaySoundAtPos();
				myPriest.GetComponent<Animator>().SetBool("Throw", true);
			}
			else
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
