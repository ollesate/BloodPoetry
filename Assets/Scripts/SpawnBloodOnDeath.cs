using UnityEngine;
using System.Collections;

public class SpawnBloodOnDeath : MonoBehaviour
{

	private ParticleSystem pSBlood;
	public enum Splatter
	{
		Small,
		Medium,
		Lovely
	}
	public Splatter bloodSplatter = Splatter.Small;

	void Start()
	{
		pSBlood = Resources.Load<ParticleSystem>("pSystemBlood");
	}

	void SpawnBlood()
	{
		ParticleSystem ps = ParticleSystem.Instantiate<ParticleSystem>(pSBlood);
		ps.transform.position = transform.position;
		ParticleSystem.Burst burrito = new ParticleSystem.Burst();
		ParticleSystem.Burst quesarito = new ParticleSystem.Burst();
		ParticleSystem.Burst[] burritos = new ParticleSystem.Burst[2];
		switch (bloodSplatter)
		{
			case Splatter.Small:
				burrito.minCount = 15;
				burrito.maxCount = 15;
				burrito.time = 0;
				break;
			case Splatter.Medium:
				burrito.minCount = 20;
				burrito.maxCount = 20;
				burrito.time = 0;
				break;
			case Splatter.Lovely:
				burrito.minCount = 40;  
				burrito.maxCount = 40;
				burrito.time = 0;

				quesarito.minCount = 40;
				quesarito.maxCount = 40;
				quesarito.time = 0.1f;
				burritos[1] = quesarito;
				

				ParticleSystem[] psExtra = new ParticleSystem[1];
				psExtra[0] = pSBlood;
				var sub = ps.subEmitters;
				sub.birth0 = pSBlood;
				sub.death0 = pSBlood;
				sub.enabled = true;
				break;
			default:
				break;
		}
		burritos[0] = burrito;
		ps.emission.SetBursts(burritos);
	}

	void OnDestroy()
	{
		GetComponent<PlaySoundEffect>().PlaySoundAtPos();
		SpawnBlood();
	}
}
