using UnityEngine;
using System.Collections;

public class SpawnBloodOnDeath : MonoBehaviour {

	public ParticleSystem pSBlood;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnBlood()
	{
		ParticleSystem ps = ParticleSystem.Instantiate<ParticleSystem>(pSBlood);
		ps.transform.position = transform.position;
	}

	void OnDestroy()
	{
		SpawnBlood();
	}
}
