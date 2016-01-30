using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public GameObject[] MenuItems;
	

	void Update () {
		if (Input.GetButton("Fire2"))
		{
				MenuItems[0].SetActive(true);
				MenuItems[1].SetActive(false);
		}
	
	}
}
