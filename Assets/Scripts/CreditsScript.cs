using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public GameObject[] MenuItems;

	ProperInput pIn;

	void Start()
	{
		pIn = new ProperInput(0);
	}

	void Update () {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MVolume");
		if (pIn.GetDown(ButtonAction.Green) || pIn.GetDown(ButtonAction.Red))
		{
				MenuItems[0].SetActive(true);
				MenuItems[1].SetActive(false);
		}
	}
}
