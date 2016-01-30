using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEffect : MonoBehaviour {


	private AudioClip myClip;
	public AudioClip myAttackClip;
	private AudioSource myASource;

	void Start () {
		myASource = GetComponent<AudioSource>();
		myClip = myASource.clip;
	}
	
	void Update () {
	
	}

	public void PlaySound()
	{
		if (myClip.isReadyToPlay)
		{
			myASource.PlayOneShot(myASource.clip);
		}
	}

	public void PlaySoundAtPos()
	{
		AudioSource.PlayClipAtPoint(myClip, transform.position);
	}
	public void PlayMyAttack()
	{
		AudioSource.PlayClipAtPoint(myAttackClip, transform.position);
	}

}
